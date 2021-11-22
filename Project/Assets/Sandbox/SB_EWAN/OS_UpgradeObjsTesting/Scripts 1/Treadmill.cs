using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Ewan.UpgradeObjsTesting
{
    public class Treadmill : UpgradeableObject
    {
        void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            ConnectToUIManagerOnStartUp();

            currentUpgradeIndex = 0;
            maxUpgradeIndex = upgrades.Length - 2;

            CreateObjectOnStartUp();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void UpgradeObject()
        {
            Debug.Log("treadmill upgraded");
            base.UpgradeObject();
        }

        public override void ConnectToUIManagerOnStartUp()
        {
            UIManager.UIManagerInstance.treadmillController = this;
        }
    }

}
