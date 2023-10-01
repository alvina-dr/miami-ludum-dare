using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    #region Methods
    public void StartGame()
    {
        DataHolder.Instance.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {

    }

    public void Upgrades()
    {

    }
    #endregion
}
