using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrl : MonoBehaviour
{
    [Header("REFERENCES")]
    public UI_UpgradeMenu UpgradeMenu;
    public UI_GameOver GameOverMenu;

    public void Retry()
    {
        DataHolder.Instance.StartGame();
    }

    public void MainMenu()
    {
        DataHolder.Instance.MainMenu();
    }
}
