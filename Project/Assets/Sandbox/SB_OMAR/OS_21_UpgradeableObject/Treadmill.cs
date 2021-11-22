using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.UpgradeObjectTesting
{
    public class Treadmill : UpgradeableObject
    {
        void Awake()
        {
            ConnectToUIManagerOnStartUp();
        }

        // Start is called before the first frame update
        void Start()
        {
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
