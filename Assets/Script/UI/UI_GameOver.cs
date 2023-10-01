using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_GameOver : MonoBehaviour
{
    public void OpenMenu()
    {
        transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            transform.DOScale(1f, .2f);
        });
    }
}
