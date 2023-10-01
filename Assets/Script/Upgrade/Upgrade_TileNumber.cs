using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileNumber", menuName = "ScriptableObjects/UpgradeData/TileNumber", order = 1)]
public class Upgrade_TileNumber : UpgradeData
{
    public override void Upgrade(GPCtrl.UpgradeSave data)
    {
        base.Upgrade(data);
        data.tileNumber += factor;
    }
}
