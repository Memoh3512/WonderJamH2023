using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldersManager : MonoBehaviour
{
    List<GameObject> holders;
    int currentHolderIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetHolders();
        EnableNextHolder(false);
    }

    private void GetHolders()
    {
        holders = new List<GameObject>();
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            holders.Add(gameObject.transform.Find("Holder (" + i + ")").gameObject);
            holders[i].SetActive(false);
            holders[i].GetComponent<CardHolder>().InitManager(i, this);

        }
    }

    public void CardAdded()
    {
        EnableNextHolder(false);
    }

    public void CardRemoved()
    {
        DisableLastHolder();
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
