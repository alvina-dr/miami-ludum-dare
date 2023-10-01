using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weapon : MonoBehaviour
{
    public WeaponData data;
    public GameObject mesh;

    #region Methods
    public virtual void Setup()
    {

    }

    #endregion

    void Start()
    {
        Setup();
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy _enemy = other.GetComponent<Enemy>();
        if (_enemy != null)
        {
            _enemy.Damage(data.damage);
            CinemachineShake.Instance.ShakeCamera(3, .1f);
        }
    }
}
