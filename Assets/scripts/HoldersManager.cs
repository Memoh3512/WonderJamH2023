using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldersManager : MonoBehaviour
{
    List<GameObject> holders;
    int currentHolderIndex = 0;
    int cardCount = 0;
    
    //ask cards
    private int askedHolderIndex = 0;

    [SerializeField]
    public JackPlayer owner;

    void Start()
    {        
        owner.holderManager = this;
        GetHolders();
    }

    private void GetHolders()
    {
        holders = new List<GameObject>();
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            holders.Add(gameObject.transform.Find("Holder (" + i + ")").gameObject);
            holders[i].SetActive(false);
            holders[i].GetComponent<CardHolder>().InitManager(i, this);
            holders[i].GetComponent<CardHolder>().SetOwner(owner);

        }
    }

    public void AskForCards(int cardNb)
    {

        askedHolderIndex = currentHolderIndex + cardNb;
        EnableNextHolder(false);

    }

    public void CardAdded()
    {
        
        SoundPlayer.instance.PlaySFX("sfx/Drop card");
        if (currentHolderIndex >= askedHolderIndex)
        {
            owner.FireCardAskEnd();
            cardCount++;
        }
        else
        {
            EnableNextHolder(false);
            cardCount++;
        }
    }

    public void CardRemoved()
    {
        if (cardCount != currentHolderIndex)
            DisableLastHolder();

        cardCount--;
        SoundPlayer.instance.PlaySFX("sfx/Pickup card");
    }

    public void EnableNextHolder(bool required)
    {
        if (currentHolderIndex >= holders.Count) return;
        holders[currentHolderIndex].SetActive(true);
        holders[currentHolderIndex].GetComponent<CardHolder>().Activate(required);
        currentHolderIndex++;
    }

    public void DisableLastHolder()
    {
        currentHolderIndex--;
        holders[currentHolderIndex].SetActive(false);
        holders[currentHolderIndex].GetComponent<CardHolder>().Deactivate();
    }

}
