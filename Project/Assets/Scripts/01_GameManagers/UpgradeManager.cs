using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] public List<UpgradeableObject> upgradeableObjs;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.UIManagerInstance.UpgradeManager = this;
    }

    public void UpgradeObject(string objToUpgrade)
    {
        foreach(var obj in upgradeableObjs)
        {
            if (obj.name == objToUpgrade)
            {
                obj.UpgradeObject();
            }
        }
    }

    public void AddToObjectList(UpgradeableObject objToAdd)
    {
        upgradeableObjs.Add(objToAdd);
    }


}
