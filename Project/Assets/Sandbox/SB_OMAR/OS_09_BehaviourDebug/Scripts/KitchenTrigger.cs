using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.BehaviourDebug
{
    public class KitchenTrigger : MonoBehaviour
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
                //CameraManager.SetPlayerPosition("kitchen");

                PlayerController.SetPlayerPosition("kitchen");
                Debug.Log("I am in the kitchen");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                IsInRoom(false);

                //CameraManager.SetPlayerPosition();

                PlayerController.SetPlayerPosition();
                Debug.Log("I am no longer in the kitchen");
            }
        }

        public void IsInRoom(bool value)
        {
            //if (value == true) renderer.material.color = Color.green;
            //else renderer.material.color = Color.red;
        }
    }
}