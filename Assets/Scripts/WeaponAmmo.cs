using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
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

    protected void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
    }
    protected void OnCollisionEnter(Collision collision)
    {
        bool t = collision.gameObject.GetComponent<Player>() != null;
        //Debug.Log(t);
        Debug.Log(TryGetComponent(out _target)); //не находит родительский класс, почему?

        if (TryGetComponent(out _target))
        {
            DoDamage();
            Destroy(gameObject);
        }
    }

    private void DoDamage()
    {
        _target.Guard -= _damage;
        if (_target.Guard < 0)
        {
            _target.Health += _target.Guard;
            _target.Guard = 0;
        }
    }
}
