using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


namespace Sandbox.Omar.CameraSetup
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera overworldCamera;
        [SerializeField] private CinemachineVirtualCamera kitchenCam;
        [SerializeField] private CinemachineVirtualCamera gymCam;

        private bool isPlayerInGym = false;
        private bool isPlayerInKitchen = false;

        public void SwitchCamera()
        {
            if (isPlayerInGym)
            {
                //Debug.Log("Gym camera switched on");
                gymCam.Priority = 1;
                overworldCamera.Priority = 0;
                kitchenCam.Priority = 0;
            }
            else if (isPlayerInKitchen)
            {
                //Debug.Log("kitchen camera switched on");
                kitchenCam.Priority = 1;
                gymCam.Priority = 0;
                overworldCamera.Priority = 0;
            }
            else
            {
                //Debug.Log("overworld camera switched on");
                overworldCamera.Priority = 1;
                gymCam.Priority = 0;
                kitchenCam.Priority = 0;
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
                    break;

                case "kitchen":
                    Debug.Log("player in kitchen");
                    isPlayerInKitchen = true;
                    isPlayerInGym = false;
                    break;

                default:
                    Debug.Log("player in house");
                    isPlayerInKitchen = false;
                    isPlayerInGym = false;
                    break;
            }
        }

    }
}