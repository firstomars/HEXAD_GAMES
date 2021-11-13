using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.SleepTest
{
    public class RoomTrigger : MonoBehaviour
    {

        [SerializeField] private GameObject playerObj;
        private PlayerController PlayerController;
        
        // Start is called before the first frame update
        void Start()
        {
            PlayerController = playerObj.GetComponent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerController.IsInBedroom(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerController.IsInBedroom(false);
            }
        }
    }
}
