using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sandbox.Omar.Behaviour
{
    public abstract class Behaviour //doesn't work if inherits from monobehaviour?
    {
        protected PlayerController PlayerController;

        public Behaviour(PlayerController playerController)
        {
            PlayerController = playerController;
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
    }
}


