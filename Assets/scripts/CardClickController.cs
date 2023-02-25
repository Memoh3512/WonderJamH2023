using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardRepresentation))]
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

    private CardRepresentation cardRep;
    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("Hand");
        //sr = gameObject.GetComponent<SpriteRenderer>();
        cardRep = GetComponent<CardRepresentation>();
        // originialColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.75f);
        // hoverColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
    }

    public void OnMouseDown()
    {
        if (!held && !inHolder && !handFull)
        {
            //pickup card
            gameObject.transform.position = hand.transform.position;
            gameObject.transform.parent = hand.transform;
            handFull = true;
            held = true;

            if (holder != null)
            {
                if (holder.GetComponent<CardHolder>().owner != null)
                {
                    DeckManager.PlayerCards[holder.GetComponent<CardHolder>().owner].Remove(cardRep.card);
                }   
            }

        }
        else if(holder != null)
        {
            //drop card
            gameObject.transform.position = holder.transform.position;
            gameObject.transform.parent = holder.transform;
            held = false;
            handFull = false;
            inHolder = true;
            holder.GetComponent<CardHolder>().SwapCard(cardRep.card);

        }
        
    }

}
