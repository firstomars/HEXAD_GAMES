using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sandbox.Omar.CameraSetup
{
    public class GymCollisionManager : MonoBehaviour
    {
        CameraManager CameraManager;

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
}