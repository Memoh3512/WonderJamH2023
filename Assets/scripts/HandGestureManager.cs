using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGestureManager : MonoBehaviour
{
    public AIJackPlayer jackPlayer;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void HitGesture()
    {
        anim.SetTrigger("Hit");
    }

    public void HoldGesture()
    {
        anim.SetTrigger("Hold");   
    }

}
