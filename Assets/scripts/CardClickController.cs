using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardClickController : MonoBehaviour
{
    Color originialColor;
    Color hoverColor;
    SpriteRenderer sr;

    static bool handFull = false;
    bool held = false;
    public bool inHolder = false;
    GameObject hand;
    public GameObject holder;
    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("Hand");
        sr = gameObject.GetComponent<SpriteRenderer>();
        originialColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.75f);
        hoverColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
    }

    public void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    public void OnMouseExit()
    {
        sr.color = originialColor;
    }

    public void OnMouseDown()
    {
        if (!held && !inHolder && !handFull)
        {
            gameObject.transform.position = hand.transform.position;
            gameObject.transform.parent = hand.transform;
            handFull = true;
            held = true;
        }
        else if(holder != null)
        {
            gameObject.transform.position = holder.transform.position;
            gameObject.transform.parent = holder.transform;
            held = false;
            handFull = false;
            inHolder = true;
        }
        
    }

}
