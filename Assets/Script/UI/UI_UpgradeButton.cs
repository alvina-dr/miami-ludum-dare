using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_UpgradeButton : MonoBehaviour
{
    public UpgradeData data;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image image;

    #region Methods
    public void SetupButton(UpgradeData _data)
    {
        data = _data;
        nameText.text = data.upgradeName;
        descriptionText.text = data.upgradeDescription;
        image.sprite = data.sprite;
    }

    public void Upgrade()
    {
        data.Upgrade(GPCtrl.Instance.GeneralData);
    }

    #endregion
}
