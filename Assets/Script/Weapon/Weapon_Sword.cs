using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weapon_Sword : Weapon
{
    #region Methods
    public override void Setup()
    {
        transform.DOKill();
        transform.localEulerAngles = new Vector3(0, -GPCtrl.Instance.upgradeSave.swordRange / 2, 0);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalRotate(new Vector3(0, transform.localEulerAngles.y + GPCtrl.Instance.upgradeSave.swordRange, 0), .2f, RotateMode.FastBeyond360).OnComplete(() =>
        {
            if (mesh != null) mesh.gameObject.SetActive(false);
        }));
        sequence.Append(transform.DOLocalRotate(new Vector3(0, transform.localEulerAngles.y + GPCtrl.Instance.upgradeSave.swordRange, 0), .01f, RotateMode.FastBeyond360));
        sequence.AppendInterval(data.reloadTime);
        sequence.AppendCallback(() =>
        {
            if (mesh != null) mesh.gameObject.SetActive(true);
        });
        sequence.SetLoops(-1, LoopType.Restart);
    }
    #endregion
}
