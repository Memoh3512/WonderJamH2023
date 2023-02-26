using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    float suslevel = 0;
    public float susrate;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Card") return;
        suslevel += Time.fixedDeltaTime*susrate;
        if(suslevel >= 1)
        {
            BlackJackManager.DoIllegalAction(1);
            suslevel = 0;
        }
        
    }
}
