using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlower : Character
{
    [SerializeField] private Transform _spawnWeaponPoint;
    private Transform[] _waypoints;
    private ObjManager manager;
    private GameObject _player;

    private bool _hasTarget;
    private Vector3 _direction;
    private Vector3 _stepDirection;

    private float _speed;

    private float _rotationSpeed;
    private float _timeToNextShot;
    private float _fireRate;

    private int _waypointIndex;


    private void Awake()
    {
        manager = FindObjectOfType<ObjManager>();
        _player = ObjManager.FindPlayer();
        Health = 10f;
        Guard = 0f;
        _speed = 5f;
        _rotationSpeed = 100f;

        _hasTarget = false;
        _fireRate = 1f;

        _waypointIndex = 0;
        FindPatrolPoints();
    }

    private void FindPatrolPoints()
    {
        Transform[] temp;
        temp = GameObject.Find("Waypoints").GetComponentsInChildren<Transform>();
        _waypoints = new Transform[temp.Length - 1];
        for(int i = 1; i< temp.Length; i++)
        {
            _waypoints[i - 1] = temp[i];
        }
        Debug.Log(_waypoints.Length);
    }

    private void Start()
    {
    }

    private void Update()
    {
        Die();
    }

    private void FixedUpdate()
    {
        if (_hasTarget)
        {
            Aim();
            Fire();
        } else
        {
            Walk();
        }
    }

    private void Walk()
    {
        if(_waypointIndex >= _waypoints.Length)
        {
            _waypointIndex = 0;
        }
        _direction = _waypoints[_waypointIndex].position - transform.position;
        transform.Translate(_direction.normalized * _speed * Time.fixedDeltaTime); //этим задаем вектор движения
        if (_direction != Vector3.zero)//если движемся
        {
            Quaternion toRotation = Quaternion.LookRotation(_direction, Vector3.up); //получаем кватернион куда поворачиваться
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.fixedDeltaTime); //этим задаем поворот в сторону движения
        }
        _waypointIndex++;
        //if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        //{
        //    _waypointIndex = (_waypointIndex + 1) % _waypoints.Length;
        //    navMeshAgent.SetDestination(_waypoints[_waypointIndex].position);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _hasTarget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _hasTarget = false;
        }
    }

    private void Aim()
    {
        _direction = _player.transform.position - transform.position;
        _stepDirection = Vector3.RotateTowards(transform.forward, _direction, _rotationSpeed * Time.fixedDeltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(_stepDirection);
    }

    private void Fire()
    {
        _timeToNextShot -= Time.deltaTime;
        if (_timeToNextShot <= 0)
        {
            _timeToNextShot = 1 / _fireRate;
            manager.Spawn(Tags.suriken, _spawnWeaponPoint);
        }
    }
}
