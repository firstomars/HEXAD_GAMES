using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : Behaviour
{
    private Vector3 targetPosition;

    public SeekBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("SeekBehaviour Start called");
        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //set seek destination
        if (targetPosition != PlayerController.targetPosition) SetPlayerDestination(targetPosition);
        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("SeekBehaviour End called");
        base.EndBehaviour();
    }

    private void SetPlayerDestination(Vector3 targetPos)
    {
        targetPosition = PlayerController.targetPosition;
        PlayerController.SetPlayerDestination(targetPosition);
    }
}

