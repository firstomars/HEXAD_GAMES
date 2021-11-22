using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.UpgradeObjectTesting
{
    public class UpgradeableObject : MonoBehaviour
    {
        [Header ("Upgrade Details")]
        [SerializeField] protected GameObject[] upgrades;
        [SerializeField] protected int currentUpgradeIndex;
        
        //variables to manage upgrade process
        protected int maxUpgradeIndex;
        protected GameObject currentObject;

        public virtual void UpgradeObject()
        {
            if (currentUpgradeIndex > maxUpgradeIndex) Debug.Log("max upgrade reached");
            else
            {
                currentUpgradeIndex++;
                Destroy(currentObject);
                currentObject = Instantiate(upgrades[currentUpgradeIndex], gameObject.transform);

                Debug.Log("Current object Index " + currentUpgradeIndex);
                Debug.Log("Max object Index " + maxUpgradeIndex);
            }
        }

        public virtual void CreateObjectOnStartUp()
        {
            currentObject = Instantiate(upgrades[currentUpgradeIndex], gameObject.transform);
        }

        public virtual void ConnectToUIManagerOnStartUp()
        {
            
        }
    }
}

