using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class JackEvent
{
    protected UnityEvent onEventEnded;


    abstract public void ExecuteEvent();
    abstract public void EventEnded();


    public void addListenerEventEnded(UnityAction action)
    {
        if (onEventEnded == null) onEventEnded = new UnityEvent();

        onEventEnded.AddListener(action);
    }

    public void ClearListeners()
    {
        onEventEnded.RemoveAllListeners();
    }

}
