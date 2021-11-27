using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeableObject : MonoBehaviour
{
    [Header("Upgrade Details")]
    [SerializeField] protected GameObject[] upgrades;
    [SerializeField] protected int currentUpgradeIndex;
    [SerializeField] public int upgradeCost;

    //variables to manage upgrade process
    protected int maxUpgradeIndex;
    protected GameObject currentObject;

    protected PlayerStatistics PlayerStatistics;

    public virtual void UpgradeObject()
    {
        if (currentUpgradeIndex > maxUpgradeIndex)
        {
            Debug.Log("max upgrade reached");
            return;
        }

        if (PlayerStatistics.GetSleepDollars() >= upgradeCost)
        {
            PlayerStatistics.ReduceSleepDollars(upgradeCost);
            currentUpgradeIndex++;
            Destroy(currentObject);
            currentObject = Instantiate(upgrades[currentUpgradeIndex], gameObject.transform);

            Debug.Log("Current object Index " + currentUpgradeIndex);
            Debug.Log("Max object Index " + maxUpgradeIndex);
        }
        else
        {
            Debug.Log("not enough money");
        }

    }

    public virtual void CreateObjectOnStartUp()
    {
        currentObject = Instantiate(upgrades[currentUpgradeIndex], gameObject.transform);
    }

    public virtual void ConnectToUpgradeManagerOnStartUp()
    {

    }
}
