using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionBehaviour : Behaviour
{
    private bool hasPetColourBeenSet = false;

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
        if (!hasPetColourBeenSet && UIManager.UIManagerInstance.isPetColourSet)
        {
            PlayerController.petSkinnedMeshRenderer.material = UIManager.UIManagerInstance.playerMaterial;
            hasPetColourBeenSet = true;
        }

        if (DialogueManager.DialogueManagerInstance.currentConversationComplete)
        {
            PlayerController.hasIntroHappened = true;
        }
            
        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        PlayerController.PlayerStatistics.SetInitialSleepTimesAndGoals(UIManager.UIManagerInstance.timeEntries);
        
        Debug.Log("ExerciseBehaviour End called");
        base.EndBehaviour();
    }
}
