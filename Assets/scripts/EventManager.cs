using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private List<int> EventList= new List<int>();
    public int minEventTime;
    public int maxEventTime;
    // Start is called before the first frame update
    void Start()
    {
        EventList.Add(1);
        EventList.Add(2);
        minEventTime = 20;
        maxEventTime = 45;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EventRoutine()
    {

        while(true)
        {

        }
        
    }

}
