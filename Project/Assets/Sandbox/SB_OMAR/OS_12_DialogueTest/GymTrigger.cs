using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.DialogueTest
{
    public class GymTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject playerControllerGO;
        private PlayerController PlayerController;

        private void Start()
        {
            PlayerController = playerControllerGO.GetComponent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                PlayerController.SetPlayerPosition("gym");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                PlayerController.SetPlayerPosition();
            }
        }
    }
}
