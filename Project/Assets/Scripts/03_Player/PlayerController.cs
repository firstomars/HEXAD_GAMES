using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BehaviourStateMachine
{
    //behvaviour set-up
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

    //camera set-up
    [Header("Cameras")]
    [SerializeField] private GameObject camMgr;
    private CameraManager CameraManager;

    //navigation variables for camera
    private Vector3 currentTarget;
    private bool isTargetReached = true;

    [Header("NavMesh Settings")]
    private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsPlayer, whatIsGround;
    [SerializeField] public Vector3 targetPosition;



    // Start is called before the first frame update
    void Start()
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

        //REWORK
        camMgr = GameManager.Instance.house.transform.GetChild(3).gameObject;
        CameraManager = camMgr.GetComponent<CameraManager>();


        //GameObject ChildGameObject1 = ParentGameObject.transform.GetChild(1).gameObject;


        //set starting behaviour
        currentBehaviour = WanderBehaviour;
        SetBehaviour(currentBehaviour);
    }

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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("key 1 pressed");
            nextBehaviour = WanderBehaviour;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("key 2 pressed");
            nextBehaviour = MoodBehaviour;
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
        else if (Input.GetMouseButtonDown(0) && isClickPointOnGround(Input.mousePosition))
        {
            Debug.Log("mouse clicked on ground");
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

    public void CameraSwitch()
    {
        CameraManager.SwitchCamera(); //rework
    }

}
