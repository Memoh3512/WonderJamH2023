using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private List<int> EventList= new List<int>();
    public float minEventTime;
    public float maxEventTime;
    
    // Start is called before the first frame update
    void Start()
    {
        EventList.Add(1);
        EventList.Add(2);
        minEventTime = 20;
        maxEventTime = 45;
        StartCoroutine(EventRoutine());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator EventRoutine()
    {

        while(true)
        {
          
            yield return new WaitForSeconds( Random.Range(minEventTime, maxEventTime));
            //le toString est pas la bonne chose j'ai juste pas encore le Event dans la list
            EventList[Random.Range(0, EventList.Count - 1)].ToString();



        }
        
    }

}
