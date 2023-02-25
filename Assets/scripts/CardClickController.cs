using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardRepresentation))]
public class CardClickController : MonoBehaviour
{
    public static CardClickController heldCard;

    bool held = false;
    public bool inHolder = false;
    GameObject hand;
    public GameObject holder;

    private CardRepresentation cardRep;
    void Start()
    {
        hand = GameObject.Find("Hand");
        cardRep = GetComponent<CardRepresentation>();
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
               
    }

    public void PickUpCard(GameObject holder = null)
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
