using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinksEvent : JackEvent
{
    public AnimationCurve walkingXCurve;
    public AnimationCurve walkingYCurve;
    public Vector3 posInitiale = GameObject.FindGameObjectWithTag("DrinkGuySpawn").transform.position;
    public AnimationCurve leavingXCurve;
    public AnimationCurve leavingYCurve;
    private float timeElapsed;
    public float animationTime = 7;
    public float leavingTime = 4;
    public Vector3 posGlass1;
    public Vector3 posGlass2;
    public Vector3 posGlass3;
    private bool glassOnTable = false;
    private bool firstTime = true;
    GameObject[] glassList = new GameObject[3];
    GameObject[] drinkPossibilities = new GameObject[4];
    
    
       
    

   
    public void Distract(GameObject waiter)
    {
        BlackJackManager.StartGlobalCoroutine(Distraction(waiter));
    }

    IEnumerator Distraction(GameObject waiter)
    {
        float posX = posInitiale.x;
        
        while (timeElapsed < animationTime)
        {
            yield return null;
           

            timeElapsed += Time.deltaTime;
            waiter.transform.position = new Vector3( walkingXCurve.Evaluate(timeElapsed), walkingYCurve.Evaluate(timeElapsed), posInitiale.z);
        }
        posX = -4;

        BlackJackManager.DistractAll(60);
        yield return new WaitForSeconds(1);
        if (firstTime)
        {

            waiter.GetComponent<Animator>().StartPlayback();
            int firstGlass = Random.Range(0,3);
                GameObject glass1 = GameObject.Instantiate(drinkPossibilities[firstGlass]);
                
                SoundPlayer.instance.PlaySFX("sfx/Verre sur table");
                glassList[0] = glass1;
                glass1.transform.position = posGlass1;
                yield return new WaitForSeconds(1);
                int secondGlass = Random.Range(0, 3);
                GameObject glass2 = GameObject.Instantiate(drinkPossibilities[secondGlass]);
              
                SoundPlayer.instance.PlaySFX("sfx/Verre sur table");
                glassList[1] = glass2;
                glass2.transform.position = posGlass2;
                yield return new WaitForSeconds(1);
                int thirdGlass = Random.Range(0, 3);
                GameObject glass3 = GameObject.Instantiate(drinkPossibilities[thirdGlass]);
              
                SoundPlayer.instance.PlaySFX("sfx/Verre sur table");
                glassList[2] = glass3;
                glass3.transform.position = posGlass3;
                yield return new WaitForSeconds(1);
                firstTime = false;
                glassOnTable = true;

            
        }
        else
        {
            waiter.GetComponent<Animator>().StartPlayback();
            glassOnTable = !glassOnTable;
            glassList[0].SetActive(glassOnTable);
           
            SoundPlayer.instance.PlaySFX("sfx/Verre sur table");
            yield return new WaitForSeconds(1);
            glassList[1].SetActive(glassOnTable);
            SoundPlayer.instance.PlaySFX("sfx/Verre sur table");
            yield return new WaitForSeconds(1);
            glassList[2].SetActive(glassOnTable);
           
            SoundPlayer.instance.PlaySFX("sfx/Verre sur table");
            yield return new WaitForSeconds(1);

        }
        
        timeElapsed = 0;
        waiter.GetComponent<SpriteRenderer>().flipX = true;
        
        waiter.GetComponent<Animator>().StopPlayback();
        while (timeElapsed < leavingTime)
        {
            yield return null;
            timeElapsed += Time.deltaTime;
            waiter.transform.position = new Vector3(leavingXCurve.Evaluate(timeElapsed), leavingYCurve.Evaluate(timeElapsed), posInitiale.z);
        }
        
        //Debug.Log("destroy");

        BlackJackManager.DistractAll(0);
        EventEnded();
    }
    public override void EventEnded()
    {
        if (onEventEnded == null) return;
        onEventEnded.Invoke();
    }
    public override void ExecuteEvent()
    {
        posGlass1 = GameObject.FindGameObjectWithTag("Glass1Spawn").transform.position;
        posGlass2 = GameObject.FindGameObjectWithTag("Glass2Spawn").transform.position;
        posGlass3 = GameObject.FindGameObjectWithTag("Glass3Spawn").transform.position;


        walkingXCurve = Resources.Load<AnimationCurveAsset>("waiterCurve");
        walkingYCurve = Resources.Load<AnimationCurveAsset>("YWaiterCurve");
        leavingXCurve = Resources.Load<AnimationCurveAsset>("leavingWaiterX");
        leavingYCurve = Resources.Load<AnimationCurveAsset>("leavingWaiterY");

        GameObject waiterPrefab = Resources.Load<GameObject>("Waiter");
        drinkPossibilities[0] = Resources.Load<GameObject>("wine");
        drinkPossibilities[1] = Resources.Load<GameObject>("Champagne");        
        drinkPossibilities[2] = Resources.Load<GameObject>("Bottle");
        drinkPossibilities[3] = Resources.Load<GameObject>("Cocktail");
        

        if (waiterPrefab != null)
        {
            GameObject waiter = GameObject.Instantiate(waiterPrefab);
            waiter.transform.position = posInitiale;
           
            Distract(waiter);

        }
    }
}
