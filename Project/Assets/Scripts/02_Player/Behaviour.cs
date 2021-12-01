using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour
{
    //connected with parent controller class so multiple GOs can use state machine
    protected BehaviourStateMachine BehaviourStateMachine;

    protected PlayerController PlayerController { get; set; }

    //returns GO attached to BehaviourStateMachine (see line 18 in ExerciseBehaviour.cs)
    protected GameObject GameObject { get; private set; }

    public Behaviour(BehaviourStateMachine behaviourStateMachine)
    {
        BehaviourStateMachine = behaviourStateMachine;
        GameObject = BehaviourStateMachine.gameObject;
        PlayerController = GameObject.GetComponent<PlayerController>();
    }

    public virtual void StartBehaviour()
    {

    }

    public virtual void RunBehaviour()
    {
        PlayerController.CameraSwitch();
    }

    public virtual void EndBehaviour()
    {
        UIManager.UIManagerInstance.CurrentBehaviour = null;
    }

    public virtual void SetUI(string room = default)
    {
        UIManager.UIManagerInstance.SwitchPlayRoomUI(room);
    }

    //monobehaviour functions accessed by behaviour classes
    public virtual void OnCollisionEnter()
    {

    }

    public virtual void OnCollisionExit()
    {

    }

    public virtual void SendToBed()
    {

    }

    public virtual void WakePetUp()
    {

    }

    public virtual void PlayMiniGame()
    {

    }

    public virtual void SeekKitchen()
    {

    }

    public virtual void SeekGym()
    {

    }

    public virtual void SeekBathroom()
    {

    }
    public virtual void SeekBedroom()
    {

    }

    public virtual void SeekTrophyCabinet()
    {

    }

    public virtual void SeekLivingRoom()
    {

    }

    public virtual Vector3 FindWaypointHelper(string room)
    {
        foreach (var waypoint in PlayerController.HouseWaypoints.waypoints)
        {
            if (waypoint.name == room) return waypoint.position;
        }
        return Vector3.zero;
    }

    public virtual void EatFood()
    {

    }

    public virtual void EatJunkFood()
    {

    }

    public virtual void BenchPress()
    {

    }

    #region Conversations

    public virtual void StartConversation()
    {

    }

    public virtual void StartConversationWakeUp()
    {

    }

    public virtual void StartConversationMinigame()
    {

    }

    #endregion
}
