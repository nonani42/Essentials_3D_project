using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    float _lifespan;
    float _speed;

    private void Awake()
    {
        _lifespan = 3f;
        _speed = 15f;
    }


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
    }
}
