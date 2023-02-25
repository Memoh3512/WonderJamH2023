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
        AIJackPlayer[] playersQueried = FindObjectsOfType<AIJackPlayer>();
        GameOngoing = true;
        yield return null;

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
