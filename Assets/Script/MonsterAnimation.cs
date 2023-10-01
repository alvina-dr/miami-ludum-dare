using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterAnimation : MonoBehaviour
{
    void Start()
    {
        transform.DOLocalMoveY(.3f, .4f).SetLoops(-1, LoopType.Yoyo);
    }
}
