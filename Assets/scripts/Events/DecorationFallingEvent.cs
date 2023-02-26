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
        //List<GameObject> decorations = new List<GameObject>(GameObject.FindGameObjectsWithTag("Decoration"));
       // if (decorations.Count == 0) return;
        Debug.Log("Decoration falling");
        GameObject deco = GameObject.FindGameObjectWithTag("Decoration");

        shaker s = deco.GetComponent<shaker>();
        s.StartShake();
        s.addListenerShakeEnded(Distract);
        
    }


    public void Distract()
    {
        BlackJackManager.StartGlobalCoroutine(Distraction());
    }

    IEnumerator Distraction()
    {
        BlackJackManager.DistractAll(80);
        yield return new WaitForSeconds(3);
        BlackJackManager.DistractAll(0);
        EventEnded();
    }
}
