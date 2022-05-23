using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Character
{
    protected Transform _spawnWeaponPoint;
    protected Transform[] _waypoints;
    protected int _waypointIndex;
    protected GameObject _player;
    protected ObjManager manager;
    protected Tags _tag;
    protected int _index;


    protected Vector3 _direction;
    protected Vector3 _stepDirection;
    protected float _rotationSpeed;

    protected NavMeshAgent _agent;
    protected bool _hasPatrol;

    protected bool _hasTarget;
    protected float _timeToNextShot;
    protected float _fireRate;

    protected Transform _headPoint;
    protected float _visionAngle;

    public int Index {set => _index = value; }

    protected void Stop()
    {
        _agent.isStopped = true;
    }

    protected void Walk()
    {
        _agent.isStopped = false;
        if (_agent.remainingDistance < _agent.stoppingDistance)
        {
            _waypointIndex = (_waypointIndex + 1) % _waypoints.Length;
            _agent.SetDestination(_waypoints[_waypointIndex].position);
        }
        #region Movement without NavMeshAgent
        //if (_waypointIndex >= _waypoints.Length)
        //{
        //    _waypointIndex = 0;
        //}
        //_direction = _waypoints[_waypointIndex].position - transform.position;
        //transform.Translate(_direction.normalized * _speed * Time.fixedDeltaTime, Space.World); //этим задаем вектор движения
        //transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _direction, _rotationSpeed * Time.fixedDeltaTime, 0f));
        //if ((transform.position - _waypoints[_waypointIndex].position).sqrMagnitude < 1f)
        //{
        //    _waypointIndex++;
        //}
        #endregion
    }

    protected void Aim()
    {
        _direction = _player.transform.position - transform.position;
        _stepDirection = Vector3.RotateTowards(transform.forward, _direction, _rotationSpeed * Time.fixedDeltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(_stepDirection);
    }

    protected void Fire()
    {
        _timeToNextShot -= Time.deltaTime;
        if (_timeToNextShot <= 0)
        {
            _timeToNextShot = 1 / _fireRate;
            manager.Spawn(Tags.suriken, _spawnWeaponPoint);
        }

    }

    protected void FindPatrolPoints()
    {
        if (_hasPatrol)
        {
            _waypoints = manager.GetPatrolPoints(_tag, _index);
        }
    }

    protected Transform FindPointByName(Transform parent, string name)
    {
        Transform result = null;
        if (parent.name.Equals(name)) return parent;
        foreach (Transform child in parent)
        {
            result = FindPointByName(child, name);
            if (result != null) return result;
        }
        return result;
    }

    protected void NoticeTarget() //дописать метод, работает неправильно
    {
        float _angle = Vector3.Angle(transform.forward, _player.transform.position);
        Debug.Log("_angle: " + _angle);
        if (_angle <= _visionAngle && _angle >= -_visionAngle) 
        {
            Stop();
            Aim();
            Physics.Raycast(_headPoint.position, _headPoint.forward, out RaycastHit hit);
            if (hit.collider.gameObject == _player)
            {
                Fire();
            }
        }

        //установить угол зрения в 45 градусов, например, и делать проверку
        //если угол между вектором вперед и вектором на игрока меньше или равен этому углу, то атаковать (добавить условие на расстояние?)
        //триггер можно будет сделать побольше и использовать как дополнительное условие (или убрать?)
        //при этом проверить, что он не начинает атаковать, даже если выполнены предыдущие условия, если игрок за стеной (Raycast?), т.е. он его не видит
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _hasTarget = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _hasTarget = false;
        }
    }

}