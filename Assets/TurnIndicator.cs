using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{

    static TurnIndicator instance;
    // Start is called before the first frame update
    void Start()
    {
       instance = this; 
    }
    
    static void SetText(string text)
    {
        if (instance != null)
        {
            instance.GetComponent<TextMeshProUGUI>().text = text;
        }
    }
    
}
