using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sandbox.Omar.Behaviour
{
    public class HopBehaviour : Behaviour
    {       
        public HopBehaviour(PlayerController playerController) : base(playerController)
        {

        }

        public override void StartBehaviour()
        {
            Debug.Log("Hop Start called");
            base.StartBehaviour();
        }

        public override void RunBehaviour()
        {
            //Debug.Log("Hop Update called");

            if (Input.GetKeyDown(KeyCode.Space))
                Debug.Log("space pressed");

            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            Debug.Log("Hop End called");
            base.EndBehaviour();
        }
    }
}