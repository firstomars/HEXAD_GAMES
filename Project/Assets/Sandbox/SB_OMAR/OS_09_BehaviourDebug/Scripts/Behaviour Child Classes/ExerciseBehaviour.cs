using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.BehaviourDebug
{
    public class ExerciseBehaviour : Behaviour
    {
        //===
        //NEW
        private GameObject player;
        private BehaviourDebugUI BehaviourDebugUI;
        //===

        private PlayerController PlayerController { get; set; }
        public ExerciseBehaviour(PlayerController playerController) : base(playerController)
        {
            PlayerController = playerController;
        }

        public override void StartBehaviour()
        {
            Debug.Log("ExerciseBehaviour Start called - press E to test update");
            //Debug.Log(GameObject.name);
            
            base.StartBehaviour();

            //===
            //NEW
            player = PlayerController.ReturnPlayerGameObject();
            BehaviourDebugUI = player.GetComponent<BehaviourDebugUI>();
            //===
        }

        public override void RunBehaviour()
        {
            //===
            //NEW
            BehaviourDebugUI.SetDebugUI("gym");
            //BehaviourDebugUI.SetGymDebugUI(true);
            //===

            //Debug.Log("ExerciseBehaviour Update called");

            if (Input.GetKeyDown(KeyCode.E))
                Debug.Log("key E has been pressed");

            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            Debug.Log("ExerciseBehaviour End called");

            //===
            //NEW
            BehaviourDebugUI.SetDebugUI();
            //BehaviourDebugUI.SetGymDebugUI(false);
            //===

            base.EndBehaviour();
        }

        public override void OnCollisionEnter()
        {
            base.OnCollisionEnter();
        }
    }
}