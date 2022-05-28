using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pause;
    [SerializeField] GameObject _camera;

    PlayerController _player;
    CamController _camController;

    void Start()
    {
        _player = ObjManager.FindPlayer().GetComponent<PlayerController>();
        _camController = _camera.GetComponent<CamController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !_pause.activeSelf)
        {
            _pause.SetActive(true);

        } else if(Input.GetButtonDown("Cancel") && _pause.activeSelf)
        {
            _pause.SetActive(false);
        }
        if (_pause.activeSelf)
        {
            SetCursor();
            SetControl();
        }
        else
        {
            SetCursor();
            SetControl();
        }
    }

    private void SetControl()
    {
        _player.enabled = !_pause.activeSelf;
        _camController.enabled = !_pause.activeSelf;
    }

    void SetCursor()
    {
        if(_pause.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Cursor.visible = _pause.activeSelf;
    }

}
