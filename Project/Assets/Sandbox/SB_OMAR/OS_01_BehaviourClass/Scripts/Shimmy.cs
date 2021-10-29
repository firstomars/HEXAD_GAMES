using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sandbox.Omar.Behaviour
{
    public class Shimmy : Behaviour
    {
        public Shimmy(PlayerController playerController) : base(playerController)
        {

        }

        //not derived from monobehaviour - choose a different function name for Start and Update?
        public override void StartBehaviour()
        {
            Debug.Log("Shimmy Start called");
            base.StartBehaviour();
        }
        
        public override void RunBehaviour()
        {
            //Debug.Log("Shimmy Update called");

            if (Input.GetKeyDown(KeyCode.Return))
                Debug.Log("enter pressed");
            
            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            base.EndBehaviour();
        }
    }
}