using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecision //: MonoBehaviour
{
    //Remplacer par get Card plus tard
    public List<Card> ai_cards;
    public List<Card> table_cards;
    public Card dealer_card;
    public bool intelligent;//Pour savoir si y pige de fa?on intelligente ou pas
    public List<int> ai_values;
    public List<int> table_values;
    public int score;
    
    public AIDecision(bool intelligent)
    {
        
        this.intelligent = intelligent;
       
    }
   
    /*public void Start()
    {
        intelligent = true;
        List<Card> hand = new List<Card>();
        List<Card> table_hand = new List<Card>();
        Card dealer_hand = new Card(2, null);
        hand.Add(new Card(6, null));
        hand.Add(new Card(6, null));
        table_hand.Add(new Card(4, null));
        table_hand.Add(new Card(7, null));
        table_hand.Add(new Card(2, null));
        table_hand.Add(new Card(8, null));
        table_hand.Add(new Card(10, null));
        table_hand.Add(new Card(10, null));
        table_hand.Add(new Card(7, null));
        table_hand.Add(new Card(10, null));
        table_hand.Add(new Card(5, null));
        table_hand.Add(new Card(11, null));
        Debug.Log(Pige(hand, table_hand, dealer_hand));


    }*/

    public bool Pige(List<Card> ai_cards, List<Card> table_cards, Card dealer_card) 
    {
        this.ai_cards = ai_cards;
        this.table_cards = table_cards;
        this.dealer_card = dealer_card;
        ai_values = CardValues(ai_cards);
        table_values = CardValues(table_cards);
        score = HandValue(ai_values);

        bool pige = false;
        if (score < 21)
        {
           pige = PigeCheck(score,ai_values, table_values, dealer_card, intelligent);
            
        }
        return pige;
            
    }
    
    
    public bool PigeCheck(int score,List<int> hand_values,List<int> table_values, Card dealer_card, bool intel)
    {
        //Cr?er le nombre de carte de chaque value (index 0 = nombre de value 2)
        int[] all_cards = new int[] { 16, 16, 16, 16, 16, 16, 16, 16, 64, 16 };
        int[] remaining_cards = all_cards;
        bool pige = false;

        if (intel)
        {
            foreach (int value in table_values)
            {
                remaining_cards[value - 2] -= 1;
            }
        }
        else
        {
            foreach (int value in hand_values)
            {
                remaining_cards[value - 2] -= 1;
            }
        }
        
        int nb_usefull_card = 0;
        for (int i = 0; i < 9; i++)
        {
            if ((score + i + 2) <= 21)
            {
                nb_usefull_card += remaining_cards[i];
            }
        }
        //On ajoute les as parce que si on a pas 21 les as vont toujours nous aider
        nb_usefull_card += remaining_cards[9];
        
        int nb_remaining_card = 0;
        for (int i = 0; i < remaining_cards.Length; i++)
        {
            nb_remaining_card += remaining_cards[i];
        }
        float odds_of_usefull_card = (float)nb_usefull_card / (float)nb_remaining_card;
        
        float rng = Random.Range(0f, 1);

        if (dealer_card.value >= 10){

            odds_of_usefull_card = odds_of_usefull_card * 1.1f;

            if (odds_of_usefull_card > 1)
            {
                odds_of_usefull_card = Mathf.Floor(odds_of_usefull_card);
            }
        }
        if (intel)
        {
            pige = (odds_of_usefull_card >=rng);
        }
        else
        {
            pige = (Mathf.Pow(odds_of_usefull_card,2) >= rng);
        }
        
        return pige;
    }

    public int HandValue(List<int> hand)
    {
       
        int score = 0;
        int nb_as = 0;
        int nb_as_un = 0;
        int nb_as_onze = 0;

        foreach (int value in hand)
        {
            if (value == 11)

            {
                nb_as += 1;
            }
            if (value == 11 && score + value > 21)
            {
                score += 1;
                nb_as_un += 1;
            }
            else
            {
                score += value;
            }
        }
        nb_as_onze = nb_as - nb_as_un;
        

        if (nb_as_onze != 0)
        {
           while (score > 21 && nb_as_onze != 0)
            {
                score -= 10;
                nb_as_onze -= 1;
            }
            
        }
        
        return score;
    }

    public List<int> CardValues(List<Card> cards)
    {
        List<int> values = new List<int>();
        foreach (Card card in cards)
        {
            values.Add(card.value);
        }
        return (values);
    }
}


