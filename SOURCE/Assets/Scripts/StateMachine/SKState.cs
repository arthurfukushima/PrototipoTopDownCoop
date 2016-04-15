using UnityEngine;

public abstract class SKState<T>
{
    protected float timeOnState;
	protected int mecanimStateHash;
	protected SKStateMachine<T> machine;
	protected T context;
	
	
	public SKState()
	{}


	/// <summary>
	/// constructor that takes the mecanim state name as a string
	/// </summary>
	public SKState( string pMecanimStateName ) : this( Animator.StringToHash( pMecanimStateName ) )
	{}
	
	
	/// <summary>
	/// constructor that takes the mecanim state hash
	/// </summary>
	public SKState( int pMecanimStateHash )
	{
        mecanimStateHash = pMecanimStateHash;
	}

	
	internal void SetMachineAndContext( SKStateMachine<T> pMachine, T pContext )
	{
        machine = pMachine;
		context = pContext;

		OnInitialized();
	}


	/// <summary>
	/// called directly after the machine and context are set allowing the state to do any required setup
	/// </summary>
	public virtual void OnInitialized()
	{}


	public virtual void Begin()
	{
        timeOnState = 0.0f;
    }
	
	
	public virtual void Reason()
	{}
	
	
	public virtual void Update( float pDeltaTime )
    {
        timeOnState += pDeltaTime ;  
    }
	
	
	public virtual void End()
	{}

    public virtual void OnGUI()
    {
       
    }
}
