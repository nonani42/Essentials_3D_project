using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _spawnWeaponPoint;
    ObjManager manager;
    Transform camTransform;

    float _speed;
    float _sprint;
    float _jumpHeight;
    bool _isHighJump;

    Vector3 _direction;
    Vector3 _camPosition;
    Vector3 _camDirection;
    Vector3 _horizontalInput;
    Vector3 _verticalInput;

    public float rotationSpeed;

    private void Awake()
    {
        manager = FindObjectOfType<ObjManager>();
        camTransform = FindObjectOfType<CamController>().transform;
        rotationSpeed = 720f;
        _speed = 5f;
        _sprint = 1f;
        _jumpHeight = 1f;
        _isHighJump = false;
    }
    public void Update()
    {
        Fire();
        Jump();
    }
    public void FixedUpdate()
    {
        Walk();
        CheckSprint();
        CheckJumpHeight(); //сейчас не зависит от логики
    }

    private void Walk()
    {
        _camPosition = new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z); //без учета высоты камеры
        _camDirection = transform.position - _camPosition; //убираем наклон игрока из-за наклона камеры
        _horizontalInput = camTransform.right * Input.GetAxis("Horizontal");
        _verticalInput = _camDirection * Input.GetAxis("Vertical");
        _direction = _horizontalInput + _verticalInput;
        transform.Translate(_direction.normalized * _speed * _sprint * Time.fixedDeltaTime, Space.World); //этим задаем вектор движения
        if (_direction != Vector3.zero)//если движемся
        {
            Quaternion toRotation = Quaternion.LookRotation(_direction, Vector3.up); //получаем кватернион куда поворачиваться
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime); //этим задаем поворот в сторону движения
        }
    }

    public void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            manager.Spawn(Tags.snowball, _spawnWeaponPoint);
        }
    }

    private void CheckSprint()
    {
        _sprint = Input.GetButton("Sprint") ? 2f : 1f;
    }

    private void CheckJumpHeight()
    {
        _jumpHeight = _isHighJump ? 2f : 1f;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            transform.position += Vector3.up * _jumpHeight;
        }
    }
}
