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
    private bool falling = false;
    public AnimationCurve shakeCurve;
    private bool stop = false;
    bool started = false;

    private UnityEvent onShakeEnded;
   
    public void StartShake()
    {
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) return;
        if (shakeCurve == null) return;
        if (!stop)
        {
            timeElapsed += Time.deltaTime;
            if (falling) timeFalling += Time.deltaTime;
            if (timeElapsed >= shakeTime)
            {
                if (!canFall)
                {
                    stop = true;
                    if (onShakeEnded != null) onShakeEnded.Invoke();
                    Destroy(gameObject);
                }
                else
                {
                    falling = true;
                    if (timeFalling >= fallTime)
                    {
                        stop = true;
                        if (onShakeEnded != null) onShakeEnded.Invoke();
                        Destroy(gameObject);
                    }
                    else
                    {
                        transform.position -= new Vector3(0, timeFalling/2, 0);
                    }
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
