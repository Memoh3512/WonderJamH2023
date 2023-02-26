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
        BlackJackManager.DistractAll(20);
        yield return new WaitForSeconds(5);
        //Debug.Log("destroy");

        BlackJackManager.DistractAll(0);
       EventEnded();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
