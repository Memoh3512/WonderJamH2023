using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardRepresentation))]
public class CardHolder : MonoBehaviour
{
    Color originialColor;
    Color hoverColor;
    SpriteRenderer sr;
    List<GameObject> hoveredCards;
    private CardRepresentation cardRep;
    public JackPlayer owner;

    public int holderNumber;
    public HoldersManager holdersManager;

    public CardClickController heldCard;

    public bool active = false;
    public bool required = false;
    public bool hovered = false;
    public bool clicked = false;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        cardRep = GetComponent<CardRepresentation>();
        originialColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.75f);
        hoverColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        hoveredCards = new List<GameObject>();
    }


    public void InitManager(int holderNumber, HoldersManager holdersManager)
    {
        this.holderNumber = holderNumber;
        this.holdersManager = holdersManager;
    }

    public void OnMouseDown()
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
            heldCard.PickUpCard();
            heldCard = null;
            holdersManager.CardRemoved();
        }
        else
        {
            CardClickController temp = CardClickController.heldCard;
            CardClickController.heldCard.PutDownCard(gameObject);
            heldCard.PickUpCard();
            heldCard = temp;
        }
       /* clicked = true;
        cardRep.card = null;
        if (hoveredCards.Count == 2) CardClickController.handFull = false;
        foreach(GameObject c in hoveredCards)
        {
            CardClickController ccc = c.GetComponent<CardClickController>();
            ccc.inHolder = false;
            ccc.OnMouseDown();
        }
        if (hoveredCards.Count == 1)
        {
            if (CardClickController.handFull) holdersManager.CardRemoved();
            else holdersManager.CardAdded();
        }
        clicked = false;*/

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
        hovered = false;
    }
    public void OnMouseEnter()
    {
        sr.color = hoverColor;
        if (CardClickController.heldCard != null)
            CardClickController.heldCard.holder = gameObject;
        hovered = true;
    }


    public void SwapCard(Card newCard)
    {
        if (owner != null)
        {
           // Debug.LogError("T CON OWNER NULL OLDER");
            DeckManager.PlayerCards[owner].Remove(GetComponent<CardRepresentation>().card);
            DeckManager.PlayerCards[owner].Add(newCard);
        }
        //TODO maybe si ca casse c'est la
        GetComponent<CardRepresentation>().card = newCard;
    }
}
