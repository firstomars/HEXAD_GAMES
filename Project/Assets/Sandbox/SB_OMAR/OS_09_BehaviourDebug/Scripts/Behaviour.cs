using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sandbox.Omar.BehaviourDebug
{
    public abstract class Behaviour
    {
        //connected with parent controller class so multiple GOs can use state machine
        protected BehaviourStateMachine BehaviourStateMachine;
        
        //returns GO attached to BehaviourStateMachine (see line 18 in ExerciseBehaviour.cs)
        protected GameObject GameObject { get; private set; }

        public Behaviour(BehaviourStateMachine behaviourStateMachine)
        {
            BehaviourStateMachine = behaviourStateMachine;
            GameObject = BehaviourStateMachine.gameObject;
        }

        public virtual void StartBehaviour()
        {
        }

        public virtual void RunBehaviour()
        {
            
        }

        public virtual void EndBehaviour()
        {
            
        }

        //monobehaviour functions accessed by behaviour classes
        public virtual void OnCollisionEnter()
        {

        }

        public virtual void OnCollisionExit()
        {

        }

    }
}