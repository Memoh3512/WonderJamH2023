using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public enum JackDecision
{
    Hit, Hold
}

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
        yield return new WaitForSeconds(2);
        List<AIJackPlayer> players = new List<AIJackPlayer>(FindObjectsOfType<AIJackPlayer>());
        GameObject selfObject = GameObject.FindGameObjectWithTag("Player");
        if (selfObject == null) Debug.LogError("SELFObject NULL DANS GAME ROUTINE T CON");
        JackPlayer self = selfObject.GetComponent<JackPlayer>();
        if (self == null)
        {
            Debug.LogError("SELF NULL DANS GAME ROUTINE T CON");
        }
        GameOngoing = true;

        //start of game loop
        int lostPlayers = 0;

        foreach (AIJackPlayer player in players)
        {        
            player.AddOnLoseListener(() => lostPlayers++);
        }

        int PlayersToLose = players.Count;
        while (lostPlayers < PlayersToLose)
        {
            //bet
            TurnIndicator.SetText("Mise");
            int waitAmount = 0;
            foreach (AIJackPlayer player in players)
            {
                player.Bet(10);
                player.AddOnBetEndListener(() =>
                {
                    waitAmount++;
                });
            }
            yield return new WaitUntil(() => waitAmount == players.Count);

            //draw basic cards
            TurnIndicator.SetText("Donner 2 cartes à chacun");
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
            yield return new WaitUntil(() => waitAmount == players.Count);
            
            //Draw my card
            TurnIndicator.SetText("Pige ta carte");
            waitAmount = 0;
            self.AskForCards(1);
            self.AddOnCardAskCompleteListener(() =>
            {
                waitAmount++;
                self.RemoveCardAskListener();
            });
            yield return new WaitUntil(() => waitAmount == 1);

            TurnIndicator.SetText("");
            int done = 0;
            while (done < players.Count)
            {

                yield return new WaitForSeconds(1);
                //decide
                TurnIndicator.SetText("Réflexion...");
                waitAmount = 0;
                Dictionary<AIJackPlayer, JackDecision> decisions = new Dictionary<AIJackPlayer, JackDecision>();
                foreach (AIJackPlayer player in players)
                {
                    player.Decide();
                    player.AddOnDecideEndListener((decision) =>
                    {
                        waitAmount++;
                        decisions.Add(player, decision);
                        player.RemoveDecideEndListener();
                    });
                }
                yield return new WaitUntil(() => waitAmount == players.Count);
            
                //reaction aux choix
                TurnIndicator.SetText("Donner des cartes à ceux qui en veulent");
                waitAmount = 0;
                int toWait = 0;
                foreach (KeyValuePair<AIJackPlayer, JackDecision> elem in decisions)
                {
                    if (elem.Value == JackDecision.Hit)
                    {
                        elem.Key.AskForCards(1);
                        toWait++;
                        elem.Key.AddOnCardAskCompleteListener(() =>
                        {
                            waitAmount++;
                            elem.Key.RemoveCardAskListener();
                        });
                    } else
                    {
                        done++;
                    }
                }

                yield return new WaitUntil(() => waitAmount == toWait);
            }
            yield return new WaitForSeconds(3);
            //croupier
            //if GetHandValue >= 17, fini le tour, sinon ask for card
            TurnIndicator.SetText("Pige ma carte");
            while (self.HandValue(DeckManager.GetCardsForPlayer(self)) < 17)
            {
                waitAmount = 0;
                self.AskForCards(1);
                self.AddOnCardAskCompleteListener(() =>
                {
                    waitAmount++;
                    self.RemoveCardAskListener();
                });
                yield return new WaitUntil(() => waitAmount == 1);
            }
            TurnIndicator.SetText("");
            
            //realisation
            TurnIndicator.SetText("Résultat");
            int selfHand = self.HandValue();
            List<AIJackPlayer> PlayersDead = new List<AIJackPlayer>();
            foreach (AIJackPlayer player in players)
            {
                int playerhand = player.HandValue();
                if (playerhand > 21 || (playerhand < selfHand && selfHand <= 21))
                {
                    //lose
                    player.money -= 10;
                    player.expressionManager.SadExpression();
                    if (player.money <= 0)
                    {
                        if (player.intel) PlayersDead.Add(player);
                        else GameEnd(false);
                    }

                }
                else if((playerhand <= 21 && playerhand > selfHand) || selfHand > 21)
                {
                    //win
                    player.money += 20;
                    player.expressionManager.HappyExpression();
                    if (!player.intel && player.money >= 100)
                    {
                        GameEnd(true);
                    }
                }
            }
            TurnIndicator.SetText("");
            yield return new WaitForSeconds(3);
            
            //end turn
            foreach (AIJackPlayer player in players)
            {
                player.RemoveRoundListeners();
                player.NewRound();
            }

            foreach (AIJackPlayer player in PlayersDead)
            {
                players.Remove(player);
            }
            //win si reste juste le MC
            if (players.Count == 1 && !players[0].intel)
            {
                GameEnd(true);
            }
            foreach (HoldersManager holder in FindObjectsOfType<HoldersManager>())
            {
                holder.ResetHolders();
            }
            DeckManager.ResetDeck();
        }
        GameOngoing = false;
        GameEnd(false);

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
        player.expressionManager.DistractedExpression();
        if (player.distractionLevel > 100)
        {
            player.distractionLevel = 100;
        }
        else
        {
            player.distractionLevel = distractionValue;
        }
    }

    public static void DistractAll(float distractionValue)
    {
        AIJackPlayer[] ais = FindObjectsOfType<AIJackPlayer>();
        foreach (AIJackPlayer player in ais)
        {
            Distract(player, distractionValue);
        }
    }

    public static void DoIllegalAction(float actionValue)
    {
        AIJackPlayer[] ais = FindObjectsOfType<AIJackPlayer>();
        foreach (AIJackPlayer player in ais)
        {
            Sussify(player, actionValue);
        }
    }

    public static void Sussify(AIJackPlayer player, float actionValue)
    {
        player.WitnessIllegalAction(actionValue);
    }

    public static void GameEnd(bool win)
    {
        SoundPlayer.instance.SetMusic(Songs.winlose, 2f, TransitionBehavior.Stop);
        if (win)
        {
            SceneChanger.ChangeScene(SceneTypes.WinScene);
        }
        else
        {
            
            SoundPlayer.instance.PlaySFX("sfx/Slime tabasser");
            SceneChanger.ChangeScene(SceneTypes.LoseScene);
        }
    }

}
