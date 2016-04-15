using UnityEngine;
using System;
using System.Collections.Generic;

public class SKStateMachine<T>
{
	protected T context;
#pragma warning disable
	public event Action onStateChanged;
#pragma warning restore

    public SKState<T> _CurrentState { get { return currentState; } }
	public SKState<T> previousState;
	public float elapsedTimeInState = 0f;


    private Dictionary<System.Type, SKState<T>> states = new Dictionary<System.Type, SKState<T>>();
	private SKState<T> currentState;


	public SKStateMachine( T pContext, SKState<T> pInitialState )
	{
		this.context = pContext;

		// setup our initial state
		AddState( pInitialState );
		currentState = pInitialState;
		currentState.Begin();
	}


	/// <summary>
	/// adds the state to the machine
	/// </summary>
	public void AddState( SKState<T> pState )
	{
		pState.SetMachineAndContext( this, context );
		states[pState.GetType()] = pState;
	}


	/// <summary>
	/// ticks the state machine with the provided delta time
	/// </summary>
	public void Update( float pDeltaTime )
	{
		elapsedTimeInState += pDeltaTime;
		currentState.Reason();
		currentState.Update( pDeltaTime );
	}

    public void OnGUI()
    {
        currentState.OnGUI();
    }

	/// <summary>
	/// changes the current state
	/// </summary>
	public R ChangeState<R>() where R : SKState<T>
	{
		// avoid changing to the same state
		var newType = typeof( R );
		if( currentState.GetType() == newType )
			return currentState as R;

		// only call end if we have a currentState
		if( currentState != null )
			currentState.End();

		#if UNITY_EDITOR
		// do a sanity check while in the editor to ensure we have the given state in our state list
		if( !states.ContainsKey( newType ) )
		{
			var error = GetType() + ": state " + newType + " does not exist. Did you forget to add it by calling addState?";
			Debug.LogError( error );
			throw new Exception( error );
		}
		#endif

		// swap states and call begin
		previousState = currentState;
		currentState = states[newType];
		currentState.Begin();
		elapsedTimeInState = 0f;

		// fire the changed event if we have a listener
		if( onStateChanged != null )
			onStateChanged();

		return currentState as R;
	}

}
