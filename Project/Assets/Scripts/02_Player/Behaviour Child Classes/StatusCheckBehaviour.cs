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
        Debug.Log("StatusCheckBehaviour Start called - press S to test update");
        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //Debug.Log("StatusCheckBehaviour Update called");

        if (Input.GetKeyDown(KeyCode.S))
            Debug.Log("key S has been pressed");

        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("StatusCheckBehaviour End called");
        base.EndBehaviour();
    }
}
