using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInitializer : MonoBehaviour
{
    public int nbDecks = 4;
    public List<Card> allCards;
    // Start is called before the first frame update
    void Start()
    {
        if (allCards == null) return;
        DeckManager.ResetDeck();
        for (int i = 0; i < nbDecks; i++)
        {
            foreach (Card card in allCards)
            {
                DeckManager.OgDeck.Add(new Card(card.value, card.sprite));
            }
        }
        DeckManager.Deck.AddRange(DeckManager.OgDeck);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
