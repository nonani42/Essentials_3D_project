using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlower : Character
{
    [SerializeField] private Transform _spawnWeaponPoint;
    private Transform[] _waypoints;
    private int _waypointIndex;
    private ObjManager manager;
    private GameObject _player;

    private Vector3 _direction;
    private Vector3 _stepDirection;
    private float _rotationSpeed;

    private float _speed;

    private bool _hasTarget;
    private float _timeToNextShot;
    private float _fireRate;



    private void Awake()
    {
        manager = FindObjectOfType<ObjManager>();
        _player = ObjManager.FindPlayer();
        Health = 15f;
        Guard = 0f;
        _speed = 4f;
        _rotationSpeed = 100f;

        _hasTarget = false;
        _fireRate = 0.5f;

        _waypointIndex = 0;
        FindPatrolPoints();
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
        if (_hasTarget && _player != null)
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
        transform.Translate(_direction.normalized * _speed * Time.fixedDeltaTime, Space.World); //этим задаем вектор движения
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _direction, _rotationSpeed * Time.fixedDeltaTime, 0f));
        if ((transform.position - _waypoints[_waypointIndex].position).sqrMagnitude < 1f)
        {
            _waypointIndex++;
        }
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

    private void FindPatrolPoints()
    {
        Transform[] temp;
        temp = GameObject.Find("Waypoints").GetComponentsInChildren<Transform>();
        _waypoints = new Transform[temp.Length - 1];
        for(int i = 1; i< temp.Length; i++)
        {
            _waypoints[i - 1] = temp[i];
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
