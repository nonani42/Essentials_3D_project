using UnityEngine;

public class Snowball : WeaponAmmo
{
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _lifespan = 3f;
        _speed = 20f;
        _rotationspeed = 300000f;
        _rotationVector = Vector3.right;
        _damage = 2f;
    }
}