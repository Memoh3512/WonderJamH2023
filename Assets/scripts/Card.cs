using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardObject", menuName = "Cards/New Card", order = 1)]
public class Card : ScriptableObject
{
    [field: SerializeField] private int value;
    [field: SerializeField] private Sprite sprite;
}
