using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.Greybox
{
    public class RoomTrigger : MonoBehaviour
    {
        [SerializeField] protected GameObject camMgrObj;
        protected CameraManager CameraManager;
        protected Renderer renderer;
        protected bool isInRoom;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        public virtual void IsInRoom(bool value)
        {
            if (value == true) renderer.material.color = Color.green;
            else renderer.material.color = Color.red;
        }
    }
}

