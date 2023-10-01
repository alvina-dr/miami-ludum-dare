using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/UpgradeData", order = 1)]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string upgradeDescription;
    public Sprite sprite;
    public int cost;
    public float factor;

    public virtual void Upgrade(GPCtrl.UpgradeSave data)
    {

    }
}
