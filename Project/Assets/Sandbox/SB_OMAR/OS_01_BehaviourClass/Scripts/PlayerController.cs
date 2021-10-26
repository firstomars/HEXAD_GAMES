using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.Behaviour
{
    public class PlayerController : BehaviourStateMachine //this is derived from monobehaviour
    {
        [SerializeField] Transform endHopPos;
        private Vector3 currentPosition;
        
        public Vector3 Position
        {
            get { return currentPosition; }
            set { currentPosition = value; }
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            Position = gameObject.transform.position;
            
            //sets start behaviour as Hop
            SetBehaviour(new HopBehaviour(this));
        }

        // Update is called once per frame
        private void Update()
        {
            //gameObject.transform.position = currentPosition;
        }
    }
}
