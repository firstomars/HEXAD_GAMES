using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sandbox.Omar.Greybox
{
    public class BedRoomTriggerTest : MonoBehaviour
    {
        //CameraManager CameraManager;

        // Start is called before the first frame update
        private void Start()
        {
            //CameraManager = GetComponentInParent<CameraManager>();
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.red;

        }

        
        void OnTriggerEnter(Collider other)
        {
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.green;

           if (gameObject.GetComponent<Renderer>().material.color == Color.green)      
            {
                //CameraManager.SetPlayerPosition("kitchen");
                Debug.Log("Time for bed");
            }

        }


        private void OnTriggerExit(Collider other)
        {
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.red;
            if (other.gameObject.tag == "Player")
            {
                //CameraManager.SetPlayerPosition();
                Debug.Log("Wow that was a crap sleep. I feel terrible");
            }
        }

     
    }

}
