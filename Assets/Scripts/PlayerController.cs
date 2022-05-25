using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _spawnWeaponPoint;
    private ObjManager manager;
    private Transform camTransform;
    private Rigidbody _rb;
    private Animator _anim;

    private float _speed;
    private float _sprint;
    private float _jumpHeight;

    private Vector3 _direction;
    private Vector3 _camPosition;
    private Vector3 _camDirection;
    private Vector3 _horizontalInput;
    private Vector3 _verticalInput;

    public float rotationSpeed;

    private void Awake()
    {
        manager = FindObjectOfType<ObjManager>();
        camTransform = FindObjectOfType<CamController>().transform;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        rotationSpeed = 720f;
        _speed = 5f;
        _sprint = 1f;
        _jumpHeight = 240f;
    }
    public void Update()
    {
        CheckFire();
        Jump();
    }
    public void FixedUpdate()
    {
        Walk();
        CheckSprint();
        //UseRaycast();
    }

    private void Walk()
    {
        if(_direction != Vector3.zero) 
        { 
            _anim.SetBool("isMove", true); 
        }
        else
        {
            _anim.SetBool("isMove", false);
        }
        _camPosition = new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z); //без учета высоты камеры
        _camDirection = transform.position - _camPosition; //убираем наклон игрока из-за наклона камеры
        _horizontalInput = camTransform.right * Input.GetAxis("Horizontal");
        _verticalInput = _camDirection * Input.GetAxis("Vertical");
        _direction = _horizontalInput + _verticalInput;
        _rb.MovePosition(transform.position + _direction.normalized * _speed * _sprint * Time.fixedDeltaTime);
        if (_direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(_direction * rotationSpeed * Time.fixedDeltaTime, Vector3.up);
            _rb.MoveRotation(toRotation);
            
        }
        #region Movement without Rigidbody
        //_camPosition = new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z); //без учета высоты камеры
        //_camDirection = transform.position - _camPosition; //убираем наклон игрока из-за наклона камеры
        //_horizontalInput = camTransform.right * Input.GetAxis("Horizontal");
        //_verticalInput = _camDirection * Input.GetAxis("Vertical");
        //_direction = _horizontalInput + _verticalInput;
        //transform.Translate(_direction.normalized * _speed * _sprint * Time.fixedDeltaTime, Space.World); //этим задаем вектор движения
        //if (_direction != Vector3.zero)//если движемся
        //{
        //    Quaternion toRotation = Quaternion.LookRotation(_direction, Vector3.up); //получаем кватернион куда поворачиваться
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime); //этим задаем поворот в сторону движения
        //}
        #endregion
    }

    public void CheckFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _anim.SetBool("Shoot", true);
        }
        else
        {
            _anim.SetBool("Shoot", false);
        }
    }

    private void Fire()
    {
        manager.Spawn(Tags.snowball, _spawnWeaponPoint);
    }

    private void CheckSprint()
    {
        _sprint = Input.GetButton("Sprint") ? 2f : 1f;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        }
        #region Jump without Rigidbody
        //if (Input.GetButtonDown("Jump"))
        //{
        //    transform.position += Vector3.up * _jumpHeight;
        //}
        #endregion
    }

    private void UseRaycast()
    {
        if (Physics.Raycast(_spawnWeaponPoint.position, transform.forward, out RaycastHit hit))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
