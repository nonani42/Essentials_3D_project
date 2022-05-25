using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Animator _anim;

    private void Awake()
    {
        Guard = 10f;
        Health = 10f;
        _anim = GetComponent<Animator>();
    }
    void Start()
    {
    }

    void Update()
    {
        Die();
    }

    void FixedUpdate()
    {
    }
    protected override void Die()
    {
        if (health <= 0)
        {
            _anim.SetTrigger("Dead");
            Debug.Log("Snowman dead");
            gameObject.GetComponent<PlayerController>().enabled = false;
            gameObject.GetComponent<Player>().enabled = false;
            //_anim.enabled = false;
        }
    }
}
