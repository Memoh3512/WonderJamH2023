using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardRepresentation))]
public class CardHolder : MonoBehaviour
{
    Color originialColor;
    Color hoverColor;
    SpriteRenderer sr;
    private CardRepresentation cardRep;
    private JackPlayer owner;

    public int holderNumber;
    public HoldersManager holdersManager;

    public CardClickController heldCard;

    public bool active = false;
    public bool required = false;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        cardRep = GetComponent<CardRepresentation>();
        originialColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.75f);
        hoverColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
    }

    public void SetOwner(JackPlayer newOwner)
    {
        owner = newOwner;
    }
    public void InitManager(int holderNumber, HoldersManager holdersManager)
    {
        this.holderNumber = holderNumber;
        this.holdersManager = holdersManager;
    }

    public void ResetHolder() 
    { 
        if(heldCard != null)
        {
            Destroy(heldCard.gameObject);
            heldCard = null;
            GetComponent<CardRepresentation>().card = new Card(0, null);
        }
    }
    public virtual void OnMouseDown()
    {
        if(heldCard == null)
        {
            if(CardClickController.heldCard != null)
            {
                heldCard = CardClickController.heldCard;
                CardClickController.heldCard.PutDownCard(gameObject);
                holdersManager.CardAdded();
            }
        }
        else if(CardClickController.heldCard == null)
        {
            heldCard.PickUpCard(gameObject);
            heldCard = null;
            GetComponent<CardRepresentation>().card = new Card(0, null);
            holdersManager.CardRemoved();
        }
        else
        {
            CardClickController temp = CardClickController.heldCard;
            CardClickController.heldCard.PutDownCard(gameObject);
            heldCard.PickUpCard(gameObject);
            heldCard = temp;
        }

    }

    public void Activate(bool required)
    {
        active = true;
        this.required = required;
    }

    public void Deactivate()
    {
        active = false;
        this.required = false;
    }


    public void OnMouseExit()
    {
        sr.color = originialColor;
        if(CardClickController.heldCard != null)
            CardClickController.heldCard.holder = null;
    }
    public void OnMouseEnter()
    {
        sr.color = hoverColor;
        if (CardClickController.heldCard != null)
            CardClickController.heldCard.holder = gameObject;
    }


    public void SwapCard(Card newCard)
    {
        if (owner != null)
        {
            Card myCard = GetComponent<CardRepresentation>().card;
            DeckManager.PlayerCards[owner].Remove(myCard);
            if (GetComponent<CardRepresentation>().card != newCard && myCard.value != 0)
            {
                BlackJackManager.DoIllegalAction(100);
                Debug.Log("ILLEGAL ACTION");
            }
            DeckManager.PlayerCards[owner].Add(newCard);
        }
        GetComponent<CardRepresentation>().card = newCard;
    }
}
