using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class shaker : MonoBehaviour
{
    private float timeElapsed = 0;
    public bool canFall = false;
    public float shakeTime = 0;
    public float fallTime = 0;
    private float timeFalling = 0;
    public AnimationCurve shakeCurve;
    private bool stop = false;

    private UnityEvent onShakeEnded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            timeElapsed += Time.deltaTime;
            if (canFall) timeFalling += Time.deltaTime;
            if (timeElapsed >= shakeTime)
            {
                canFall = true;
                if (timeFalling >= fallTime)
                {
                    transform.position += new Vector3(0, timeFalling, 0);
                    stop = true;
                    onShakeEnded.Invoke();


                }

            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, shakeCurve.Evaluate(timeElapsed));

            }
        }
            
    }
    public void addListenerShakeEnded(UnityAction action)
    {
        if (onShakeEnded == null) onShakeEnded = new UnityEvent();

        onShakeEnded.AddListener(action);
    }

}
