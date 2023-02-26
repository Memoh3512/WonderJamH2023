using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : CardHolder
{
    private Stack<CardClickController> topCards = new();
    public override void OnMouseDown()
    {
        if(CardClickController.heldCard != null)
        {
            topCards.Push(CardClickController.heldCard);
            topCards.Peek().PutDownCard(gameObject, true);
            topCards.Peek().gameObject.SetActive(false);
        }
        else
        {
            if(topCards.Count == 0)
            {
                Card newCard = DeckManager.DrawCard();
                GameObject card = Instantiate<GameObject>(Resources.Load<GameObject>("Card"));
                card.GetComponent<CardClickController>().Init();
                card.GetComponent<CardClickController>().side1.sprite = Resources.Load<Sprite>("BackCard");
                card.GetComponent<CardClickController>().side2.sprite = newCard.sprite;
                card.GetComponent<CardClickController>().cardRep.card = newCard;
                card.GetComponent<CardClickController>().PickUpCard();
            }
            else
            {
                CardClickController newCard = topCards.Pop();
                newCard.gameObject.SetActive(true);
                newCard.PickUpCard();
                newCard.flipCard();
            }  
        }
    }
}
