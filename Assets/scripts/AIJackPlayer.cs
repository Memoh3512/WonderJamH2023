using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIJackPlayer : JackPlayer
{
    public float suspicion;
    public float distractionLevel;
    public bool intel = true;
    public AIDecision aiDecision;
    
    public bool lost = false;

    public FacialExpressionManager expressionManager;
    public HandGestureManager handGestureManager;

    public void Start()
    {
        aiDecision = new AIDecision(intel);
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
        StartCoroutine(BetRoutine(amount));
    }

    IEnumerator BetRoutine(int amount)
    {
        yield return new WaitForSeconds(Random.Range(1, 4));
        money -= amount;
        if (money < 0) Lose();
        //TODO SFX Bet
        OnBetEnd.Invoke();
    }

    public void Decide()
    {
        StartCoroutine(DecideRoutine());
    }

    IEnumerator DecideRoutine()
    {
        //TODO decider si hit or miss
        //TODO SFX Hummmmm
        yield return new WaitForSeconds(Random.Range(10, 20));
        //TODO SFX Haha!
        Card dealerCard = DeckManager.GetDealerCards()[0];
        if (dealerCard == null) Debug.LogError("DEALER A PAS DE CARTE WTFF");
        bool hit = aiDecision.Pige(hand, DeckManager.GetAllCardsOnTable(), dealerCard); //table_hand inclut la main du joueur et du dealer
        JackDecision decision = hit ? JackDecision.Hit : JackDecision.Hold;
        OnDecideEnd.Invoke(decision);
        
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
