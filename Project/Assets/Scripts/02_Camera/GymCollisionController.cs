using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymCollisionController : MonoBehaviour
{
    CameraManager CameraManager;
    //PlayerController

    private void Start()
    {
        CameraManager = GetComponentInParent<CameraManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            CameraManager.SetPlayerPosition("gym");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            CameraManager.SetPlayerPosition();
    }
}
