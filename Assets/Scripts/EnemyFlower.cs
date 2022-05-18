using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlower : MonoBehaviour, ICharacter
{
    float _health;
    float _guard;
    float _speed;
    ObjManager manager;
    [SerializeField] Transform _spawnWeaponPoint;
    [SerializeField] GameObject[] _waypoints;

    public float Health { get => _health; set => _health = value; }
    public float Guard { get => _guard; set => _guard = value; }
    public float Speed { get => _speed; set => _speed = value; }

    public void Fire()//не реализовано условие
    {
        //if (false)
        //{
        //    manager.Spawn(Tags.snowball, _spawnWeaponPoint);
        //}
    }
    void Awake()
    {
        _health = 10f;
        _guard = 0f;
        _speed = 5f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }
}
