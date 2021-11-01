using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Felix.PlayerMovement
{
    public class RoomTrigger : MonoBehaviour
    {
        //CameraManager CameraManager;

        // Start is called before the first frame update
        private void Start()
        {
            //CameraManager = GetComponentInParent<CameraManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                //CameraManager.SetPlayerPosition("kitchen");
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                //CameraManager.SetPlayerPosition();
            }
        }
    }
}
