using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPCtrl : MonoBehaviour
{
    public static GPCtrl Instance { get; private set; }

    #region Properties
    public GeneralData GeneralData;
    #endregion

    #region Unity API
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
}
