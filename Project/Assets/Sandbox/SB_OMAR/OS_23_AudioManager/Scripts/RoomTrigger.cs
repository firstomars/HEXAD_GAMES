using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.AudioManager
{
    public class RoomTrigger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            AudioManager.AudioManagerInstance.PlaySound("RoomTrigger");
        }

        private void OnTriggerExit(Collider other)
        {
            AudioManager.AudioManagerInstance.StopSound("RoomTrigger");
        }

    }

}