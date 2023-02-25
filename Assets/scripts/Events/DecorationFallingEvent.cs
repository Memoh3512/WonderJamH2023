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
        List<GameObject> decorations = new List<GameObject>(GameObject.FindGameObjectsWithTag("Decoration"));
        if (decorations.Count == 0) return;

        GameObject deco = decorations[Random.Range(0, decorations.Count)];

        shaker s = deco.AddComponent<shaker>();

        s.addListenerShakeEnded(Distract);
        
    }


    public void Distract()
    {
        BlackJackManager.StartGlobalCoroutine(Distraction());
    }

    IEnumerator Distraction()
    {
        BlackJackManager.DistractAll(10);
        yield return new WaitForSeconds(5);
        BlackJackManager.DistractAll(0);
        EventEnded();
    }
}
