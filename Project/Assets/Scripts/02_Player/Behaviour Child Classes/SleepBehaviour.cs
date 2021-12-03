using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBehaviour : Behaviour
{
    private int dayFellAsleep = -1;
    private bool isPetNapping = false;
    private int napStartTime = -1;


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
        if (isPetNapping && 
            TimeController.GetGameTime() >= napStartTime + 2)
        {
            WakePetUpFromNap();
        }
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
        if (PlayerStatistics.energyLevel <= PlayerStatistics.energyLevelSleepThreshold)
        {
            Debug.Log("Pet sent to bed");

            if (dayFellAsleep == -1)
            {
                if (TimeController.GetGameTime() > 12)
                    dayFellAsleep = TimeController.GetGameDate();
                else dayFellAsleep = TimeController.GetGameDate() - 1; 
                // logic breaks if game date is 1st day of month
            }

            SendToBedSequence();

            //PlayerController.SetPlayerDestination(PlayerController.bedPos.position);
            //SwitchCamera("bedroomSleep");
            //PlayerAnimations.GetIntoBed();

            //AudioManager.AudioManagerInstance.PlaySound("Sleeping");
            //AudioManager.AudioManagerInstance.StopSound("FootStep");

            UIManager.SendToBedBtnClicked();
        }
        else
        {
            //refactor
            Debug.Log("it's not pet's bed time yet!");
            DialogueManager.PetConversation("DontNeedSleep");
        }
    }

    public override void SendToBedForNap()
    {
        Debug.Log("sleep behaviour.cs nap");
        isPetNapping = true;

        SendToBedSequence();

        napStartTime = TimeController.GetGameTime();
        UIManager.SendToBedForNapBtnClicked();
    }

    private void SendToBedSequence()
    {
        PlayerController.IsPetSleeping(true);
        PlayerController.SetPlayerDestination(PlayerController.bedPos.position);
        SwitchCamera("bedroomSleep");
        PlayerAnimations.GetIntoBed();

        AudioManager.AudioManagerInstance.PlaySound("Sleeping");
        AudioManager.AudioManagerInstance.StopSound("FootStep");
    }

    private void WakeUpSequence()
    {
        PlayerAnimations.GetOutOfBed();
        SwitchCamera("bedroom");

        PlayerController.IsPetSleeping(false);
        Debug.Log("pet woken up");

        AudioManager.AudioManagerInstance.StopSound("Sleeping");
        UIManager.WakeUpBtnClicked();
    }

    public override void WakePetUpFromNap()
    {
        PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));
        napStartTime = -1;
        isPetNapping = false;

        WakeUpSequence();
    }

    public override void WakePetUp()
    {
        //check if nap or if sleeping
        //if sleeping only able to wake up when it's morning

        WakeUpSequence();

        //PlayerAnimations.GetOutOfBed();
        //SwitchCamera("bedroom");

        //PlayerController.IsPetSleeping(false);
        //Debug.Log("pet woken up");

        //AudioManager.AudioManagerInstance.StopSound("Sleeping");
        //UIManager.WakeUpBtnClicked();

        if (TimeController.IsNextDay(dayFellAsleep) &&
            TimeController.IsTimeBefore(13))
        {
            UIManager.WakeUpNextDayBtnClicked();
            PlayerController.IsReportDelivered(false);
        }
        else PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));

        dayFellAsleep = -1;
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

    public override void StartConversationWakeUpFromNap()
    {
        DialogueManager.PetConversation("BedroomWakeUpFromNap");
        
        //DialogueManager.PetConversation("BedroomWakeUp");
    }

    public override void StartConversationMinigame()
    {
        DialogueManager.PetConversation("BedroomMinigame");
    }

    private void SwitchCamera(string room)
    {
        PlayerController.CameraManager.SetPlayerPosition(room);
        PlayerController.CameraSwitch();
    }

}

