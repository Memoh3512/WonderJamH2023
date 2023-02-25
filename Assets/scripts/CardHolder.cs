using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    Color originialColor;
    Color hoverColor;
    SpriteRenderer sr;
    List<GameObject> hoveredCards;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
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
}
