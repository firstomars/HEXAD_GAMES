using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.Behaviour
{
    public class WanderBehaviour : Behaviour
    {
        public WanderBehaviour(PlayerController playerController) : base(playerController)
        {

        }

        public override void StartBehaviour()
        {
            Debug.Log("WanderBehaviour Start called - press W to test update");
            base.StartBehaviour();
        }

        public override void RunBehaviour()
        {
            //Debug.Log("WanderBehaviour Update called");

            if (Input.GetKeyDown(KeyCode.W))
                Debug.Log("key W has been pressed");

            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            Debug.Log("WanderBehaviour End called");
            base.EndBehaviour();
        }
    }
}

