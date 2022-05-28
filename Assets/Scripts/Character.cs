using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour

{
    protected float health;
    protected float guard;
    private float maxHealth;
    private float maxGuard;


    public float Health { get => health; set => health = value; }
    public float Guard { get => guard; set => guard = value; }
    protected float MaxHealth { get => maxHealth; set => maxHealth = value; }
    protected float MaxGuard { get => maxGuard; set => maxGuard = value; }

    protected virtual void Die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
