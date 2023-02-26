using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardRepresentation))]
public class CardClickController : MonoBehaviour
{
    public static CardClickController heldCard;

    bool held = false;
    public bool inHolder = false;
    private bool flipped = false;
    public Vector2 offsetHolder;
    GameObject hand;
    public GameObject holder;

    public SpriteRenderer side1;
    public SpriteRenderer side2;

    public CardRepresentation cardRep;
    void Start()
    {

        
    }

    public void Init()
    {
        hand = GameObject.Find("Hand");
        cardRep = GetComponent<CardRepresentation>();
        side1 = transform.Find("Side1").GetComponent<SpriteRenderer>();
        side2 = side1.transform.Find("Side2").GetComponent<SpriteRenderer>();
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

    public void flipCard()
    {
        Sprite temp = side1.sprite;
        side1.sprite = side2.sprite;
        side2.sprite = temp;
        flipped = !flipped;
        
        
        SoundPlayer.instance.PlaySFX("sfx/Flip card");
    }

    public void PickUpCard(GameObject holder = null)
    {
        transform.position = hand.transform.position;
        transform.rotation = hand.transform.rotation;
        GetComponent<FollowObject>().target = hand.transform;
        GetComponent<FollowObject>().offset = Vector3.zero;

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
        transform.rotation = holder.transform.rotation;
        transform.position = holder.transform.position;
        GetComponent<FollowObject>().target = holder.transform;
        GetComponent<FollowObject>().offset = new Vector3(0.203f,-0.04f,0);
        heldCard = null;
        holder.GetComponent<CardHolder>().SwapCard(cardRep.card);
        if (!flipped) flipCard();
    }

}
