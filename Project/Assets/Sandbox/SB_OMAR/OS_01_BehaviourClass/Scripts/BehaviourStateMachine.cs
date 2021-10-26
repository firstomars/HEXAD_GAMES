using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.Behaviour
{
    public class BehaviourStateMachine : MonoBehaviour
    {
        protected Behaviour Behaviour;

        public void SetBehaviour(Behaviour behaviour)
        {
            Behaviour = behaviour;
            Behaviour.Start();
        }
    }

}

