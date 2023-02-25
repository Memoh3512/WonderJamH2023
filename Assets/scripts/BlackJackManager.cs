using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJackManager
{
    IEnumerator GameRoutine()
    {
        yield return new WaitForSeconds(0);
    }

    public static void Distract(JackPlayer player, float distractionValue)
    {
        //distraction
    }

    public static void DistractAll(float distractionValue = 0)
    {
    }
}
