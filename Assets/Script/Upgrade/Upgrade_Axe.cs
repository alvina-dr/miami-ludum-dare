using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Axe", menuName = "ScriptableObjects/UpgradeData/Axe", order = 1)]
public class Upgrade_Axe : UpgradeData
{
    public override void Upgrade(GPCtrl.UpgradeSave data, Player player, int num)
    {
        base.Upgrade(data, player, num);
        if (num == 1)
        {
            player.axe.gameObject.SetActive(true);
        } else
        {
            data.axeFrequency *= factor;
            player.axe.Setup();
        }
    }
}
