using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;

public class AIJackPlayer : JackPlayer
{
    public float suspicion;
    public float distractionLevel;
    [Range(0.1f, 10)]
    public float attentiveness = 1;
    public bool intel = true;
    public AIDecision aiDecision;
    
    public bool lost = false;
    public bool lostRound = false;
    private bool holds = false;

    public FacialExpressionManager expressionManager;
    public HandGestureManager handGestureManager;
    public TokenPile tokenPile;

    public void Start()
    {
        aiDecision = new AIDecision(intel);
        expressionManager = transform.GetComponentInChildren<FacialExpressionManager>();
        handGestureManager = transform.GetComponentInChildren<HandGestureManager>();

        //StartCoroutine(testHitMiss());
        StartCoroutine(SuspicionCooldown());
    }

    public void Bet(int amount)
    {
        expressionManager.StressedExpression();
        StartCoroutine(BetRoutine(amount));
    }

    IEnumerator BetRoutine(int amount)
    {
        yield return new WaitForSeconds(Random.Range(1, 4));
        money -= amount;
        tokenPile.Bet();
        if (money < 0) Lose();
        SoundPlayer.instance.PlaySFX("sfx/Deplacement jeton");
        expressionManager.HappyExpression();
        OnBetEnd.Invoke();
    }

    public void Decide()
    {
        int handvalue = HandValue();
        Debug.Log($"{name} HAS {handvalue}");
        if (handvalue == 21)
        {
            StartCoroutine(BlackJackRoutine());
        }
        else if (handvalue > 21)
        {
            StartCoroutine(LostRoutine());
        }
        else
        {
            StartCoroutine(DecideRoutine());
        }
    }

    IEnumerator BlackJackRoutine()
    {
        expressionManager.HappyExpression();
        yield return new WaitForSeconds(2);
        handGestureManager.HoldGesture();
        OnDecideEnd.Invoke(JackDecision.Hold);

    }

    IEnumerator LostRoutine()
    {
        expressionManager.AngryExpression();
        LoseRound();
        yield return new WaitForSeconds(Random.Range(1.0f,2.0f));
        handGestureManager.HoldGesture();
        yield return new WaitForSeconds(1);
        OnDecideEnd.Invoke(JackDecision.Hold);

    }

    IEnumerator DecideRoutine()
    {
        if (holds)
        {
            yield return new WaitForSeconds(1);
            handGestureManager.HoldGesture();
            yield return new WaitForSeconds(1);
            OnDecideEnd.Invoke(JackDecision.Hold);
            yield break;
        }
        //TODO SFX Hummmmm
        expressionManager.StressedExpression();
        yield return new WaitForSeconds(Random.Range(10, 20));
        //TODO SFX Haha!
        Card dealerCard = DeckManager.GetDealerCards()[0];
        if (dealerCard == null) Debug.LogError("DEALER A PAS DE CARTE WTFF");
        bool hit = aiDecision.Pige(DeckManager.GetCardsForPlayer(this), DeckManager.GetAllCardsOnTable(), dealerCard); //table_hand inclut la main du joueur et du dealer
        JackDecision decision = hit ? JackDecision.Hit : JackDecision.Hold;
        switch (decision)
        {
            case JackDecision.Hit:
                handGestureManager.HitGesture();
                break;
            case JackDecision.Hold:
                holds = true;
                handGestureManager.HoldGesture();
                break;
        }
        yield return new WaitForSeconds(1);
        OnDecideEnd.Invoke(decision);
    }

    public void Lose()
    {
        lost = true;
        expressionManager.SetFace(FaceType.sad);
    }

    public void LoseRound()
    {
        expressionManager.SetFace(FaceType.sad);
        lostRound = true;
    }

    public void NewRound()
    {
        if (lost) return;
        expressionManager.SetFace(FaceType.neutral);
        lostRound = false;
        holds = false;
        tokenPile.Reset();
    }

    public void WitnessIllegalAction(float actionValue)
    {
        
        suspicion += actionValue + (100 - distractionLevel);
        if (suspicion > 100)
        {
            suspicion = 100;
        }

        if (suspicion >= 100)
        {
            //Debug.Log("SUS = 100, LOSE");
            BlackJackManager.GameEnd(false);
        }

        if (suspicion > 25)
        {
            expressionManager.StressedExpression();
        } else if (suspicion > 50)
        {
            expressionManager.SusExpression();
        } else if (suspicion > 75)
        {
            expressionManager.AngryExpression();
        }
    }

    IEnumerator SuspicionCooldown()
    {
        while (!lost)
        {
            if (suspicion > 0)
            {
                suspicion -= 2*(1.0f/attentiveness);
            }
            suspicion = Mathf.Clamp(suspicion, 0, 100);
            yield return new WaitForSeconds(1);
        }
    }

    #region Events
    protected UnityEvent OnBetEnd;
    protected UnityEvent<JackDecision> OnDecideEnd;
    protected UnityEvent OnLose;

    public void AddOnBetEndListener(UnityAction action)
    {
        if (OnBetEnd == null) OnBetEnd = new UnityEvent();
        OnBetEnd.AddListener(action);
    }

    public void AddOnDecideEndListener(UnityAction<JackDecision> action)
    {
        if (OnDecideEnd == null) OnDecideEnd = new UnityEvent<JackDecision>();
        OnDecideEnd.AddListener(action);
    }
    
    public void RemoveDecideEndListener()
    {
        OnDecideEnd.RemoveAllListeners();
    }

    public override void RemoveRoundListeners()
    {
        base.RemoveRoundListeners();
        OnBetEnd.RemoveAllListeners();
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
