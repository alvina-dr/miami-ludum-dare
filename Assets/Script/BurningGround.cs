using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningGround : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.GetComponent<Player>();
        if (_player != null)
        {
            Debug.Log("PLAYER SHOULD BURN");
            _player.Damage(_player.getHealth());
            _player.Death();
        }
    }
}
