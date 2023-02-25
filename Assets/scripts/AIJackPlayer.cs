using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIJackPlayer : JackPlayer
{
    public float suspicion;
    public float distractionLevel;
    public int tokens = 50;

    public bool lost = false;

    public void Bet(int amount)
    {
        tokens -= amount;
        if (tokens < 0) Lose();
    }

    public void Decide()
    {
        //TODO decider si hit or miss
    }

    public void Lose()
    {
        lost = true;
    }

    #region Events
    protected UnityEvent OnBetEnd;
    protected UnityEvent OnDrawCardEnd;
    protected UnityEvent OnDecideEnd;
    protected UnityEvent OnLose;

    public void AddOnBetEndListener(UnityAction action)
    {
        if (OnBetEnd == null) OnBetEnd = new UnityEvent();
        OnBetEnd.AddListener(action);
    }

    public void AddOnDrawCardEndListener(UnityAction action)
    {
        if (OnDrawCardEnd == null) OnDrawCardEnd = new UnityEvent();
        OnDrawCardEnd.AddListener(action);
    }

    public void AddOnDecideEndListener(UnityAction action)
    {
        if (OnDecideEnd == null) OnDecideEnd = new UnityEvent();
        OnDecideEnd.AddListener(action);
    }

    public override void RemoveRoundListeners()
    {
        base.RemoveRoundListeners();
        OnBetEnd.RemoveAllListeners();
        OnDrawCardEnd.RemoveAllListeners();
        OnDecideEnd.RemoveAllListeners();
        OnCardAskComplete.RemoveAllListeners();
    }
    
    public void AddOnLoseListener(UnityAction action)
    {
        if (OnLose == null) OnLose = new UnityEvent();
        OnLose.AddListener(action);
    }
    #endregion

}
