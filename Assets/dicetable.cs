using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dicetable : MonoBehaviour
{
    public void PlayDiceSFX()
    {
        
        SoundPlayer.instance.PlaySFX("sfx/Roll dice");
    }
}
