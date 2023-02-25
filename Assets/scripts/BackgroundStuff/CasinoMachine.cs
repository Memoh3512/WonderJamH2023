using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CasinoMachine : MonoBehaviour
{
    public GameObject machine;
    public GameObject lever;
    public Sprite[] bad_results;
    private bool is_running = false;
    
    // Start is called before the first frame update
    void Start()
    {
        machine.GetComponent<Animator>().StopPlayback();
        lever.GetComponent<Animator>().StopPlayback();
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_running)
        {
            StartCoroutine(StartRoll());
        }
    }
    IEnumerator StartRoll()
    {
        if (is_running)
        {
            Debug.Log("Calling StartRoll while running");
            yield return null;
        }
        
        is_running = true;
        lever.GetComponent<Animator>().Play("PullBras",0,0f);
        yield return new WaitForSeconds(1);
        machine.GetComponent<Animator>().enabled = true;
        machine.GetComponent<Animator>().Play("Rolling");
        machine.GetComponent<Animator>().SetTrigger("Roll");
        machine.GetComponent<Animator>().ResetTrigger("Roll");
        yield return new WaitForSeconds(2);
        int random = Random.Range(0, 10);
        if (random<2)
        {
            //Debug.Log("Winner");
            //WINNER
            machine.GetComponent<Animator>().SetTrigger("Winner");
            
            yield return new WaitForSeconds(3);
            //machine.GetComponent<Animator>().enabled = false;
            machine.GetComponent<Animator>().ResetTrigger("Winner");
            machine.GetComponent<Animator>().enabled = false;
        }
        else
        {
            //Debug.Log("NOPE");
            //NOPPERS
            machine.GetComponent<Animator>().enabled = false;
            Sprite chosenResult = bad_results[Random.Range(0, bad_results.Length)];
            machine.GetComponent<SpriteRenderer>().sprite = chosenResult;
        }
        
        yield return new WaitForSeconds(2);
        is_running = false;
    }
}
