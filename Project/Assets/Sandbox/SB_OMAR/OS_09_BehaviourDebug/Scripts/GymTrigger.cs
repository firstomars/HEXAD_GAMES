using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.BehaviourDebug
{
    public class GymTrigger : MonoBehaviour
    {
        protected Renderer renderer;
        protected bool isInRoom;

        PlayerController PlayerController;

        // Start is called before the first frame update
        void Start()
        {
            //CameraManager = camMgrObj.GetComponent<CameraManager>();
            //renderer = GetComponent<Renderer>();
            PlayerController = GameManager.Instance.player.GetComponent<PlayerController>();
            IsInRoom(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                IsInRoom(true);
                //CameraManager.SetPlayerPosition("gym");

                PlayerController.SetPlayerPosition("gym");
                //Debug.Log("I am in the gym");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                IsInRoom(false);

                //CameraManager.SetPlayerPosition();
                PlayerController.SetPlayerPosition();
                Debug.Log("I am no longer in the gym");
            }
        }

        public void IsInRoom(bool value)
        {
            //if (value == true) renderer.material.color = Color.green;
            //else renderer.material.color = Color.red;
        }
    }
}

