using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBehaviour : Behaviour
{
    private int dayFellAsleep = -1;

    public SleepBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("SleepBehaviour Start called");

        UIManager = UIManager.UIManagerInstance;
        DialogueManager = DialogueManager.DialogueManagerInstance;
        PlayerAnimations = PlayerController.PlayerAnimations;
        PlayerStatistics = PlayerController.PlayerStatistics;
        TimeController = PlayerController.TimeController;

        UIManager.CurrentBehaviour = this;
        DialogueManager.CurrentBehaviour = this;

        SetUI("bedroom");
        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("SleepBehaviour End called");
        
        SetUI();
        
        base.EndBehaviour();
    }

    public override void SendToBed()
    {
        if (TimeController.IsTimeAfter(PlayerController.petBedTime))
        {
            SwitchCamera("bedroomSleep");

            PlayerController.IsPetSleeping(true);

            PlayerAnimations.GetIntoBed();

            Debug.Log("Pet sent to bed");
            if (dayFellAsleep == -1) dayFellAsleep = TimeController.GetGameDate();
            PlayerController.SetPlayerDestination(PlayerController.bedPos.position);

            //PLACE IN PLAYERANIMATIONS
            AudioManager.AudioManagerInstance.PlaySound("Sleeping");
            AudioManager.AudioManagerInstance.StopSound("FootStep");

            UIManager.SendToBedBtnClicked();
        }
        else
        {
            //refactor
            Debug.Log("it's not pet's bed time yet!");
            DialogueManager.PetConversation("DontNeedSleep");
        }
    }

    private void SwitchCamera(string room)
    {
        PlayerController.CameraManager.SetPlayerPosition(room);
        PlayerController.CameraSwitch();
    }

    public override void WakePetUp()
    {
        PlayerAnimations.GetOutOfBed();
        SwitchCamera("bedroom");

        PlayerController.IsPetSleeping(false);
        Debug.Log("pet woken up");

        AudioManager.AudioManagerInstance.StopSound("Sleeping");
        UIManager.WakeUpBtnClicked();

        if (TimeController.IsNextDay(dayFellAsleep) && 
            TimeController.IsTimeAfter(PlayerController.petWakeUpTime))
        {
            UIManager.WakeUpNextDayBtnClicked();
            PlayerController.IsReportDelivered(false);
            dayFellAsleep = -1;
        }
        else
        {
            PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));
        }
    }

    public override void PlayMiniGame()
    {
        if (PlayerStatistics.energyLevel >= 30)
        {
            PlayerController.SetPlayerDestination(PlayerController.miniGamePos.position);
            UIManager.ActivateBedroomControl(false);

            SwitchCamera("bedroomGame");
            PlayerAnimations.PlayMinigame();
            PlayerStatistics.MinigameStatsImpact();

            if (TimeController.IsTimeAfter(16))
            {
                DialogueManager.DisplayTip("BlueLightFilter");
                PlayerController.StartCoroutine(ExecuteMiniGameUI(6));
            }
            else PlayerController.StartCoroutine(ExecuteMiniGameUI(0));
        }
        else
        {
            Debug.Log("pet too tired to play minigames");
            DialogueManager.PetConversation("BedroomTooTiredForGames");
        }

    }

    IEnumerator ExecuteMiniGameUI(float secsToWait)
    {
        yield return new WaitForSeconds(secsToWait);

        UIManager.MinigameClicked(true);
    }

    public override void StopMiniGame()
    {
        PlayerAnimations.StopMinigame();
        PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));
        UIManager.MinigameClicked(false);
        UIManager.ActivateBedroomControl(true);
        SwitchCamera("bedroom");
    }

    public override void StartConversation()
    {
        DialogueManager.PetConversation("Bedroom");
    }

    public override void StartConversationWakeUp()
    {
        DialogueManager.PetConversation("BedroomWakeUp");
    }

    public override void StartConversationMinigame()
    {
        DialogueManager.PetConversation("BedroomMinigame");
    }
}

