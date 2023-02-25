using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JackPlayer : MonoBehaviour
{
    public List<Card> hand;
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
        //TODO
    }
    
    public void AddOnCardAskCompleteListener(UnityAction action)
    {
        if (OnCardAskComplete == null) OnCardAskComplete = new UnityEvent();
        OnCardAskComplete.AddListener(action);
    }
}
