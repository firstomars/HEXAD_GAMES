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
        //PlayerController.CameraSwitch();
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
}
