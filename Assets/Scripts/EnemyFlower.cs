using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlower : Enemy
{


    private void Awake()
    {
        _tag = Tags.enemyFlower;
        _spawnWeaponPoint = FindPointByName(transform, "WeaponPoint");
        _headPoint = FindPointByName(transform, "Head");
        _hasPatrol = true;
        manager = FindObjectOfType<ObjManager>();
        _player = ObjManager.FindPlayer();
        _agent = GetComponent<NavMeshAgent>();
        Health = 15f;
        Guard = 0f;
        _agent.speed = 4f;
        _rotationSpeed = 100f;

        _hasTarget = false;
        _fireRate = 0.5f;

        _waypointIndex = 0;

        _visionAngle = 90f;

    }

    private void Start()
    {
        FindPatrolPoints(); //в Awake не переносить, _index не успевает присвоиться
    }

    private void Update()
    {
        Die();
    }

    private void FixedUpdate()
    {
        if (_hasTarget && _player != null)
        {
            //NoticeTarget();
            Stop();
            Aim();
            Fire();
        }
        else
        {
            Walk();
        }
    }
}
