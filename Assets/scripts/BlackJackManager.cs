using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class BlackJackManager : MonoBehaviour
{

    public static BlackJackManager instance;
    public static bool GameOngoing = false;
    
    private void Start()
    {
        instance = this;
        StartCoroutine(GameRoutine());
    }
    
    IEnumerator GameRoutine()
    {
        AIJackPlayer[] players = FindObjectsOfType<AIJackPlayer>();
        GameObject selfObject = GameObject.FindGameObjectWithTag("Player");
        if (selfObject == null) Debug.LogError("SELFObject NULL DANS GAME ROUTINE T CON");
        JackPlayer self = selfObject.GetComponent<JackPlayer>();
        if (self == null)
        {
            Debug.LogError("SELF NULL DANS GAME ROUTINE T CON");
        }
        GameOngoing = true;

        int lostPlayers = 0;

        while (lostPlayers < players.Length)
        {
            //bet
            int waitAmount = 0;
            foreach (AIJackPlayer player in players)
            {
                player.AddOnLoseListener(() => lostPlayers++);
                player.Bet(10);
                player.AddOnBetEndListener(() => waitAmount++);
            }
            yield return new WaitUntil(() => waitAmount == players.Length);

            //draw basic cards
            waitAmount = 0;
            foreach (AIJackPlayer player in players)
            {
                player.AskForCards(2);
                player.AddOnCardAskCompleteListener(() =>
                {
                    waitAmount++;
                    player.RemoveCardAskListener();
                });
            }
            yield return new WaitUntil(() => waitAmount == players.Length);
            
            //Draw my card
            waitAmount = 0;
            self.AskForCards(1);
            self.AddOnCardAskCompleteListener(() =>
            {
                waitAmount++;
                self.RemoveCardAskListener();
            });
            yield return new WaitUntil(() => waitAmount == 1);
            
            //decide
            waitAmount = 0;
            foreach (AIJackPlayer player in players)
            {
                player.Decide();
                player.AddOnDecideEndListener(() => waitAmount++);
            }
            yield return new WaitUntil(() => waitAmount == players.Length);
            
            //reaction aux choix
            
            //end turn
            foreach (AIJackPlayer player in players)
            {
                player.RemoveRoundListeners();
                //TODO call ramassage de cartes
            }
        }
        yield return null;

        GameOngoing = false;

    }

    public static void StartGlobalCoroutine(IEnumerator routine)
    {
        if (instance != null)
        {
            instance.StartCoroutine(routine);
        }
    }

    public static void Distract(AIJackPlayer player, float distractionValue)
    {
        //distraction
        player.distractionLevel = distractionValue;
    }

    public static void DistractAll(float distractionValue)
    {
        AIJackPlayer[] ais = Object.FindObjectsOfType<AIJackPlayer>();
        foreach (AIJackPlayer player in ais)
        {
            Distract(player, distractionValue);
        }
    }

    static void GameEnd()
    {
        
    }

}
