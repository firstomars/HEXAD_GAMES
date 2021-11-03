using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.BehaviourDebug
{
    public class BathroomBehaviour : Behaviour
    {
        private PlayerController PlayerController { get; set; }
        
        public BathroomBehaviour(PlayerController playerController) : base(playerController)
        {
            PlayerController = playerController;
        }

        public override void StartBehaviour()
        {
            Debug.Log("BathroomBehaviour Start called - press B to test update");
            base.StartBehaviour();
        }

        public override void RunBehaviour()
        {
            //Debug.Log("BathroomBehaviour Update called");

            if (Input.GetKeyDown(KeyCode.B))
                Debug.Log("key B has been pressed");

            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            Debug.Log("BathroomBehaviour End called");
            base.EndBehaviour();
        }
    }
}
