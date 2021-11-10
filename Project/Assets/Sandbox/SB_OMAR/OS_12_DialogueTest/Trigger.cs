using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.DialogueTest
{
    public class Trigger : MonoBehaviour
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
                PlayerController.SetPlayerPosition("bedroom");
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

