using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.BehaviourDebug
{
    public class EatBehaviour : Behaviour
    {
        //===
        //NEW
        private GameObject player;
        private BehaviourDebugUI BehaviourDebugUI;
        //===

        private PlayerController PlayerController { get; set; }

        public EatBehaviour(PlayerController playerController) : base(playerController)
        {
            PlayerController = playerController;
        }

        public override void StartBehaviour()
        {
            Debug.Log("EatBehaviour Start called - press A to test update");
            base.StartBehaviour();

            //===
            //NEW
            player = PlayerController.ReturnPlayerGameObject();
            BehaviourDebugUI = player.GetComponent<BehaviourDebugUI>();
            //===
        }

        public override void RunBehaviour()
        {
            //Debug.Log("EatBehaviour Update called");

            //===
            //NEW
            BehaviourDebugUI.SetDebugUI("kitchen");
            //===

            if (Input.GetKeyDown(KeyCode.A))
                Debug.Log("key A has been pressed");

            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            Debug.Log("EatBehaviour End called");

            //===
            //NEW
            BehaviourDebugUI.SetDebugUI();
            //===

            base.EndBehaviour();
        }
    }
}
