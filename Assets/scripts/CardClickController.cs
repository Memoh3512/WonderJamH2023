using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardRepresentation))]
public class CardClickController : MonoBehaviour
{
    Color originialColor;
    Color hoverColor;
    SpriteRenderer sr;

    public static CardClickController heldCard;

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

    public void OnMouseEnter()
    {
        if (holder != null)
        {
            holder.GetComponent<CardHolder>().OnMouseEnter();
        }
    }

    public void OnMouseExit()
    {
        if (holder != null)
        {
            holder.GetComponent<CardHolder>().OnMouseExit();
        }
    }

    public void OnMouseDown()
    {

        if(holder == null && heldCard == null)
        {
            PickUpCard();
        }
        if(holder != null)
        {
            holder.GetComponent<CardHolder>().OnMouseDown();
        }
        
        /*
        if (!held && !inHolder && !handFull)
        {
            //pickup card
            gameObject.transform.position = hand.transform.position;
            gameObject.transform.parent = hand.transform;
            handFull = true;
            held = true;
            HeldCard = gameObject;
            if (holder != null)
            {
                if (holder.GetComponent<CardHolder>().owner != null)
                {
                    DeckManager.PlayerCards[holder.GetComponent<CardHolder>().owner].Remove(cardRep.card);
                }
            }

        }
        else if(holder != null && holder.GetComponent<CardHolder>().hovered)
        {
            //drop card
            gameObject.transform.position = holder.transform.position;
            gameObject.transform.parent = holder.transform;
            handFull = false;
            inHolder = true;
            held = true;
            holder.GetComponent<CardHolder>().SwapCard(cardRep.card);

        }*/
        
    }

    public void PickUpCard()
    {
        gameObject.transform.position = hand.transform.position;
        gameObject.transform.parent = hand.transform;
        heldCard = this;
        if (holder != null)
        {
            if (holder.GetComponent<CardHolder>().holdersManager.owner != null)
            {
                DeckManager.PlayerCards[holder.GetComponent<CardHolder>().holdersManager.owner].Remove(cardRep.card);
            }
        }

    }


    public void PutDownCard(GameObject holder)
    {
        gameObject.transform.position = holder.transform.position;
        gameObject.transform.parent = holder.transform;
        heldCard = null;
        holder.GetComponent<CardHolder>().SwapCard(cardRep.card);
    }

}
