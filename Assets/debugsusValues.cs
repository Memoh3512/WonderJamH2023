using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class debugsusValues : MonoBehaviour
{
    private static bool debug = false;

    public AIJackPlayer owner;

    // Update is called once per frame
    void Update()
    {
        if (debug && owner != null)
        {
            GetComponent<TextMeshProUGUI>().text = $"sus: {owner.suspicion}\ndistraction: {owner.distractionLevel}\nmoney: {owner.money}";
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
