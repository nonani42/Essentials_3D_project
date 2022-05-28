using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Animator _anim;
    HUD _hud;

    float _snowballCount;

    public float SnowballCount { get => _snowballCount; set => _snowballCount = value; }

    private void Awake()
    {
        MaxGuard = 10f;
        MaxHealth = 10f;
        Guard = MaxGuard;
        Health = MaxHealth;
        _anim = GetComponent<Animator>();
        SnowballCount = 50;
        _hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

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
    public void ChangeValues(Tags tag, float amount)
    {
        switch (tag)
        {
            case Tags.heal:
                amount = CheckValue(Health, MaxHealth, amount);
                Health += amount;
                _hud.ChangeSliders();
                break;
            case Tags.shield:
                amount = CheckValue(Guard, MaxGuard, amount);
                Guard += amount;
                _hud.ChangeSliders();
                break;
            case Tags.spareAmmo:
                SnowballCount += amount;
                _hud.ChangeText();
                break;
            default:
                Debug.Log("No action for this tag");
                break;
        }
    }
    private float CheckValue(float _value, float _maxValue, float _change)
    {
        if((_value + _change) <= _maxValue)
        {
            return _change;
        } else 
        {
            return _maxValue - _value;
        }
    }
}
