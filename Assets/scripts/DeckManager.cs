using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager
{
    public static List<Card> OgDeck;
    public static List<Card> Deck;
    public static List<Card> KnownCards;

    public static Dictionary<JackPlayer, List<Card>> PlayerCards;

    public void Reset()
    {
        if (Deck == null) Deck = new List<Card>();
        if (KnownCards == null) KnownCards = new List<Card>();
        if (PlayerCards == null) PlayerCards = new Dictionary<JackPlayer, List<Card>>();
        Deck.Clear();
        KnownCards.Clear();
        PlayerCards.Clear();
    }

    public static Card DrawCard(JackPlayer player)
    {
        if (Deck.Count == 0) ResetDeck();
        int cardIndex = Random.Range(0, Deck.Count - 1);
        if (cardIndex < 0 || cardIndex >= Deck.Count)
        {
            Debug.LogError("CARD DRAW OUT OF RANGE! T CON CONAR");
        }
        Card cardToDraw = Deck[cardIndex];
        Deck.RemoveAt(cardIndex);
        KnownCards.Add(cardToDraw);
        
        if (!PlayerCards.ContainsKey(player)) PlayerCards.Add(player, new List<Card>());
        PlayerCards[player].Add(cardToDraw);

        return cardToDraw;

    }

    public static void ResetDeck()
    {
        Deck = new List<Card>(OgDeck);
    }

    public static List<Card> GetCardsForPlayer(JackPlayer player)
    {
        if (PlayerCards.ContainsKey(player)) return PlayerCards[player];
        return new List<Card>();
    }
}
