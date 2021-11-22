using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Sandbox.Omar.UpgradeObjectTesting
{
    public class UIManager : MonoBehaviour
    {
        #region Singelton

        private static UIManager _UIManagerInstance;

        public static UIManager UIManagerInstance { get { return _UIManagerInstance; } }

        private void Awake()
        {
            if (_UIManagerInstance != null) Destroy(gameObject);
            else _UIManagerInstance = this;
        }
        #endregion


        [SerializeField] private GameObject btnUpgradeBedObj;
        private Button btnUpgradeBed;
        [SerializeField] private GameObject btnUpgradeTreadmillObj;
        private Button btnUpgradeTreadmill;

        [HideInInspector] public Bed bedController;
        [HideInInspector] public Treadmill treadmillController;

        // Start is called before the first frame update
        void Start()
        {
            btnUpgradeBed = btnUpgradeBedObj.GetComponent<Button>();
            btnUpgradeTreadmill = btnUpgradeTreadmillObj.GetComponent<Button>();
            
            if (bedController != null)
                btnUpgradeBed.onClick.AddListener(bedController.UpgradeObject);

            if (treadmillController != null)
                btnUpgradeTreadmill.onClick.AddListener(treadmillController.UpgradeObject);
        }
    }
}

