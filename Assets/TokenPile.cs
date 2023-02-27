using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenPile : MonoBehaviour
{

    public GameObject betPrefab;

    public Vector3 tokenOffset;

    private GameObject betToken;
        
    public Sprite bigToken;

    public Sprite mediumToken;

    public Sprite smallToken;

    private SpriteRenderer sr;

    public JackPlayer owner;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (owner == null)
        {
            Debug.LogError("OWNER NULL, DELETING");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (owner.money <= 0)
        {
            sr.sprite = null;
        } else if (owner.money < 30)
        {
            sr.sprite = smallToken;
        } else if (owner.money < 70)
        {
            sr.sprite = mediumToken;
        }
        else
        {
            sr.sprite = bigToken;
        }
    }

    public void Bet()
    {
        betToken = Instantiate(betPrefab);
        betToken.transform.position = transform.position + tokenOffset;
        betToken.GetComponent<SpriteRenderer>().sprite = smallToken;

    }

    public void Reset()
    {
        if (betToken) Destroy(betToken);
    }
}
