using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Setup")]    
    [SerializeField] private CinemachineVirtualCamera overworldCamera;
    [SerializeField] private CinemachineVirtualCamera kitchenCam;
    [SerializeField] private CinemachineVirtualCamera gymCam;
    [SerializeField] private CinemachineVirtualCamera bedroomCam;
    [SerializeField] private CinemachineVirtualCamera bathroomCam;
    [SerializeField] private CinemachineVirtualCamera trophyCam;

    //player position checks
    private bool isPlayerInGym = false;
    private bool isPlayerInKitchen = false;
    private bool isPlayerInBedroom = false;
    private bool isPlayerInBathroom = false;
    private bool isPlayerAtTrophyCabinet = false;

    public void SwitchCamera()
    {
        if (isPlayerInKitchen)
        {
            overworldCamera.Priority = 0;
            kitchenCam.Priority = 1;
            gymCam.Priority = 0;
            bedroomCam.Priority = 0;
            bathroomCam.Priority = 0;
            trophyCam.Priority = 0;
        }
        else if (isPlayerInGym)
        {
            overworldCamera.Priority = 0;
            kitchenCam.Priority = 0;
            gymCam.Priority = 1;
            bedroomCam.Priority = 0;
            bathroomCam.Priority = 0;
            trophyCam.Priority = 0;
        }
        else if (isPlayerInBedroom)
        {
            overworldCamera.Priority = 0;
            kitchenCam.Priority = 0;
            gymCam.Priority = 0;
            bedroomCam.Priority = 1;
            bathroomCam.Priority = 0;
            trophyCam.Priority = 0;
        }
        else if (isPlayerInBathroom)
        {
            overworldCamera.Priority = 0;
            kitchenCam.Priority = 0;
            gymCam.Priority = 0;
            bedroomCam.Priority = 0;
            bathroomCam.Priority = 1;
            trophyCam.Priority = 0;
        }
        else if (isPlayerAtTrophyCabinet)
        {
            Debug.Log("trophy camera switched on"); //DELETE
            overworldCamera.Priority = 0;
            kitchenCam.Priority = 0;
            gymCam.Priority = 0;
            bedroomCam.Priority = 0;
            bathroomCam.Priority = 0;
            trophyCam.Priority = 1;
        }
        else
        {
            overworldCamera.Priority = 1;
            kitchenCam.Priority = 0;
            gymCam.Priority = 0;
            bedroomCam.Priority = 0;
            bathroomCam.Priority = 0;
            trophyCam.Priority = 0;
            overworldCamera.LookAt = GameManager.Instance.player.transform;
        }
    }

    public void SetPlayerPosition(string room = default)
    {
        switch(room)
        {
            case "gym":
                isPlayerInGym = true;
                isPlayerInKitchen = false;
                isPlayerInBedroom = false;
                isPlayerInBathroom = false;
                isPlayerAtTrophyCabinet = false;
                break;

            case "kitchen":
                isPlayerInGym = false;
                isPlayerInKitchen = true;
                isPlayerInBedroom = false;
                isPlayerInBathroom = false;
                isPlayerAtTrophyCabinet = false;
                break;

            case "bedroom":
                isPlayerInGym = false;
                isPlayerInKitchen = false;
                isPlayerInBedroom = true;
                isPlayerInBathroom = false;
                isPlayerAtTrophyCabinet = false;
                break;

            case "bathroom":
                isPlayerInGym = false;
                isPlayerInKitchen = false;
                isPlayerInBedroom = false;
                isPlayerInBathroom = true;
                isPlayerAtTrophyCabinet = false;
                break;

            case "trophycabinet":
                Debug.Log("player at trophy cabinet"); //DELETE
                isPlayerInGym = false;
                isPlayerInKitchen = false;
                isPlayerInBedroom = false;
                isPlayerInBathroom = false;
                isPlayerAtTrophyCabinet = true;
                break;

            default:
                isPlayerInGym = false;
                isPlayerInKitchen = false;
                isPlayerInBedroom = false;
                isPlayerInBathroom = false;
                isPlayerAtTrophyCabinet = false;
                break;
        }
    }

}