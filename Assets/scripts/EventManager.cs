using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private List<JackEvent> EventList= new List<JackEvent>();
    public float minEventTime = 20;
    public float maxEventTime = 45;
    private bool firstEvent;
    
    // Start is called before the first frame update
    void Start()
    {
       //faire un add pour chaque type d'évent
        StartCoroutine(EventRoutine());
        firstEvent = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator EventRoutine()
    {

        while(true)
        {
            if (firstEvent)
            {
                yield return new WaitForSeconds( Random.Range(minEventTime, maxEventTime));
                firstEvent = false;

            }

            Debug.Log("Execute event!");
            if (EventList.Count > 0) EventList[Random.Range(0, EventList.Count - 1)].ExecuteEvent();
            yield return new WaitForSeconds(Random.Range(minEventTime, maxEventTime));

        }
        
    }

}
