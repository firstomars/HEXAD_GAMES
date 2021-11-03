using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


namespace Sandbox.Omar.Greybox
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera overworldCamera;
        [SerializeField] private CinemachineVirtualCamera kitchenCam;
        [SerializeField] private CinemachineVirtualCamera gymCam;
        [SerializeField] private CinemachineVirtualCamera bedroomCam;
        [SerializeField] private CinemachineVirtualCamera bathroomCam;

        private bool isPlayerInGym = false;
        private bool isPlayerInKitchen = false;
        private bool isPlayerInBedroom = false;
        private bool isPlayerInBathroom = false;


        public void SwitchCamera()
        {

            if (isPlayerInKitchen)
            {
                //Debug.Log("kitchen camera switched on");
                //Debug.Log("Gym camera switched on");
                overworldCamera.Priority = 0;
                kitchenCam.Priority = 1;
                gymCam.Priority = 0;
                bedroomCam.Priority = 0;
                bathroomCam.Priority = 0;
            }
            else if (isPlayerInGym)
            {
                //Debug.Log("Gym camera switched on");
                overworldCamera.Priority = 0;
                kitchenCam.Priority = 0;
                gymCam.Priority = 1;
                bedroomCam.Priority = 0;
                bathroomCam.Priority = 0;
            }
            else if (isPlayerInBedroom)
            {
                //Debug.Log("kitchen camera switched on");
                //Debug.Log("Gym camera switched on");
                overworldCamera.Priority = 0;
                kitchenCam.Priority = 0;
                gymCam.Priority = 0;
                bedroomCam.Priority = 1;
                bathroomCam.Priority = 0;
            }
            else if (isPlayerInBathroom)
            {
                //Debug.Log("bathroom camera switched on");
                overworldCamera.Priority = 0;
                kitchenCam.Priority = 0;
                gymCam.Priority = 0;
                bedroomCam.Priority = 0;
                bathroomCam.Priority = 1;
            }
            else
            {
                //Debug.Log("overworld camera switched on");
                overworldCamera.Priority = 1;
                kitchenCam.Priority = 0;
                gymCam.Priority = 0;
                bedroomCam.Priority = 0;
                bathroomCam.Priority = 0;
            }
        }

        public void SetPlayerPosition(string room = default)
        {
            switch(room)
            {
                case "gym":
                    Debug.Log("player in gym");
                    isPlayerInGym = true;
                    isPlayerInKitchen = false;
                    isPlayerInBedroom = false;
                    isPlayerInBathroom = false;
                    break;

                case "kitchen":
                    Debug.Log("player in kitchen");
                    isPlayerInGym = false;
                    isPlayerInKitchen = true;
                    isPlayerInBedroom = false;
                    isPlayerInBathroom = false;
                    break;

                case "bedroom":
                    Debug.Log("player in bedroom");
                    isPlayerInGym = false;
                    isPlayerInKitchen = false;
                    isPlayerInBedroom = true;
                    isPlayerInBathroom = false;
                    break;

                case "bathroom":
                    Debug.Log("player in bathroom");
                    isPlayerInGym = false;
                    isPlayerInKitchen = false;
                    isPlayerInBedroom = false;
                    isPlayerInBathroom = true;
                    break;

                default:
                    Debug.Log("player in house");
                    isPlayerInGym = false;
                    isPlayerInKitchen = false;
                    isPlayerInBedroom = false;
                    isPlayerInBathroom = false;
                    break;
            }
        }

    }
}