using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.Behaviour
{
    public class PlayerController : BehaviourStateMachine //this is derived from monobehaviour
    {
        Behaviour HopBehaviour; //DELETE
        Behaviour ShimmyBehaviour; //DELETE
        Behaviour ExerciseBehaviour;
        Behaviour ConverseBehaviour;
        Behaviour EatBehaviour;
        Behaviour StatusCheckBehaviour;
        Behaviour BathroomBehaviour;
        Behaviour SeekBehaviour;
        Behaviour SleepBehaviour;
        Behaviour WanderBehaviour;
        Behaviour MoodBehaviour;
        Behaviour EmoteBehaviour;

        Behaviour currentBehaviour;
        Behaviour nextBehaviour;

        // Start is called before the first frame update
        private void Start()
        {
            HopBehaviour = new HopBehaviour(this); //DELETE
            ShimmyBehaviour = new Shimmy(this); // DELETE

            ExerciseBehaviour = new ExerciseBehaviour(this);
            ConverseBehaviour = new ConverseBehaviour(this);
            EatBehaviour = new EatBehaviour(this);
            StatusCheckBehaviour = new StatusCheckBehaviour(this);
            BathroomBehaviour = new BathroomBehaviour(this);
            SeekBehaviour = new SeekBehaviour(this);
            SleepBehaviour = new SleepBehaviour(this);
            WanderBehaviour = new WanderBehaviour(this);
            MoodBehaviour = new MoodBehaviour(this);
            EmoteBehaviour = new EmoteBehaviour(this);

            //sets start behaviour as Hop
            currentBehaviour = HopBehaviour;
            SetBehaviour(currentBehaviour);
        }

        // Update is called once per frame
        private void Update()
        {
            //set new behaviour if required
            if (nextBehaviour != null && nextBehaviour != currentBehaviour)
            {
                EndBehaviour();
                currentBehaviour = nextBehaviour;
                SetBehaviour(nextBehaviour);
            }

            //call behaviour's update function
            if (currentBehaviour != null) UpdateBehaviour();

            //behaviour decision logic
            RunBehaviourLogic();
        }

        private void RunBehaviourLogic()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("key 1 pressed");
                nextBehaviour = HopBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("key 2 pressed");
                nextBehaviour = ShimmyBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("key 3 pressed");
                nextBehaviour = ExerciseBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("key 4 pressed");
                nextBehaviour = ConverseBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Debug.Log("key 5 pressed");
                nextBehaviour = EatBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log("key 6 pressed");
                nextBehaviour = StatusCheckBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Debug.Log("key 7 pressed");
                nextBehaviour = BathroomBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                Debug.Log("key 8 pressed");
                nextBehaviour = SeekBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                Debug.Log("key 9 pressed");
                nextBehaviour = EmoteBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Debug.Log("key 0 pressed");
                nextBehaviour = SleepBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Minus))
            {
                Debug.Log("key Minus pressed");
                nextBehaviour = WanderBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Equals))
            {
                Debug.Log("key Equals pressed");
                nextBehaviour = MoodBehaviour;
            }
        }
    }
}
