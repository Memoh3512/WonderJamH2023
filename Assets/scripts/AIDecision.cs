using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecision: MonoBehaviour
{
    //Remplacer par get Card plus tard
    List<Card> ai_cards = new List<Card>();
    List<Card> table_cards = new List<Card>();
    Card dealer_card = new Card();
    bool intelligent = true//Pour savoir si y pige de façon intelligente ou pas

    List<int> ai_values = CardValues(ai_cards);
    List<int> table_values = CardValues(table_cards);

    int score = HandValue(ai_values);

    

       
    
    public bool Pige() 
    {
        bool pige = false;
        if (score < 21)
        {
            if (intelligent)
            {
                pige = SmartPigeCheck(score, table_values, dealer_card);
            }
            else
            {
                //pige = DumbPigeChecl()
            }
        }
        return pige;
            
    }
    
    
    public bool SmartPigeCheck(int score, List<int> table_values, Card dealer_card)
    {
        //Créer le nombre de carte de chaque value (index 0 = nombre de value 2)
        int[] all_cards = new int[] { 16, 16, 16, 16, 16, 16, 16, 16, 64, 16 };
        int[] remaining_cards = all_cards;
        bool pige = false;
        foreach (int value in table_values)
        {
            remaining_cards[value - 2] -= 1;
        }
        int nb_usefull_card = 0;
        for (int i = 0; i < 9; i++)
        {
            if ((score + i + 2) =< 21)
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
        float odds_of_usefull_card = nb_usefull_card / nb_remaining_card;
        if (dealer_card.value => 10){
            if (odds_of_usefull_card => 0.4)
            {
                pige = true;
            }
        }
        else
        {
            if (odds_of_usefull_card => 0.5)
            {
                pige = true;
            }
        }
        return pige;
    }
    


 
    
        
      
      

        
        



    pubic int HandValue(List<int> hand)
    {
        int score = 0;

        foreach (int value in hand)
        {
            if (value == 11 && score + value > 21)
            {
                value = 1;
            }

            score += value;
        }

        return score;
    }

    





    public List<int> CardValues(List<Card> cards)
    {
        List<int> values = new List<int>;
        foreach (Card card in cards)
        {
            values.Add(card.value);
        }
        return (values);
    }






}


