using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOpenSetupMusic : MonoBehaviour
{
    private static bool gameOpened = false;

    private void Start()
    {
        if (!gameOpened)
        {
            SoundPlayer.instance.SetMusic(Songs.intro);
            gameOpened = true;
        }
    }
}
