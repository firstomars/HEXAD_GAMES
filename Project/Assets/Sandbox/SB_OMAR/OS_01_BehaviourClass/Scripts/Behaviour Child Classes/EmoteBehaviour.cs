using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.Behaviour
{
    public class EmoteBehaviour : Behaviour
    {
        public EmoteBehaviour(PlayerController playerController) : base(playerController)
        {

        }

        public override void StartBehaviour()
        {
            Debug.Log("EmoteBehaviour Start called - press T to test update");
            base.StartBehaviour();
        }

        public override void RunBehaviour()
        {
            //Debug.Log("EmoteBehaviour Update called");

            if (Input.GetKeyDown(KeyCode.T))
                Debug.Log("key T has been pressed");

            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            Debug.Log("EmoteBehaviour End called");
            base.EndBehaviour();
        }
    }
}


