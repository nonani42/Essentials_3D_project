using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject _bridge;
    [SerializeField] ParticleSystem _particleSys;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            _bridge.SetActive(true);
            _particleSys.Stop();
        }
    }
}
