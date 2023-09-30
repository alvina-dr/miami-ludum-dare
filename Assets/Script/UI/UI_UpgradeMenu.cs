using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_UpgradeMenu : MonoBehaviour
{
    #region Properties
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform layout;
    [SerializeField] private UI_UpgradeButton buttonPrefab;
    #endregion

    #region 
    public void CreateMenu()
    {
        for (int i = 0; i < GPCtrl.Instance.upgradeDataList.Count; i++)
        {
            UI_UpgradeButton _button = Instantiate(buttonPrefab, layout);
            _button.SetupButton(GPCtrl.Instance.upgradeDataList[i]);
        }
    }

    public void CallMenu()
    {
        if (canvasGroup.transform.localScale.x >= 0.5f)
        {
            CloseMenu();
        } else
        {
            OpenMenu();
        }
    }

    public void OpenMenu()
    {
        GPCtrl.Instance.pause = true;
        canvasGroup.transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            canvasGroup.transform.DOScale(1f, .2f);
        });
    }

    public void CloseMenu()
    {
        canvasGroup.transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            canvasGroup.transform.DOScale(0f, .2f);
            GPCtrl.Instance.pause = false;
        });
    }
    #endregion

    #region Unity API
    private void Start()
    {
        CreateMenu();
    }
    #endregion
}
