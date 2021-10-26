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

        public override void Start()
        {
            Debug.Log("Hop Start called");
            base.Start();
        }

        public override void Update()
        {
            Debug.Log("Hop Update called");
            PlayerController.gameObject.transform.position = new Vector3(0, 2, 0);
            base.Update();
        }

        public override void End()
        {
            base.End();
        }
    }
}
