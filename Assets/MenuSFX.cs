using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSFX : MonoBehaviour
{
    public void PlayClickSFX()
    {
        
        SoundPlayer.instance.PlaySFX("sfx/Menu click");
    }
}
