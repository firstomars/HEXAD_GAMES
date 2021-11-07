using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBehaviour : Behaviour
{
    public EatBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("EatBehaviour Start called - press A to test update");
        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //Debug.Log("EatBehaviour Update called");

        if (Input.GetKeyDown(KeyCode.A))
            Debug.Log("key A has been pressed");

        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("EatBehaviour End called");
        base.EndBehaviour();
    }
}