using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controls Animation Events setted in the Animation Window in Unity.
/// Attach this in a object with Animator component to receive events.
/// </summary>
public class AnimationEventsController : MonoBehaviour 
{
	public delegate void OnAnimationEventDelegate();
	public OnAnimationEventDelegate onAnimEventCallback;

	private Dictionary<string, OnAnimationEventDelegate> events;

	public void CreateEvent(string pEventName, OnAnimationEventDelegate pCallback)
	{
		if(events == null)
			events = new Dictionary<string, OnAnimationEventDelegate>();

		if(!events.ContainsKey(pEventName))
			events.Add(pEventName, pCallback);
		else
			events[pEventName] = pCallback;
	}

	public void OnAnimationEvent(string pEventName)
	{
		if(events != null)
		{
			if(events.ContainsKey(pEventName))
			{
				if(events[pEventName] != null)
					events[pEventName]();
			}
			else
			{
				Debug.LogError("Event " + pEventName + " not found! Maybe there is no receiver for " +
					"this event...");
			}
		}
	}
}
