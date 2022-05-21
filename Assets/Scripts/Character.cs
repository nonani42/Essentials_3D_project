using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour

{
    protected float health;
    protected float guard;

    public float Health { get => health; set => health = value; }
    public float Guard { get => guard; set => guard = value; }

    protected void Die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
