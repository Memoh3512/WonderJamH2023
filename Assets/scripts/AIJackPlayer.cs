using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIJackPlayer : JackPlayer
{
    public float suspicion;
    public float distractionLevel;

    public bool lost = false;

    public FacialExpressionManager expressionManager;
    public HandGestureManager handGestureManager;

    private void Start()
    {
        expressionManager = transform.GetComponentInChildren<FacialExpressionManager>();
        handGestureManager = transform.GetComponentInChildren<HandGestureManager>();

        StartCoroutine(testHitMiss());

    }

    IEnumerator testHitMiss()
    {
        handGestureManager.HitGesture();

        yield return new WaitForSeconds(3);
        
        handGestureManager.HoldGesture();

        yield return new WaitForSeconds(3);
        
        handGestureManager.HitGesture();
    }

    public void Bet(int amount)
    {
        money -= amount;
        if (money < 0) Lose();
    }

    public void Decide()
    {
        //TODO decider si hit or miss
        handGestureManager.HitGesture();
    }

    public void Lose()
    {
        lost = true;
    }

    #region Events
    protected UnityEvent OnBetEnd;
    protected UnityEvent OnDrawCardEnd;
    protected UnityEvent<JackDecision> OnDecideEnd;
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

    public void AddOnDecideEndListener(UnityAction<JackDecision> action)
    {
        if (OnDecideEnd == null) OnDecideEnd = new UnityEvent<JackDecision>();
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
