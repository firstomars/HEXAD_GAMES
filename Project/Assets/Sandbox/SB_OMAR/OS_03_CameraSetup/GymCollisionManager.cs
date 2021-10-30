using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sandbox.Omar.CameraSetup
{
    public class GymCollisionManager : MonoBehaviour
    {
        CameraManager cameraManager;

        private void Start()
        {
            cameraManager = GetComponentInParent<CameraManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("player has entered the gym");
                cameraManager.isPlayerInGym = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("player has left the gym");
                cameraManager.isPlayerInGym = false;
            }
        }
    }
}