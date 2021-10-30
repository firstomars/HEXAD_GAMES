using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


namespace Sandbox.Omar.CameraSetup
{
    public class CameraManager : MonoBehaviour
    {
        //[System.Serializable]
        //public class CinemachineCameras
        //{
        //    [SerializeField] public string cameraName;
        //    [SerializeField] private CinemachineVirtualCamera cineCamObject;
        //}

        //[SerializeField] private CinemachineCameras[] cameras;

        //[SerializeField]
        //public enum CameraState
        //{
        //    OVERWORLD,
        //    KITCHEN,
        //    GYM
        //}


        [SerializeField] private CinemachineVirtualCamera overworldCamera;
        [SerializeField] private CinemachineVirtualCamera kitchenCam;
        [SerializeField] private CinemachineVirtualCamera gymCam;
        private CinemachineVirtualCamera currentCam;

        private bool isOverworldCamActive = true;

        public bool isPlayerInGym = false;
        public bool hasPlayerReachedTarget = false;
        
        
        

        // Start is called before the first frame update
        void Start()
        {
            
        }

        private void Update()
        {
            //logic to switch cameras at right time
            
            if (hasPlayerReachedTarget)
                SwitchCamera();
            
            //if (Input.GetKeyDown(KeyCode.Space))
            //    SwitchCamera();
        }

        //private void SwitchCamera() //string cameraName
        //{
        //    if (isOverworldCamActive)
        //    {
        //        overworldCamera.Priority = 0;
        //        kitchenCam.Priority = 1;
        //        Debug.Log("Switched to close cam");
        //    }
        //    else
        //    {
        //        overworldCamera.Priority = 1;
        //        kitchenCam.Priority = 0;
        //        Debug.Log("Switched to overworld cam");
        //    }
        //    isOverworldCamActive = !isOverworldCamActive;
        //}

        private void SwitchCamera()
        {
            if (isPlayerInGym)
            {
                gymCam.Priority = 1;
                overworldCamera.Priority = 0;
                kitchenCam.Priority = 0;
            }
        }

    }
}