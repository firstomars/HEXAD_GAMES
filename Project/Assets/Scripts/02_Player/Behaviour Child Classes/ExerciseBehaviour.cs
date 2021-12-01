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
        UIManager = UIManager.UIManagerInstance;
        DialogueManager = DialogueManager.DialogueManagerInstance;
        PlayerAnimations = PlayerController.PlayerAnimations;
        PlayerStatistics = PlayerController.PlayerStatistics;

        UIManager.CurrentBehaviour = this;
        DialogueManager.CurrentBehaviour = this;

        SetUI("gym");

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
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
        PlayerAnimations.Workout();

        PlayerStatistics.BenchPressStatsImpact();
        AudioManager.AudioManagerInstance.PlaySound("Gym");
    }

    public override void StartConversation()
    {
        DialogueManager.PetConversation("Gym");
    }
}