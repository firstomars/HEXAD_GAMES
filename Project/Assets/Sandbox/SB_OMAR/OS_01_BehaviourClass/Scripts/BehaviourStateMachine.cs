using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.Behaviour
{
    public class BehaviourStateMachine : MonoBehaviour
    {
        protected Behaviour Behaviour;

        private void Start()
        {
            
        }

        public void SetBehaviour(Behaviour behaviour)
        {
            Behaviour = behaviour;
            Behaviour.StartBehaviour();
        }
         
        public void UpdateBehaviour()
        {
            Behaviour.RunBehaviour();
        }

        public void EndBehaviour()
        {
            Behaviour.EndBehaviour();
        }
    }

}

