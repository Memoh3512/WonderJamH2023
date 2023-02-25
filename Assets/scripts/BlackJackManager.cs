using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJackManager
{
    IEnumerator GameRoutine()
    {
        yield return new WaitForSeconds(0);
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
}
