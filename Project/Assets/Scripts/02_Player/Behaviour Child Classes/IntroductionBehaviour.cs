using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionBehaviour : Behaviour
{
    public IntroductionBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("Intro behaviour");
        DialogueManager.DialogueManagerInstance.PetConversation("Intro");
        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("ExerciseBehaviour End called");
        base.EndBehaviour();
    }
}
