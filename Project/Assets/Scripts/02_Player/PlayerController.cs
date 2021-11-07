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
    private GameObject camMgr;
    private CameraManager CameraManager;

    //navigation variables for camera
    //private Vector3 currentTarget;          // DELETE
    //private bool isTargetReached = true;    // DELETE

    [Header("NavMesh Settings")]
    private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsPlayer, whatIsGround;
    [SerializeField] public Vector3 targetPosition;

    //===
    //NEW
    //===
    private bool isPlayerInGym = false;
    private bool isPlayerInKitchen = false;
    private bool isPlayerInBedroom = false;
    private bool isPlayerInBathroom = false;
    //===

    [Header("Player Stats UI")]
    [SerializeField] private GameObject playerStatsUI;

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

        //links Camera Manager to player in run time
        if (camMgr == null)
            camMgr = GameManager.Instance.world.transform.GetChild(0).gameObject;

        CameraManager = camMgr.GetComponent<CameraManager>();

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
        //if mouse clicked or not in any action rooms, set to seekbehaviour
        if (Input.GetMouseButtonDown(0) && isClickPointOnGround(Input.mousePosition) ||
            !isPlayerInBathroom && !isPlayerInBedroom && !isPlayerInGym && !isPlayerInKitchen)
        {
            //Debug.Log("seek behaviour set");
            nextBehaviour = SeekBehaviour;
        }
        else if (isPlayerInKitchen)
        {
            nextBehaviour = EatBehaviour;
        }
        else if (isPlayerInGym) 
        {
            //Debug.Log("gym behaviour set");
            nextBehaviour = ExerciseBehaviour;
        }
        else if (isPlayerInBedroom)
        {
            //Debug.Log("sleep behaviour set");
            nextBehaviour = SleepBehaviour;
        }
        else if (isPlayerInBathroom)
        {
            //Debug.Log("bathroom behaviour set");
            nextBehaviour = BathroomBehaviour;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) // set to top
        {
            Debug.Log("key 1 pressed");
            nextBehaviour = WanderBehaviour;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("key 2 pressed");
            nextBehaviour = MoodBehaviour;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("key 4 pressed");
            nextBehaviour = ConverseBehaviour;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("key 6 pressed");
            nextBehaviour = StatusCheckBehaviour;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log("key 9 pressed");
            nextBehaviour = EmoteBehaviour;
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

    public void TogglePlayerStats(bool value)
    {
        playerStatsUI.SetActive(value);
    }

}
