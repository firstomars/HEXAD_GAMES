using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseBehaviour : Behaviour
{
    private PlayerController PlayerController { get; set; }
    public ExerciseBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("ExerciseBehaviour Start called - press E to test update");

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //Debug.Log("ExerciseBehaviour Update called");

        if (Input.GetKeyDown(KeyCode.E))
            Debug.Log("key E has been pressed");

        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("ExerciseBehaviour End called");
        base.EndBehaviour();
    }

    public override void OnCollisionEnter()
    {
        base.OnCollisionEnter();
    }
}