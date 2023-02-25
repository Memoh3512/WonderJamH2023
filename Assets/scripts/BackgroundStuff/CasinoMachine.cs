using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoMachine : MonoBehaviour
{
    public GameObject machine;
    public GameObject lever;
    
    // Start is called before the first frame update
    void Start()
    {
        machine.GetComponent<Animator>();
        StartRoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartRoll()
    {
        lever.GetComponent<Animator>().Play("Rolling");
    }
}
