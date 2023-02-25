using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaker : MonoBehaviour
{
    private float timeElapsed = 0;
    public bool canFall = false;
    public float shakeTime = 0;
    public AnimationCurve shakeCurve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= shakeTime)
        {
           
        }
        else
        {
            transform.eulerAngles = new Vector3(0,0, shakeCurve.Evaluate(timeElapsed));
                
        }
        
    }
}
