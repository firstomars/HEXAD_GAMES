using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BehaviourStateMachine
{
    #region Behaviours Setup

    private Behaviour IntroductionBehaviour;
    private Behaviour ExerciseBehaviour;
    private Behaviour EatBehaviour;
    private Behaviour StatusCheckBehaviour;
    private Behaviour BathroomBehaviour;
    private Behaviour SeekBehaviour;
    private Behaviour SleepBehaviour;
    private Behaviour WanderBehaviour;
    private Behaviour ViewTrophyBehaviour;

    private Behaviour currentBehaviour;
    private Behaviour nextBehaviour;

    #endregion

    #region Camera Class

    private GameObject camMgr;
    [HideInInspector] public CameraManager CameraManager;

    #endregion

    #region Time Controller Class
    
    private GameObject timeCtrlr;
    [HideInInspector] public TimeController TimeController { get; private set; }

    #endregion

    #region Trophy Controller Class
    
    private GameObject trophyCtrlr;
    [HideInInspector] public TrophyController TrophyController { get; private set; }

    #endregion

    //room trigger bools
    private bool isPlayerInGym = false;
    private bool isPlayerInKitchen = false;
    private bool isPlayerInBedroom = false;
    private bool isPlayerInBathroom = false;
    private bool isPlayerAtTrophyCabinet = false;
    private bool isPlayerSleeping = false;

    //pet behaviour bools
    [SerializeField] public bool hasIntroHappened = false;
    private bool isPetSleeping = false;
    private bool isReportDelivered = true;
    private bool isPetSeeking = false;

    //player animations
    [HideInInspector] public PlayerAnimations PlayerAnimations;

    [Header("NavMesh Settings")]
    private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsPlayer, whatIsGround;
    [SerializeField] public Vector3 targetPosition;
    [HideInInspector] public Transform bedPos;
    [HideInInspector] public Transform miniGamePos;
    [HideInInspector] public Transform trophyCabinetPosition; // DELETE
    [HideInInspector] public HouseWaypoints HouseWaypoints;
    [HideInInspector] public WanderWaypoints WanderWaypoints;

    [Header("Player Stats")]
    [SerializeField] private GameObject playerStatsGameObject;
    [HideInInspector] public PlayerStatistics PlayerStatistics { get; private set; }

    [Header("Player Stats Logic")]
    [SerializeField] public float minEnergyLevelToGym;
    [SerializeField] public int petBedTime;
    [SerializeField] public int petWakeUpTime;
    
    [Header("Player Cosmetics")]
    [SerializeField] private GameObject petBody;
    [HideInInspector] public Renderer petSkinnedMeshRenderer;
    [SerializeField] private GameObject Sweatband;
    [SerializeField] private GameObject RightDumbell;
    [SerializeField] private GameObject LeftDumbell;
    [SerializeField] private GameObject UnhealthyFood;
    [SerializeField] private GameObject HealthyFood;

    private int roomCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //set-up behaviour classes
        IntroductionBehaviour   = new IntroductionBehaviour(this);
        ExerciseBehaviour       = new ExerciseBehaviour(this);
        EatBehaviour            = new EatBehaviour(this);
        StatusCheckBehaviour    = new StatusCheckBehaviour(this);
        BathroomBehaviour       = new BathroomBehaviour(this);
        SeekBehaviour           = new SeekBehaviour(this);
        SleepBehaviour          = new SleepBehaviour(this);
        WanderBehaviour         = new WanderBehaviour(this);
        ViewTrophyBehaviour     = new ViewTrophyBehaviour(this);

        #region REGION : Get Components - PlayerStatistics, PlayerAnimations, Renderer, NavMeshAgent

        //set-up navmesh agent
        agent = GetComponent<NavMeshAgent>();

        //connect player stats
        PlayerStatistics = playerStatsGameObject.GetComponent<PlayerStatistics>();

        //set-up pet renderers
        petSkinnedMeshRenderer = petBody.GetComponent<Renderer>();

        //connect player animations class
        PlayerAnimations = GetComponent<PlayerAnimations>();

        #endregion

        #region REGION : Runtime Connect - CameraManager, TimeController, TrophyController

        //links Camera Manager to player in run time
        if (camMgr == null) camMgr = GameManager.Instance.world.transform.GetChild(0).gameObject;
        CameraManager = camMgr.GetComponent<CameraManager>();

        //links Time Controller to player in run time
        if (timeCtrlr == null) timeCtrlr = GameManager.Instance.world.transform.GetChild(1).gameObject;
        TimeController = timeCtrlr.GetComponent<TimeController>();

        //links Tropher Controller to player in run time
        if (trophyCtrlr == null) trophyCtrlr = GameManager.Instance.world.transform.GetChild(3).gameObject;
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
        //run animations and audio
        if (agent.remainingDistance > (agent.stoppingDistance + 0.1f)) PetStartsWalking();
        else PetStopsWalking();

        //determine next behaviour
        if (!hasIntroHappened)              nextBehaviour = IntroductionBehaviour;
        else if (isPetSleeping)             return;
        else if (!isReportDelivered)        nextBehaviour = StatusCheckBehaviour;
        else if(isPetSeeking)               nextBehaviour = SeekBehaviour;
        else if (isPlayerInKitchen)         nextBehaviour = EatBehaviour;
        else if (isPlayerInGym)             nextBehaviour = ExerciseBehaviour;
        else if (isPlayerInBedroom)         nextBehaviour = SleepBehaviour;
        else if (isPlayerInBathroom)        nextBehaviour = BathroomBehaviour;
        else if (isPlayerAtTrophyCabinet)   nextBehaviour = ViewTrophyBehaviour;
        else                                nextBehaviour = WanderBehaviour;
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
                isPlayerSleeping = false;
                break;

            case "bedroomSleep":
                isPlayerInGym = false;
                isPlayerInKitchen = false;
                isPlayerInBedroom = false;
                isPlayerInBathroom = false;
                isPlayerAtTrophyCabinet = false;
                isPlayerSleeping = true;
                break;

            case "bathroom":
                isPlayerInGym = false;
                isPlayerInKitchen = false;
                isPlayerInBedroom = false;
                isPlayerInBathroom = true;
                isPlayerAtTrophyCabinet = false;
                break;

            case "trophycabinet":
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

    private void PetStartsWalking()
    {
        //Debug.Log("idle to walk");
        if (!AudioManager.AudioManagerInstance.IsAudioPlaying("FootStep"))
            AudioManager.AudioManagerInstance.PlaySound("FootStep");

        PlayerAnimations.IdleToWalk();
    }

    private void PetStopsWalking()
    {
        PlayerAnimations.WalkToIdle();
        //bug.Log("Walk to Idle called");

        if (AudioManager.AudioManagerInstance.IsAudioPlaying("FootStep"))
            AudioManager.AudioManagerInstance.StopSound("FootStep");

        if (isPetSleeping) FaceTarget(bedPos.position);
        else FaceTarget(Camera.main.transform.position);
    }


    //set player destination
    public void SetPlayerDestination(Vector3 targetPos)
    {
        agent.SetDestination(targetPos);
    }

    public bool HasPlayerReachedDestination()
    {
        if (agent.remainingDistance < 1.0f)
            return true;
        return false;
    }

    public Vector3 GetAgentPosition()
    {
        return agent.transform.position;
    }

    public void CameraSwitch()
    {
        CameraManager.SwitchCamera();
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.05f);
    }

    public void WaitForTimeBeforeSettingDestination(float secs, Vector3 destination = default)
    {
        StartCoroutine(WaitBeforeSleepEnds(secs, destination));
    }

    private IEnumerator WaitBeforeSleepEnds(float secs, Vector3 destination)
    {
        yield return new WaitForSeconds(secs);
        IsPetSleeping(false);
        SetPlayerDestination(destination);
    }

    public IEnumerator DelayedCallback(Action<bool> callBack, float secs)
    {
        Debug.Log("Delayed Callback successfully called");
        yield return new WaitForSeconds(secs);
        callBack(true);
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

    public void IsPetSeeking(bool value)
    {
        isPetSeeking = value;
    }

    //===
    //don't include below in portfolio

    public void ActivateGymGearObjs(bool value)
    {
        Sweatband.SetActive(value);
        RightDumbell.SetActive(value);
        LeftDumbell.SetActive(value);
    }

    public void ActivateFoodObj(string foodType, bool value)
    {
        if (foodType == "healthy") HealthyFood.SetActive(value);
        if (foodType == "unhealthy") UnhealthyFood.SetActive(value);
    }

    public void IncreaseRoomCounter()
    {
        roomCounter++;
        Debug.Log(roomCounter);

        //if (roomCounter > 2 && !UIManager.UIManagerInstance.hasSpiritLevelBeenPressedForFirstTime)
        //{
        //    DialogueManager.DialogueManagerInstance.PetConversation("SpiritLevelFirstClick");
        //    UIManager.UIManagerInstance.SetSpiritLevelFirstClick();
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
