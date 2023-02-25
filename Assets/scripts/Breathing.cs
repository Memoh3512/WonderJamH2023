using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing : MonoBehaviour
{
    private float timeElapsed = 0;
    public float stressLevel = 1;
    public AnimationCurve breathCurve;
    public float amplitude = 1;
    //public float replayTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
            if (breathCurve == null) return;
          //  if (timeElapsed >= replayTime) timeElapsed = 0;
            timeElapsed += Time.deltaTime * stressLevel;
            transform.position = new Vector3((breathCurve.Evaluate(timeElapsed)*amplitude) / 4, breathCurve.Evaluate(timeElapsed)*amplitude, transform.position.z);
            
        

    }
}
