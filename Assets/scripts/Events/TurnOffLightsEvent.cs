using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLightsEvent : JackEvent
{
    public override void EventEnded()
    {
        if (onEventEnded == null) return;
        onEventEnded.Invoke();
    }
    public override void ExecuteEvent()
    {
        
        SoundPlayer.instance.PlaySFX("sfx/Panne de courant");
        GameObject effectPrefab = Resources.Load<GameObject>("LightsOffEffect");
        if (effectPrefab != null)
        {
            GameObject effect = GameObject.Instantiate(effectPrefab);
            Distract(effect);
            
        }

    }
    public void Distract(GameObject effect)
    {
        BlackJackManager.StartGlobalCoroutine(Distraction(effect));
    }

    IEnumerator Distraction(GameObject effect)
    {
        BlackJackManager.DistractAll(40);
        yield return new WaitForSeconds(5);
        

        BlackJackManager.DistractAll(0);
       EventEnded();
    }

   
}
