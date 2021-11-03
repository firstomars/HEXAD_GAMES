using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Omar.BehaviourDebug
{
    public class PlayerController : BehaviourStateMachine //this is derived from monobehaviour
    {
        #region BehaviourClasses

        private Behaviour ExerciseBehaviour;
        private Behaviour ConverseBehaviour;
        private Behaviour EatBehaviour;
        private Behaviour StatusCheckBehaviour;
        private Behaviour BathroomBehaviour;
        private Behaviour SeekBehaviour;
        private Behaviour SleepBehaviour;
        private Behaviour WanderBehaviour;
        private Behaviour MoodBehaviour;
        private Behaviour EmoteBehaviour;

        #endregion

        private Behaviour currentBehaviour;
        private Behaviour nextBehaviour;

        [Header("NavMesh Settings")]
        private NavMeshAgent agent;
        [SerializeField] private LayerMask whatIsPlayer, whatIsGround;
        [SerializeField] public Vector3 targetPosition;

        // Start is called before the first frame update
        private void Start()
        {
            #region SetUpBehaviourClasses

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

            #endregion

            agent = GetComponent<NavMeshAgent>();

            //set starting behaviour
            currentBehaviour = WanderBehaviour;
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
                nextBehaviour = null;
                SetBehaviour(currentBehaviour);
            }

            //call behaviour's update function
            if (currentBehaviour != null) UpdateBehaviour();

            //behaviour decision logic
            RunBehaviourLogic();
        }

        private void RunBehaviourLogic()
        {
            //===
            //NEW
            if (Input.GetMouseButtonDown(0) && isClickPointOnGround(Input.mousePosition))
            {
                //Debug.Log("mouse clicked on ground");
                nextBehaviour = SeekBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("key 1 pressed");
                nextBehaviour = WanderBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("key 2 pressed");
                nextBehaviour = MoodBehaviour;
            }
            else if (isPlayerInGym)//(Input.GetKeyDown(KeyCode.Alpha3))
            {
                //Debug.Log("in gym");
                nextBehaviour = ExerciseBehaviour;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("key 4 pressed");
                nextBehaviour = ConverseBehaviour;
            }
            else if (isPlayerInKitchen) //(Input.GetKeyDown(KeyCode.Alpha5))
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
        }

        //check whether input is valid movement target for player
        private bool isClickPointOnGround(Vector3 mouseClickPos)
        {
            Ray myRay = Camera.main.ScreenPointToRay(mouseClickPos);
            RaycastHit hitInfo;

            //check if mouse click pos hits ground
            if (Physics.Raycast(myRay, out hitInfo, 100, whatIsGround))
            {
                //set target pos accessed by seek.cs
                targetPosition = hitInfo.point;
                return true;
            }
                
            return false;
        }

        //set player destination
        public void SetPlayerDestination(Vector3 targetPos)
        {
            agent.SetDestination(targetPos);
        }

        //===
        //NEW
        //===
        private bool isPlayerInGym = false;
        private bool isPlayerInKitchen = false;
        private bool isPlayerInBedroom = false;
        private bool isPlayerInBathroom = false;

        //return player gameobject
        public GameObject ReturnPlayerGameObject()
        {
            return this.gameObject;
        }

        public void SetPlayerPosition(string room = default)
        {
            switch (room)
            {
                case "gym":
                    Debug.Log("player in gym");
                    isPlayerInGym = true;
                    isPlayerInKitchen = false;
                    isPlayerInBedroom = false;
                    isPlayerInBathroom = false;
                    break;

                case "kitchen":
                    Debug.Log("player in kitchen");
                    isPlayerInGym = false;
                    isPlayerInKitchen = true;
                    isPlayerInBedroom = false;
                    isPlayerInBathroom = false;
                    break;

                case "bedroom":
                    Debug.Log("player in bedroom");
                    isPlayerInGym = false;
                    isPlayerInKitchen = false;
                    isPlayerInBedroom = true;
                    isPlayerInBathroom = false;
                    break;

                case "bathroom":
                    Debug.Log("player in bathroom");
                    isPlayerInGym = false;
                    isPlayerInKitchen = false;
                    isPlayerInBedroom = false;
                    isPlayerInBathroom = true;
                    break;

                default:
                    Debug.Log("player in house");
                    isPlayerInGym = false;
                    isPlayerInKitchen = false;
                    isPlayerInBedroom = false;
                    isPlayerInBathroom = false;
                    break;
            }
        }
    }
}
