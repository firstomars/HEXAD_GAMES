using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.Behaviour
{
    public class SeekBehaviour : Behaviour
    {
        public SeekBehaviour(PlayerController playerController) : base(playerController)
        {

        }

        public override void StartBehaviour()
        {
            Debug.Log("SeekBehaviour Start called - press K to test update");
            base.StartBehaviour();
        }

        public override void RunBehaviour()
        {
            //Debug.Log("SeekBehaviour Update called");

            if (Input.GetKeyDown(KeyCode.K))
                Debug.Log("key K has been pressed");

            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            Debug.Log("SeekBehaviour End called");
            base.EndBehaviour();
        }
    }
}

