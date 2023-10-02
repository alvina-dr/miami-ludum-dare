using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weapon_Sword : Weapon
{
    #region Methods
    public override void Setup()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalRotate(new Vector3(0, transform.localRotation.y + data.angle, 0), .2f).OnComplete(() =>
        {
            if (mesh != null) mesh.gameObject.SetActive(false);
        }));
        sequence.Append(transform.DOLocalRotate(new Vector3(0, transform.localRotation.y + data.angle, 0), .01f));
        sequence.AppendInterval(GPCtrl.Instance.upgradeSave.swordFrequency);
        sequence.AppendCallback(() =>
        {
            if (mesh != null) mesh.gameObject.SetActive(true);
        });
        sequence.SetLoops(-1, LoopType.Restart);
    }
    #endregion
}
