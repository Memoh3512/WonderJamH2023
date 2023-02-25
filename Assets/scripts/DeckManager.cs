using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager
{
    public static List<Card> OgDeck;
    public static List<Card> Deck;

    public static Dictionary<JackPlayer, List<Card>> PlayerCards;

    public static void Reset()
    {
        if (OgDeck == null) OgDeck = new List<Card>();
        if (Deck == null) Deck = new List<Card>();
        if (PlayerCards == null) PlayerCards = new Dictionary<JackPlayer, List<Card>>();
        PlayerCards.Clear();
        foreach (JackPlayer player in GameObject.FindObjectsOfType<JackPlayer>())
        {
            Debug.Log($"Add {player.name} To PlayerCards");
            PlayerCards.Add(player, new List<Card>());
        }
        Deck.Clear();
    }

    public static Card DrawCard()
    {
        if (Deck.Count == 0) ResetDeck();
        int cardIndex = Random.Range(0, Deck.Count - 1);
        if (cardIndex < 0 || cardIndex >= Deck.Count)
        {
            Debug.LogError("CARD DRAW OUT OF RANGE! T CON CONAR");
        }
        Card cardToDraw = Deck[cardIndex];
        Deck.RemoveAt(cardIndex);
        return cardToDraw;
    }

    public static void ResetDeck()
    {
        Deck = new List<Card>(OgDeck);
        PlayerCards.Clear();
    }

    public static List<Card> GetCardsForPlayer(JackPlayer player)
    {
        if (PlayerCards.ContainsKey(player)) return PlayerCards[player];
        return new List<Card>();
    }

    public static List<Card> GetAllCardsOnTable()
    {
        List<Card> cardsOnTable = new List<Card>();
        foreach (List<Card> cardList in PlayerCards.Values)
        {
            cardsOnTable.AddRange(cardList);
        }

        return cardsOnTable;
    }

    public static List<Card> GetDealerCards()
    {
        foreach (KeyValuePair<JackPlayer, List<Card>> elem in PlayerCards)
        {
            if (!(elem.Key is AIJackPlayer))
            {
                return elem.Value;
            }
        }
        return new List<Card>();
    }
}
