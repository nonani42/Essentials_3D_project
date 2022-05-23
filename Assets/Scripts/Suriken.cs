using UnityEngine;

public class Suriken : WeaponAmmo
{
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _lifespan = 3f;
        _speed = 20f;
        _rotationspeed = 300000f;
        _rotationVector = Vector3.up;
        _damage = 4f;
    }
}