using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseBehaviour : Behaviour
{
    private bool hasBeenInGym = false;
    private bool isRoomUISet = false;

    public ExerciseBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("ExerciseBehaviour Start called - press E to test update");
        UIManager.UIManagerInstance.CurrentBehaviour = this;
        DialogueManager.DialogueManagerInstance.CurrentBehaviour = this;


        SetUI("gym");

        //if (!hasBeenInGym) DialogueManager.DialogueManagerInstance.PetConversation("Gym");
        //else SetUI("gym");

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //Debug.Log("ExerciseBehaviour Update called");

        //if (DialogueManager.DialogueManagerInstance.currentConversationComplete) hasBeenInGym = true;

        //if (!isRoomUISet && hasBeenInGym)
        //{
        //    SetUI("gym");
        //    isRoomUISet = true;
        //}

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

        PlayerController.PlayerAnimations.Workout();

        PlayerController.PlayerStatistics.BenchPressStatsImpact();
        AudioManager.AudioManagerInstance.PlaySound("Gym");
    }

    public override void StartConversation()
    {
        DialogueManager.DialogueManagerInstance.PetConversation("Gym");
    }
}