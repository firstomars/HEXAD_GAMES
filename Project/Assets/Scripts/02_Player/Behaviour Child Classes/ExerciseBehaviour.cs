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
        TimeController = PlayerController.TimeController;

        UIManager.CurrentBehaviour = this;
        DialogueManager.CurrentBehaviour = this;

        SetUI("gym");

        PlayerAnimations.InsideGym();

        if (TimeController.IsTimeAfter(18)) DialogueManager.DisplayTip("ExerciseAfter6");

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

        PlayerAnimations.OutsideGym();

        base.EndBehaviour();
    }

    public override void BenchPress()
    {
        PlayerController.ActivateGymGearObjs(true);

        PlayerAnimations.Workout();

        PlayerStatistics.BenchPressStatsImpact();
        AudioManager.AudioManagerInstance.PlaySound("Gym");
    }

    public override void StartConversation()
    {
        DialogueManager.PetConversation("Gym");
    }
}