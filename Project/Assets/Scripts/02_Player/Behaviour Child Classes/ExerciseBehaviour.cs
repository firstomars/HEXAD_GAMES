using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseBehaviour : Behaviour
{
    public ExerciseBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("ExerciseBehaviour Start called - press E to test update");
        UIManager.UIManagerInstance.CurrentBehaviour = this;
        SetUI("gym");
        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //Debug.Log("ExerciseBehaviour Update called");

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerController.PlayerStatistics.energyLevel > PlayerController.minEnergyLevelToGym)
                Debug.Log("key E has been pressed");
            else
                Debug.Log("Energy too low to work out");
        }
            
        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("ExerciseBehaviour End called");
        AudioManager.AudioManagerInstance.StopSound("Gym");
        SetUI();
        base.EndBehaviour();
    }

    public override void BenchPress()
    {
        //Debug.Log("bench press called");
        PlayerController.PlayerStatistics.BenchPressStatsImpact();
        AudioManager.AudioManagerInstance.PlaySound("Gym");
    }
}