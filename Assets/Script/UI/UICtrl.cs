using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UICtrl : MonoBehaviour
{
    [Header("REFERENCES")]
    public UI_UpgradeMenu UpgradeMenu;
    public UI_GameOver GameOverMenu;
    public TextMeshProUGUI counter;

    public void Retry()
    {
        DataHolder.Instance.StartGame();
    }

    public void MainMenu()
    {
        DataHolder.Instance.MainMenu();
    }

    public void SetCounter(int _value)
    {
        counter.transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            counter.transform.DOScale(1f, .2f);
            counter.text = _value.ToString();
        });
    }
}
