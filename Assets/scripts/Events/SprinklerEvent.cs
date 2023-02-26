using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerEvent : JackEvent
{
    GameObject[] dropletList = new GameObject[18];
    GameObject[] positions = new GameObject[18];
    private float timeElapsed = 0;
    private float animationTime = 7;
    public AnimationCurve dropletCurve = Resources.Load<AnimationCurveAsset>("dropletCurve");

    public override void EventEnded()
    {
        if (onEventEnded == null) return;
        onEventEnded.Invoke();
    }
    public override void ExecuteEvent()
    {
        timeElapsed = 0;

        GameObject dropletPrefab = Resources.Load<GameObject>("Droplet");
        if (dropletPrefab != null)
        {
            for(int x = 0; x < 17; x++)
            {
                dropletList[x] = GameObject.Instantiate(dropletPrefab); 
               
            }
            positions = GameObject.FindGameObjectsWithTag("droplet");
            for (int x = 0; x < 17; x++)
            {
                dropletList[x].transform.position = positions[x].transform.position;
                dropletList[x].transform.position = new Vector3(dropletList[x].transform.position.x, dropletList[x].transform.position.y + 2, dropletList[x].transform.position.z);

            }
            


                Distract();

        }

    }
    public void Distract()
    {
        BlackJackManager.StartGlobalCoroutine(Distraction());
    }

    IEnumerator Distraction()
    {
        BlackJackManager.DistractAll(20);
        while (timeElapsed < animationTime)
        {
           
            yield return null;
            timeElapsed += Time.deltaTime;
            for (int x = 0; x < 17; x++)
            {
                
                dropletList[x].transform.position = new Vector3(dropletList[x].transform.position.x, dropletCurve.Evaluate(timeElapsed) + (positions[x].transform.position.y-6)*3 +2, 0);
;
            
            }
        }
        BlackJackManager.DistractAll(0);

        EventEnded();
    }

   
}
