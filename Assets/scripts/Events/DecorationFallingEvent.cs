using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationFallingEvent : JackEvent
{
    public override void EventEnded()
    {
        if (onEventEnded == null) return;
        onEventEnded.Invoke();
    }

    public override void ExecuteEvent()
    {

    }
}
