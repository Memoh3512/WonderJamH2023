using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTable : MonoBehaviour
{
    public Animator DieTableAnimator;
    private bool is_running = false;
    // Start is called before the first frame update
    void Start()
    {
        DieTableAnimator.enabled = false;
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
            Debug.Log("Calling StartRoll(DieTable) while running");
            yield return null;
        }
        
        is_running = true;
        DieTableAnimator.enabled = is_running;
        
        DieTableAnimator.Play("RollDices",0,0f);
        yield return new WaitForSeconds(3);
        
        is_running = false;
        DieTableAnimator.enabled = is_running;
    }
}
