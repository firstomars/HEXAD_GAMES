using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseBehaviour : Behaviour
{
    private bool isExercising = false;

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

        PlayerController.ActivateGymGearObjs(false);
        PlayerAnimations.OutsideGym();

        base.EndBehaviour();
    }

    public override void BenchPress()
    {
        if (!isExercising)
        {
            PlayerController.ActivateGymGearObjs(true);

            PlayerAnimations.Workout();

            PlayerStatistics.BenchPressStatsImpact();
            AudioManager.AudioManagerInstance.PlaySound("Gym");

            isExercising = true;
        }
        else
        {
            PlayerController.ActivateGymGearObjs(false);
            AudioManager.AudioManagerInstance.StopSound("Gym");

            isExercising = false;
        }
    }

    public override void StartConversation()
    {
        DialogueManager.PetConversation("Gym");
    }
}