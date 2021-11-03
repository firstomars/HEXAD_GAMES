using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.BehaviourDebug
{
    public class SleepBehaviour : Behaviour
    {
        private PlayerController PlayerController { get; set; }
        public SleepBehaviour(PlayerController playerController) : base(playerController)
        {
            PlayerController = playerController;
        }

        public override void StartBehaviour()
        {
            Debug.Log("SleepBehaviour Start called - press L to test update");
            base.StartBehaviour();
        }

        public override void RunBehaviour()
        {
            //Debug.Log("SleepBehaviour Update called");

            if (Input.GetKeyDown(KeyCode.L))
                Debug.Log("key L has been pressed");

            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            Debug.Log("SleepBehaviour End called");
            base.EndBehaviour();
        }
    }
}

