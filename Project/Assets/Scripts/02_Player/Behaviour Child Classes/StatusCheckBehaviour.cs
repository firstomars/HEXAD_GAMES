using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCheckBehaviour : Behaviour
{
    public StatusCheckBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("StatusCheckBehaviour Start called");
        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //Debug.Log("StatusCheckBehaviour Update called");

        PlayerController.TrophyController.TrophyConditionCheck();

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("key T has been pressed");
            PlayerController.TrophyController.InstantiateTrophy();
        }

        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("StatusCheckBehaviour End called");
        base.EndBehaviour();
    }
}
