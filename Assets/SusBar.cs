using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SusBar : MonoBehaviour
{
    float startY;
    float endY = 4.1f;
    public float lerpSpeed;
    public AIJackPlayer player;
    void Start()
    {
        startY = transform.localPosition.y;
    }

    void Update()
    {
        
        float yValue = startY + ((endY - startY) * (player.suspicion / 100f));
        Vector3 target = new Vector3(transform.localPosition.x, yValue, transform.localPosition.z);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, lerpSpeed*Time.deltaTime);
    }
}
