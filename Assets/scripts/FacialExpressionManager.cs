using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FaceType
{
    sad, neutral
}
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
    public Sprite angryFace;
    public Sprite distractedFace;

    private Sprite faceNormal;
    // Start is called before the first frame update
    void Start()
    {
        faceRenderer = GetComponent<SpriteRenderer>();
        faceNormal = neutralFace;
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
        
        SoundPlayer.instance.PlaySFX("sfx/Sus");
        StopAllCoroutines();
        StartCoroutine(ExpressionRoutine(susFace));
    }

    public void StressedExpression()
    {
        
        SoundPlayer.instance.PlaySFX("sfx/exp_Stressed");
        StopAllCoroutines();
        StartCoroutine(ExpressionRoutine(stressedFace));
    }

    public void HappyExpression()
    {
        
        SoundPlayer.instance.PlaySFX("sfx/exp_Happy");
        StopAllCoroutines();
        StartCoroutine(ExpressionRoutine(happyFace));
    }

    public void SadExpression()
    {
        
        SoundPlayer.instance.PlaySFX("sfx/exp_Sad");
        StopAllCoroutines();
        StartCoroutine(ExpressionRoutine(sadFace));
    }

    public void FiouExpression()
    {
        SoundPlayer.instance.PlaySFX("sfx/exp_Fiou");
        StopAllCoroutines();
        StartCoroutine(ExpressionRoutine(fiouFace));
    }

    public void AngryExpression()
    {
        SoundPlayer.instance.PlaySFX("sfx/Perso fache");
        StopAllCoroutines();
        if (angryFace != null) StartCoroutine(ExpressionRoutine(angryFace));
        else StartCoroutine(ExpressionRoutine(susFace));
    }

    public void DistractedExpression()
    {
        
        SoundPlayer.instance.PlaySFX("sfx/exp_Distracted");
        StopAllCoroutines();
        if (distractedFace != null) StartCoroutine(ExpressionRoutine(distractedFace));
        else StartCoroutine(ExpressionRoutine(susFace));
    }

    public void SetFace(FaceType type)
    {
        switch (type)
        {
            case FaceType.sad:
                SoundPlayer.instance.PlaySFX("sfx/exp_Sad");
                faceRenderer.sprite = sadFace;
                faceNormal = sadFace;
                break;
            case FaceType.neutral:
                faceRenderer.sprite = neutralFace;
                faceNormal = neutralFace;
                break;
        }
    }

    IEnumerator ExpressionRoutine(Sprite expression)
    {
        if (jackPlayer.lost) yield break;
        faceRenderer.sprite = expression;
        yield return new WaitForSeconds(Random.Range(2, 6));
        faceRenderer.sprite = faceNormal;
    }

}
