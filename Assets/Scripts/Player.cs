using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    #region Movement
    Vector3 _direction;
    float _speed;
    float _sprint;
    float _jumpHeight;
    bool _isHighJump;
    float horizontalInput;
    float verticalInput;
    public float rotationSpeed;
    Transform camTransform;
    Vector3 previousLocation;
    #endregion

    #region Rotation after mouse
    //Rigidbody _rbPlayer;
    //float _sensitivityX;
    //float _sensitivityY;
    //float rotX;
    //float rotY;
    //Vector3 _targetRotation; 
    //Vector3 _startRotation;
    #endregion
    #region
    [SerializeField] Transform _spawnWeaponPoint;
    ObjManager manager;
    float _health;
    float _guard;

    public float Health { get => _health; set => _health = value; }
    public float Guard { get => _guard; set => _guard = value; }
    #endregion
    private void Awake()
    {
        #region Movement
        rotationSpeed = 720f;
        _speed = 5f;
        _sprint = 1f;
        _jumpHeight = 1f;
        _isHighJump = false;
        #endregion
        #region Rotation after mouse
        //_sensitivityX = 2f;
        //_sensitivityY = 2f;
        //_rbPlayer = GetComponent<Rigidbody>();
        #endregion
        _health = 10f;
        _guard = 0f;
        manager = FindObjectOfType<ObjManager>();
        camTransform = FindObjectOfType<CamController>().transform;
    }
    void Start()
    {
    }

    void Update()
    {
        CheckSprint();
        CheckJumpHeight(); //сейчас не зависит от логики
        Jump();
        Fire();
    }

    void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        _direction = new Vector3(horizontalInput, 0f, verticalInput);
        //_direction += camTransform.rotation.eulerAngles;
        transform.Translate(_direction.normalized * _speed * _sprint * Time.fixedDeltaTime, Space.World);//этим задаем вектор движения
        if (_direction != Vector3.zero)//если движемся
        {
            Quaternion toRotation = Quaternion.LookRotation(_direction, Vector3.up);//получаем кватернион куда поворачиваться
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);//этим задаем поворот в сторону движения
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

    private void LookAround()
    {
        //transform.LookAt(Input.mousePosition * _sensitivity);
        //transform.rotation = Quaternion.LookRotation(Input.mousePosition * _sensitivity);
        //snowman.transform.RotateAround(snowPos.rotation.y, 10);
        //transform.Rotate(Input.GetAxis("Mouse Y") * _sensitivityY, Input.GetAxis("Mouse X") * _sensitivityX, 0);
        //rotX += Input.GetAxis("Mouse X") * _sensitivityX * Time.fixedDeltaTime;
        //rotY += Input.GetAxis("Mouse Y") * _sensitivityY * Time.fixedDeltaTime;
        //rotY = Mathf.Clamp(rotY, -90f, 90f);
        //_rbPlayer.MoveRotation(Quaternion.Euler(0f, rotX, 0f));
        //transform.localRotation = Quaternion.Euler(-rotY, 0f, 0f);

        //rotX += Input.GetAxis("Mouse X") * _sensitivityX;
        //rotY += Input.GetAxis("Mouse Y") * _sensitivityY;
        //transform.eulerAngles = new Vector3(rotY, rotX, 0f);


        //rotX += Input.mousePosition.x;
        //rotY += Input.mousePosition.y;
        //Vector3 rotate = new Vector3(0f, rotY, 0f);
        //transform.eulerAngles = rotate * _sensitivityX * Time.fixedDeltaTime;

        //_targetRotation += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 10f);
        //_startRotation = transform.rotation.eulerAngles;
        //transform.rotation = Quaternion.Euler(_targetRotation - _startRotation);


        //_targetRotation = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 10f);
        //Vector3 direction = _targetRotation - transform.position;
        //Vector3 stepDirection = Vector3.RotateTowards(transform.forward, direction, _sensitivityX * Time.fixedDeltaTime, 0f);
        //transform.rotation = Quaternion.LookRotation(stepDirection);

        //float ang = Vector3.Angle(transform.position, Input.mousePosition + Vector3.forward * 10f);
        //Vector3 newPosition = new Vector3(0f, ang, 0f);
        //transform.eulerAngles = newPosition * _sensitivityX;

        //Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        //float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);
        //transform.rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));
    }
    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
