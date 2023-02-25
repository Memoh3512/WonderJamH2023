using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Card
{
    [field: SerializeField] public int value;
    [field: SerializeField] public Sprite sprite;

    public Card(int _value, Sprite _sprite)
    {
        value = _value;
        sprite = _sprite;
    }
}
