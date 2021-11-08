using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBehaviour : Behaviour
{
    private int petBedTime;
    
    public SleepBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("SleepBehaviour Start called - press L to test update");

        petBedTime = PlayerController.petBedTime;

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //Debug.Log("SleepBehaviour Update called");

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (PlayerController.TimeController.IsTimeAfter(petBedTime))
            {
                Debug.Log("player goes to sleep");
            }
            else
            {
                Debug.Log("it's not pet's bed time yet!");
            }
        }

        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("SleepBehaviour End called");
        base.EndBehaviour();
    }
}
