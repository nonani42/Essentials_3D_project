using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    private void Awake()
    {
        Guard = 10f;
        Health = 10f;
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
