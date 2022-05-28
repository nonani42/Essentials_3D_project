using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Pickup
{
    void Awake()
    {
        _tag = Tags.heal;
        _amount = 5f;
        _anim = GetComponent<Animation>();
    }
}
