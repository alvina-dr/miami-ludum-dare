using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSpawn", menuName = "ScriptableObjects/UpgradeData/TileSpawn", order = 1)]
public class Upgrade_TileSpawn : UpgradeData
{
    public override void Upgrade(GPCtrl.UpgradeSave data, Player player, int num)
    {
        base.Upgrade(data, player, num);
        data.tileFrequency *= factor;
    }
}
