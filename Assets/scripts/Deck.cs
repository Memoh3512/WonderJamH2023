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
                //Give new generated card to CardClickController.heldCard

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
