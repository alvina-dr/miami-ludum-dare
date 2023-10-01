using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_GameOver : MonoBehaviour
{
    public AudioSource audioSource;

    public void OpenMenu()
    {
        audioSource.clip = GPCtrl.Instance.GeneralData.menuSound[Random.Range(0, GPCtrl.Instance.GeneralData.menuSound.Count)];
        audioSource.Play();
        transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            transform.DOScale(1f, .2f);
        });
    }
}
