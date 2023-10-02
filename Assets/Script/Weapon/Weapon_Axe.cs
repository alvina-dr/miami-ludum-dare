using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weapon_Axe : Weapon
{
    #region Methods
    public override void Setup()
    {
        transform.DOKill();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalRotate(new Vector3(0, transform.localRotation.y + data.angle, 0), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (mesh != null) mesh.gameObject.SetActive(false);
        }));
        //sequence.Append(transform.DOLocalScale(0, 0), .01f));
        sequence.AppendInterval(GPCtrl.Instance.upgradeSave.axeFrequency);
        sequence.AppendCallback(() =>
        {
            if (mesh != null) mesh.gameObject.SetActive(true);
        });
        sequence.SetLoops(-1, LoopType.Restart);
    }
    #endregion
}
