using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAmmo : MonoBehaviour
{
    protected float _lifespan;
    protected float _speed;
    protected float _rotationspeed;
    protected Vector3 _rotationVector;
    protected float _damage;
    protected Character _target;
    protected Rigidbody _rb;
    protected HUD _hud;


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
        //попробовать проверку наследования прописать так: Debug.Log(interactiveObject is GoodBonus);
        //collision.gameObject.GetType().BaseType.IsAssignableFrom(typeof(Character)) - не проверяет на наследование от базового класса, от любого коллайдера возвращает true
        if (_target != null) 
        {
            DoDamage();
            if (collision.gameObject.GetComponent<Player>())
            {
                _hud.ChangeSliders();
            }
        }
        LeaveImpact();
    }

    protected void Move()
    {
        _rb.AddForce(transform.forward * _speed);
        transform.GetChild(0).Rotate(_rotationVector * _rotationspeed, Space.Self);
        //transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime, Space.Self); //движение без Rigidbody
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

    protected void LeaveImpact()
    {
        //дописать эффект: при коллизии с полом, стенами и т.д. оставлять след
        Destroy(gameObject);
    }
}
