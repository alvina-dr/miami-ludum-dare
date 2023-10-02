using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FX_Scale : MonoBehaviour
{
    public float biggerScale; 
    public float timer;
    void Start()
    {
        transform.DOScale(biggerScale, timer).SetLoops(-1, LoopType.Yoyo);
    }

}
