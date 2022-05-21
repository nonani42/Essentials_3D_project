using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    GameObject _player;
    float _radius;
    float angleX;
    float angleY;

    private void Awake()
    {
        _radius = 7f;
        angleX = 180;
        angleY = -40;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Start()
    {
        _player = ObjManager.FindPlayer();
    }
    private void LateUpdate()
    {
        if(_player != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        angleX += Input.GetAxis("Mouse X");
        angleY = Mathf.Clamp(angleY -= Input.GetAxis("Mouse Y"), -60, -10);
        _radius = Mathf.Clamp(_radius -= Input.mouseScrollDelta.y, 3, 10);
        angleX = angleX > 360 ? angleX -= 360 : angleX < 0 ? angleX += 360: angleX;
        Vector3 orbit = Vector3.forward * _radius;
        orbit = Quaternion.Euler(angleY, angleX, 0) * orbit;
        transform.position = _player.transform.position + orbit;
        transform.LookAt(_player.transform.position);
    }
}
