using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    bool isOverworldCamera = true;

    //List<CinemachineVirtualCamera> cameras;

    [SerializeField] CinemachineVirtualCamera[] cameras;

    [SerializeField] CinemachineVirtualCamera overworldCam;
    [SerializeField] CinemachineVirtualCamera closeCam;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void SwitchCamera()
    {

    }
}
