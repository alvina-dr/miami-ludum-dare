using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponRotateAnimation : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalRotate(new Vector3(transform.localRotation.x, 360, transform.localRotation.z), .4f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo);

    }
}
