using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BehaviourStateMachine
{
    #region Behaviours Setup

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

    private Behaviour currentBehaviour;
    private Behaviour nextBehaviour;

    #endregion

    #region Camera Class
    private GameObject camMgr;
    private CameraManager CameraManager;
    #endregion

    #region Time Controller Class
    //time controller class
    private GameObject timeCtrlr;
    [HideInInspector] public TimeController TimeController { get; private set; }
    #endregion

    #region Trophy Controller Class
    //trophy controller class
    private GameObject trophyCtrlr;
    [HideInInspector] public TrophyController TrophyController { get; private set; }
    #endregion

    //room trigger bools
    private bool isPlayerInGym = false;
    private bool isPlayerInKitchen = false;
    private bool isPlayerInBedroom = false;
    private bool isPlayerInBathroom = false;
    private bool isPlayerAtTrophyCabinet = false;

    //pet behaviour bools
    private bool isPetSleeping = false;
    private bool isReportDelivered = true;
    private bool isPetSeeking = false;

    [Header("NavMesh Settings")]
    private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsPlayer, whatIsGround;
    [SerializeField] public Vector3 targetPosition;
    [HideInInspector] public Transform bed;
    [HideInInspector] public Transform trophyCabinetPosition;
    [HideInInspector] public HouseWaypoints HouseWaypoints;

    [Header("Player Stats")]
    [SerializeField] private GameObject playerStatsGameObject;
    [HideInInspector] public PlayerStatistics PlayerStatistics { get; private set; }

    [Header("Player Stats Logic")]
    [SerializeField] public float minEnergyLevelToGym;
    [SerializeField] public int petBedTime;
    [SerializeField] public int petWakeUpTime;

    // Start is called before the first frame update
    void Start()
    {
        #region Set Up Behaviour Classes

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

        //set-up navmesh agent
        agent = GetComponent<NavMeshAgent>();

        #region Connect Camera, Time, Trophy Classes
        //connect player stats
        PlayerStatistics = playerStatsGameObject.GetComponent<PlayerStatistics>();

        //links Camera Manager to player in run time
        if (camMgr == null)
            camMgr = GameManager.Instance.world.transform.GetChild(0).gameObject;
        CameraManager = camMgr.GetComponent<CameraManager>();

        //links Time Controller to player in run time
        if (timeCtrlr == null)
            timeCtrlr = GameManager.Instance.world.transform.GetChild(1).gameObject;
        TimeController = timeCtrlr.GetComponent<TimeController>();

        //links Tropher Controller to player in run time
        if (trophyCtrlr == null)
            trophyCtrlr = GameManager.Instance.world.transform.GetChild(3).gameObject;
        TrophyController = trophyCtrlr.GetComponentInChildren<TrophyController>();

        #endregion

        //set starting behaviour
        currentBehaviour = SeekBehaviour;
        SetBehaviour(currentBehaviour);

        //set camera at start
        CameraSwitch();
    }

    private void Update()
    {
        //countDownTimer -= Time.deltaTime;

        //Debug.Log(countDownTimer);

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
        if (isPetSleeping)
        {
            return;
        }
        else if (!isReportDelivered)
        {
            nextBehaviour = StatusCheckBehaviour;
        }
        //else if (HasPlayerReachedDestination() && IsCountDownComplete()) //
        //{
        //    //Debug.Log("agent reached destination - call wander now");
        //    nextBehaviour = WanderBehaviour;
        //}
        else if(!isPlayerInBathroom && !isPlayerInBedroom && !isPlayerInGym && !isPlayerInKitchen && !isPlayerAtTrophyCabinet)
        {
            nextBehaviour = SeekBehaviour;
            ResetCountDownTimer();
        }
        //if mouse clicked or not in any action rooms, set to seekbehaviour
        //else if (Input.GetMouseButtonDown(0) && isClickPointOnGround(Input.mousePosition) ||
        //    !isPlayerInBathroom && !isPlayerInBedroom && !isPlayerInGym && !isPlayerInKitchen && !isPlayerAtTrophyCabinet)
        //{
        //    //Debug.Log("seek behaviour set");
        //    nextBehaviour = SeekBehaviour;
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha1) ||
        //    !isPlayerInBathroom && !isPlayerInBedroom && !isPlayerInGym && !isPlayerInKitchen && !isPlayerAtTrophyCabinet)
        //{
        //    nextBehaviour = SeekBehaviour;
        //}
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
        //else if (Input.GetKeyDown(KeyCode.Alpha1)) // set to top - UNUSED BEHAVIOURS
        //{
        //    Debug.Log("key 1 pressed");
        //    nextBehaviour = WanderBehaviour;
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Debug.Log("key 2 pressed");
        //    nextBehaviour = MoodBehaviour;
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    Debug.Log("key 4 pressed");
        //    nextBehaviour = ConverseBehaviour;
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    Debug.Log("key 9 pressed");
        //    nextBehaviour = EmoteBehaviour;
        //}
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
        //gameObject.transform.LookAt(agent.steeringTarget);
    }

    public bool HasPlayerReachedDestination()
    {
        if (Vector3.Distance(agent.destination, gameObject.transform.position) < 3.0f)
        {
            return true;
        }
        return false;
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
                isPlayerInGym = true;
                isPlayerInKitchen = false;
                isPlayerInBedroom = false;
                isPlayerInBathroom = false;
                isPlayerAtTrophyCabinet = false;
                break;

            case "kitchen":
                isPlayerInGym = false;
                isPlayerInKitchen = true;
                isPlayerInBedroom = false;
                isPlayerInBathroom = false;
                isPlayerAtTrophyCabinet = false;
                break;

            case "bedroom":
                isPlayerInGym = false;
                isPlayerInKitchen = false;
                isPlayerInBedroom = true;
                isPlayerInBathroom = false;
                isPlayerAtTrophyCabinet = false;
                break;

            case "bathroom":
                isPlayerInGym = false;
                isPlayerInKitchen = false;
                isPlayerInBedroom = false;
                isPlayerInBathroom = true;
                isPlayerAtTrophyCabinet = false;
                break;

            case "trophycabinet":
                Debug.Log("player at trophy cabinet"); //DELETE
                isPlayerInGym = false;
                isPlayerInKitchen = false;
                isPlayerInBedroom = false;
                isPlayerInBathroom = false;
                isPlayerAtTrophyCabinet = true;
                break;

            default:
                isPlayerInGym = false;
                isPlayerInKitchen = false;
                isPlayerInBedroom = false;
                isPlayerInBathroom = false;
                isPlayerAtTrophyCabinet = false;
                break;
        }
    }

    public void IsPetSleeping(bool value)
    {
        isPetSleeping = value;
    }

    public void IsReportDelivered(bool value)
    {
        isReportDelivered = value;
    }

    public bool IsPetAbleToSeek()
    {
        if (!isPetSleeping && isReportDelivered)
        {
            isPetSeeking = true;
            return true;
        }
        else return false;
    }

    private bool IsPetAbleToWander()
    {
        return false;
    }

    private float countDownTimer = 1.0f;
    private float maxCountDown = 1.0f;

    private bool IsCountDownComplete()
    {
        if (countDownTimer < 0)
            return true;

        return false;
    }

    private void ResetCountDownTimer()
    {
        countDownTimer = maxCountDown;
    }
}
