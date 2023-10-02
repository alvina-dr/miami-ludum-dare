using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "ScriptableObjects/UpgradeData/Sword", order = 1)]
public class Upgrade_Sword : UpgradeData
{
    public override void Upgrade(GPCtrl.UpgradeSave data, Player player, int num)
    {
        base.Upgrade(data, player, num);
        data.swordFrequency *= factor;
    }
}
