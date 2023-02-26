using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : CardHolder
{
    private CardClickController topCard;
    public override void OnMouseDown()
    {
        if(topCard == null)
        {
            if(CardClickController.heldCard != null)
            {
                topCard = CardClickController.heldCard;
                CardClickController.heldCard.PutDownCard(gameObject);
                topCard.gameObject.SetActive(false);
            }
            else
            {
                Card newCard = DeckManager.DrawCard();
                GameObject card = Instantiate<GameObject>(Resources.Load<GameObject>("Card"));
                card.GetComponent<CardClickController>().Init();
                card.GetComponent<CardClickController>().side1.sprite = Resources.Load<Sprite>("BackCard");
                card.GetComponent<CardClickController>().side2.sprite = newCard.sprite;
                card.GetComponent<CardClickController>().cardRep.card = newCard;
                card.GetComponent<CardClickController>().PickUpCard();
            }
        }
        else
        {
            if(CardClickController.heldCard == null)
            {
                topCard.gameObject.SetActive(true);
                topCard.PickUpCard();
                topCard = null;
            }
        }
    }
}
