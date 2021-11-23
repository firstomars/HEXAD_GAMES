using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : UpgradeableObject
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectToUpgradeManagerOnStartUp();

        currentUpgradeIndex = 0;
        maxUpgradeIndex = upgrades.Length - 2;
        CreateObjectOnStartUp();

        var PlayerController = GameManager.Instance.player.GetComponent<PlayerController>();
        PlayerStatistics = PlayerController.PlayerStatistics;
    }

    public override void UpgradeObject()
    {
        base.UpgradeObject();
    }

    public override void ConnectToUpgradeManagerOnStartUp()
    {
        UIManager.UIManagerInstance.UpgradeManager.AddToObjectList(this);
    }
}