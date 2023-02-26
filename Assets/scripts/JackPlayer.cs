using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JackPlayer : MonoBehaviour
{
    public List<CardHolder> holders;
    public HoldersManager holderManager;
    public int money;
    
    protected UnityEvent OnCardAskComplete;
    
    public virtual void RemoveRoundListeners()
    {
        OnCardAskComplete.RemoveAllListeners();
    }

    public void RemoveCardAskListener()
    {
        OnCardAskComplete.RemoveAllListeners();
    }
    
    public void AskForCards(int cardNb)
    {
        if (holderManager == null) Debug.LogError("T CON HOLDERMANAGER NULL DANS ASKFORCARDS");
        holderManager.AskForCards(cardNb);
    }

    public void FireCardAskEnd()
    {
        OnCardAskComplete.Invoke();
    }
    
    public void AddOnCardAskCompleteListener(UnityAction action)
    {
        if (OnCardAskComplete == null) OnCardAskComplete = new UnityEvent();
        OnCardAskComplete.AddListener(action);
    }

    public int HandValue(List<Card> hand)
    {
        List<int> values = new List<int>();
        foreach (Card card in hand)
        {
            values.Add(card.value);
        }

        int score = 0;
        int nb_as = 0;
        int nb_as_un = 0;
        int nb_as_onze = 0;

        foreach (int value in values)
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

}
