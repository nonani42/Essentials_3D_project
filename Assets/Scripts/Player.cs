using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void Awake()
    {
        Health = 10f;
        Guard = 0f;
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
}
