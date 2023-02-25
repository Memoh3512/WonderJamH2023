using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpressionManager : MonoBehaviour
{

    public AIJackPlayer jackPlayer;

    private SpriteRenderer faceRenderer;

    public Sprite neutralFace;

    public Sprite susFace;
    public Sprite stressedFace;
    public Sprite happyFace;
    public Sprite sadFace;
    public Sprite fiouFace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ExpressionUpdate()
    {
        yield return null;
    }

    public void SusExpression()
    {
        //TODO SFX Sus
        StopAllCoroutines();
        StartCoroutine(ExpressionRoutine(susFace));
    }

    public void StressedExpression()
    {
        //TODO SFX Stressed
        StopAllCoroutines();
        StartCoroutine(ExpressionRoutine(stressedFace));
    }

    public void HappyExpression()
    {
        //TODO SFX Happy
        StopAllCoroutines();
        StartCoroutine(ExpressionRoutine(happyFace));
    }

    public void SadExpression()
    {
        //TODO SFX Sad
        StopAllCoroutines();
        StartCoroutine(ExpressionRoutine(sadFace));
    }

    public void FiouExpression()
    {
        //TODO SFX Fiou
        StopAllCoroutines();
        StartCoroutine(ExpressionRoutine(fiouFace));
    }

    IEnumerator ExpressionRoutine(Sprite expression)
    {
        faceRenderer.sprite = expression;
        yield return new WaitForSeconds(Random.Range(2, 6));
        faceRenderer.sprite = neutralFace;
    }

}
