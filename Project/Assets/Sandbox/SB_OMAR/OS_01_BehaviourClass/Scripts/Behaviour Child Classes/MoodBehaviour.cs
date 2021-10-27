using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.Behaviour
{
    public class MoodBehaviour : Behaviour
    {
        public MoodBehaviour(PlayerController playerController) : base(playerController)
        {

        }

        public override void StartBehaviour()
        {
            Debug.Log("MoodBehaviour Start called - press M to test update");
            base.StartBehaviour();
        }

        public override void RunBehaviour()
        {
            //Debug.Log("MoodBehaviour Update called");

            if (Input.GetKeyDown(KeyCode.M))
                Debug.Log("key M has been pressed");

            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            Debug.Log("MoodBehaviour End called");
            base.EndBehaviour();
        }
    }
}


