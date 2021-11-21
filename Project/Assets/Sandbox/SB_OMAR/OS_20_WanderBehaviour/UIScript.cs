using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Sandbox.Omar.WanderBehaviour
{
    public class UIScript : MonoBehaviour
    {
        //[Header("Game Objects")]
        //[SerializeField] private Transform loungeTransform;
        //[SerializeField] private Transform gymTransform;
        //[SerializeField] private Transform kitchenTransform;
        //[SerializeField] private Transform bedroomTransform;
        //[SerializeField] private NavMeshAgent playerObject;
        //[SerializeField] private Material playerMaterial;

        [Header("UI Panels")]
        //[SerializeField] private GameObject playerResponsePanel;
        //[SerializeField] private GameObject petColourPanel;
        [SerializeField] private GameObject flyoutButtonPanel;
        [SerializeField] private GameObject mainFlyoutPanel;
        [SerializeField] private GameObject walkFlyoutPanel;

        [Header("Flyout Button Images")]
        [SerializeField] private Sprite activateFlyoutImage;
        [SerializeField] private Sprite deactivateFlyoutImage;

        //[Header("UIButton Prefabs")]
        //[SerializeField] private GameObject playerResponseButtonPrefab;
        //[SerializeField] private GameObject colourSwatchButtonPrefab;

        [Header("Navigation")]
        [SerializeField] private GameObject kitchenBtnGO;
        private Button kitchenBtn;
        [SerializeField] private GameObject gymBtnGO;
        private Button gymBtn;
        //[SerializeField] private GameObject bathroomBtnGO;
        //private Button bathroomBtn;
        [SerializeField] private GameObject bedroomBtnGO;
        private Button bedroomBtn;
        //[SerializeField] private GameObject trophyCabinetBtnGO;
        //private Button trophyCabinetBtn;
        [SerializeField] private GameObject livingRoomBtnGO;
        private Button livingRoomBtn;

        // Local class variables
        private bool mainFlyoutActivated = false;
        private bool walkFlyoutActivated = false;

        private void Awake()
        {
            kitchenBtn = kitchenBtnGO.GetComponent<Button>();
            gymBtn = gymBtnGO.GetComponent<Button>();
            //bathroomBtn = bathroomBtnGO.GetComponent<Button>();
            bedroomBtn = bedroomBtnGO.GetComponent<Button>();
            //trophyCabinetBtn = trophyCabinetBtnGO.GetComponent<Button>();
            livingRoomBtn = livingRoomBtnGO.GetComponent<Button>();
        }

        // Main flyout button pressed
        public void ActivateMainFlyoutMenu()
        {
            if (!mainFlyoutActivated)
            {
                mainFlyoutPanel.SetActive(true);
                mainFlyoutActivated = true;
                flyoutButtonPanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = deactivateFlyoutImage;
            }
            else
            {
                CloseAllFlyouts();
            }
        }

        // Walk flyout button pressed
        public void ActivateWalkFlyoutMenu()
        {
            if (!walkFlyoutActivated)
            {
                walkFlyoutPanel.SetActive(true);
                walkFlyoutActivated = true;
            }
            else
            {
                walkFlyoutPanel.SetActive(false);
                walkFlyoutActivated = false;
            }
        }

        public void CloseAllFlyouts()
        {
            if (walkFlyoutActivated)
            {
                walkFlyoutPanel.SetActive(false);
                walkFlyoutActivated = false;
            }
            mainFlyoutPanel.SetActive(false);
            mainFlyoutActivated = false;
            flyoutButtonPanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = activateFlyoutImage;
        }

        public void SetNavigationUIListeners(PlayerController playerController)
        {
            kitchenBtn.onClick.AddListener(playerController.SeekKitchen);
            gymBtn.onClick.AddListener(playerController.SeekGym);
            //bathroomBtn.onClick.AddListener(playerController.SeekBathroom);
            bedroomBtn.onClick.AddListener(playerController.SeekBedroom);
            //trophyCabinetBtn.onClick.AddListener(playerController.SeekTrophyCabinet);
            livingRoomBtn.onClick.AddListener(playerController.SeekLivingRoom);
        }

        public void RoomButtonClicked()
        {
            //===
            //OMAR FEEDBACK - explainer comments
            //add some comments in this function to explain what's being done

            string clickedButton = EventSystem.current.currentSelectedGameObject.name;
            string selectedRoom = clickedButton.Substring(4);
            Debug.Log("Player selected " + clickedButton);



            // Not UI related
            //MovePet(selectedRoom);

            // UI related
            CloseAllFlyouts();
        }
    
        private void DestroyUIButtons(GameObject UIParent)
        {
            foreach (Transform child in UIParent.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
