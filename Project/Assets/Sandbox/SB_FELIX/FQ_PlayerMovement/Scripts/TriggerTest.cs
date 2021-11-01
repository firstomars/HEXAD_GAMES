using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sandbox.Felix.PlayerMovement
{
    public class TriggerTest : MonoBehaviour
    {
        //CameraManager CameraManager;

        // Start is called before the first frame update
        private void Start()
        {
            //CameraManager = GetComponentInParent<CameraManager>();


        }

        
        void OnTriggerEnter(Collider other)
        {
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.green;

           if (gameObject.GetComponent<Renderer>().material.color == Color.green)      
            {
                //CameraManager.SetPlayerPosition("kitchen");
            }

        }


        private void OnTriggerExit(Collider other)
        {
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.red;
            if (other.gameObject.tag == "Player")
            {
                //CameraManager.SetPlayerPosition();
                Debug.Log("hello");
            }
        }

     
    }


   // private void OnTriggerExit(Collider other)
    //{
      //  Renderer render = GetComponent<Renderer>();
      //  render.material.color = Color.red;
      //  if (other.gameObject.tag == "Player")
      //  {
            //CameraManager.SetPlayerPosition();
           // Debug.Log("gameObject.tag" + "hello");
       // }
   // }

}
