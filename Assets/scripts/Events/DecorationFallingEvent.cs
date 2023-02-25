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

    IEnumerator Distraction()
    {
        BlackJackManager.DistractAll(10);
        yield return new WaitForSeconds(5);
        BlackJackManager.DistractAll(0);
        EventEnded();
    }
}
