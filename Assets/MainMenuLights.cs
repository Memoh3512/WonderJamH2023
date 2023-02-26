using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuLights : MonoBehaviour
{
    public List<GameObject> left_lights;
    public List<GameObject> right_lights;
    public Animator Titre;
    private bool is_running = false;
    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < right_lights.Count && i < left_lights.Count; i++)
        {
            right_lights[i].GetComponent<Animator>().enabled = false;
            left_lights[i].GetComponent<Animator>().enabled = false;
        }
        Titre.enabled = false;
    }
    void Start()
    {
        
        StartCoroutine(FlashLights());
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    
    IEnumerator FlashLights()
    {
        if (is_running)
        {
            Debug.Log("Calling StartRoll(CasinoMachine) while running");
            yield return null;
        }
        
        is_running = true;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < right_lights.Count && i < left_lights.Count; i++)
        {
            right_lights[i].GetComponent<Animator>().enabled = true;
            left_lights[i].GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(0.3f);
        }
        Titre.enabled = true;
        
        is_running = false;
    }
}
