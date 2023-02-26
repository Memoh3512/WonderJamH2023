using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FortuneWheel : MonoBehaviour
{
    public GameObject FortuneWheelGameObject;
    private Animator FortuneWheelAnimator;
    public Sprite[] bad_results;
    private bool is_running = false;
    
    // Start is called before the first frame update
    void Start()
    {
        FortuneWheelAnimator = FortuneWheelGameObject.GetComponent<Animator>();
        FortuneWheelAnimator.enabled = false;
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
            Debug.Log("Calling StartRoll(CasinoMachine) while running");
            yield return null;
        }
        //ARTIFICIAL
        
        is_running = true;
        FortuneWheelAnimator.enabled = true;
        
        SoundPlayer.instance.PlaySFX("sfx/Fortune wheel rolling");
        FortuneWheelAnimator.Play("Rolling");
        FortuneWheelAnimator.SetTrigger("Roll");
        FortuneWheelAnimator.ResetTrigger("Roll");
        
        yield return new WaitForSeconds(2);
        int random = Random.Range(0, 10);
        if (random<2)
        {
            
            SoundPlayer.instance.PlaySFX("sfx/Fortune wheel win");
            //Debug.Log("Winner");
            //WINNER
            FortuneWheelAnimator.SetTrigger("Winner");
            
            yield return new WaitForSeconds(3);
            //machine.GetComponent<Animator>().enabled = false;
            FortuneWheelAnimator.ResetTrigger("Winner");
            FortuneWheelAnimator.enabled = false;
            BlackJackManager.DistractAll(40);
        }
        else
        {
            //Debug.Log("NOPE");
            //NOPPERS
            FortuneWheelAnimator.enabled = false;
            Sprite chosenResult = bad_results[Random.Range(0, bad_results.Length)];
            FortuneWheelGameObject.GetComponent<SpriteRenderer>().sprite = chosenResult;
        }
        
        yield return new WaitForSeconds(2);
        is_running = false;
    }
}
