using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecision
{
    //Remplacer par get Card plus tard
    public List<Card> ai_cards;
    public List<Card> table_cards;
    public Card dealer_card;
    public bool intelligent;//Pour savoir si y pige de façon intelligente ou pas
    public List<int> ai_values;
    public List<int> table_values;
    public int score;
    
    public AIDecision(bool intelligent)
    {
        
        this.intelligent = intelligent;
       
    }
   
    
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
            if (odds_of_usefull_card >= rng * 0.8 )
            {
                pige = true;
            }
        }
        else
        {
            if (odds_of_usefull_card >= rng)
            {
                pige = true;
            }
        }
        return pige;
    }
    


 
    
        
      
      

        
        



    public int HandValue(List<int> hand)
    {
       
        int score = 0;
        int nb_as = 0;

        foreach (int value in hand)
        {
            if (value == 11)
            {
                nb_as += 1;
            }
            if (value == 11 && score + value > 21)
            {
                score += 1;
            }
            else
            {
                score += value;
            }

        
        }
        while (score > 21 && nb_as != 0)
        {
            score -= 10;
            nb_as -= 1;
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


