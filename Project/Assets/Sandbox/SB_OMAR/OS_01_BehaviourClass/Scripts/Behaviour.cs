using System.Collections;

namespace Sandbox.Omar.Behaviour
{
    public abstract class Behaviour
    {
        protected PlayerController PlayerController;

        public Behaviour(PlayerController playerController)
        {
            PlayerController = playerController;
        }

        public virtual void Start()
        {
            Update();
        }

        public virtual void Update()
        {
            
        }

        public virtual void End()
        {
            
        }
    }
}


