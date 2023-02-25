using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private List<JackEvent> EventList= new List<JackEvent>();
    public float minEventTime = 20;
    public float maxEventTime = 45;
    // Start is called before the first frame update
    void Start()
    {
       //faire un add pour chaque type d'évent
        StartCoroutine(EventRoutine());
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    IEnumerator EventRoutine()
    {
        yield return new WaitForSeconds(Random.Range(minEventTime, maxEventTime));
        JackEvent randomEvent = null;
        if(randomEvent != null)randomEvent = EventList[Random.Range(0, EventList.Count - 1)];
        randomEvent.ExecuteEvent();
        randomEvent.addListenerEventEnded(() => {
            StartCoroutine(EventRoutine());
        }); 
        
        
    }

}
