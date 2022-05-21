using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAmmo : MonoBehaviour
{
    protected float _lifespan;
    protected float _speed;
    protected float _damage;
    protected Character _target;


    protected void Start()
    {
        Destroy(gameObject, _lifespan);
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected void OnCollisionEnter(Collision collision)
    {
        _target = collision.gameObject.GetComponent<Character>();
        //collision.gameObject.GetType().BaseType.IsAssignableFrom(typeof(Character)) - не проверяет на наследование от базового класса, от любого коллайдера возвращает true
        if (_target != null) 
        {
            DoDamage();
            Destroy(gameObject);
        }
    }

    protected void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
    }

    protected void DoDamage()
    {
        _target.Guard -= _damage;
        if (_target.Guard < 0)
        {
            _target.Health += _target.Guard;
            _target.Guard = 0;
        }
        _target = null;
    }
}
