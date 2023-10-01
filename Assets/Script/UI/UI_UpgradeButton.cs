using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_UpgradeButton : MonoBehaviour
{
    public UpgradeData data;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image image;
    public int num = 1;
    [SerializeField] private AudioSource audioSource;
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
        audioSource.clip = GPCtrl.Instance.GeneralData.upgradeSound[Random.Range(0, GPCtrl.Instance.GeneralData.upgradeSound.Count)];
        audioSource.Play();
        data.Upgrade(GPCtrl.Instance.upgradeSave);
        GPCtrl.Instance.DestroyFarthestTiles(data.cost);
        GPCtrl.Instance.UICtrl.UpgradeMenu.UpdateMenu();
        num += 1;
    }

    public void UpdateButton()
    {
        costText.text = (data.cost * num).ToString();
        if (GPCtrl.Instance.builtTileCount() > data.cost)
        {
            costText.color = Color.black;
            button.interactable = true;
        } else
        {
            button.interactable = false;
            costText.color = Color.red;
        }
    }
    #endregion
}
