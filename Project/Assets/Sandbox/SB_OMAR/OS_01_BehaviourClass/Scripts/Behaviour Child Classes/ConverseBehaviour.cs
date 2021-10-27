using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.Behaviour
{
    public class ConverseBehaviour : Behaviour
    {
        public ConverseBehaviour(PlayerController playerController) : base(playerController)
        {

        }

        public override void StartBehaviour()
        {
            Debug.Log("ConverseBehaviour Start called - press C to test update");
            base.StartBehaviour();
        }

        public override void RunBehaviour()
        {
            //Debug.Log("ConverseBehaviour Update called");

            if (Input.GetKeyDown(KeyCode.C))
                Debug.Log("key C has been pressed");

            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            Debug.Log("ConverseBehaviour End called");
            base.EndBehaviour();
        }
    }
}