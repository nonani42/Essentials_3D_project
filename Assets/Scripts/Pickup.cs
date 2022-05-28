using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected Animation _anim;
    protected float _amount;
    protected Tags _tag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            _anim.GetClip("PickupDestroy");
            other.GetComponent<Player>().ChangeValues(_tag, _amount);
            Destroy(gameObject);
        }
    }
}
