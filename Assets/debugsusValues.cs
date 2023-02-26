using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class debugsusValues : MonoBehaviour
{
    private static bool debug = true;

    public AIJackPlayer owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (debug && owner != null)
        {
            GetComponent<TextMeshProUGUI>().text = $"sus: {owner.suspicion}\ndistraction: {owner.distractionLevel}";
        }
    }
}
