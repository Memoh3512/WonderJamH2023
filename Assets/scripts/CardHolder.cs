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

    public bool active = false;
    public bool required = false;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        cardRep = GetComponent<CardRepresentation>();
        originialColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.75f);
        hoverColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        hoveredCards = new List<GameObject>();
    }
    public void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    public void OnMouseDown()
    {
        cardRep.card = null;
        foreach(GameObject c in hoveredCards)
        {
            CardClickController ccc = c.GetComponent<CardClickController>();
            ccc.inHolder = false;
            ccc.OnMouseDown();
        }

    }

    public void OnMouseExit()
    {
        sr.color = originialColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Card")
        {
            collision.GetComponent<CardClickController>().holder = gameObject;
            hoveredCards.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Card")
        {
            collision.GetComponent<CardClickController>().holder = null;
            hoveredCards.Remove(collision.gameObject);
        }
    }

    public void SwapCard(Card newCard)
    {
        if (owner == null)
        {
            Debug.LogError("T CON OWNER NULL OLDER");
        }
        DeckManager.PlayerCards[owner].Remove(GetComponent<CardRepresentation>().card); //TODO maybe si ca casse c'est la
        GetComponent<CardRepresentation>().card = newCard;
        DeckManager.PlayerCards[owner].Add(newCard);
    }
}
