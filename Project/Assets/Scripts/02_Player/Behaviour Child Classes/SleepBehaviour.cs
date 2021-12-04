using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBehaviour : Behaviour
{
    private int dayFellAsleep = -1;
    private bool isPetNapping = false;
    private int napStartTime = -1;
    private bool isPetSleeping = false;
    private bool isEnergyIncreasing = false;

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

        if (isPetSleeping &&
            TimeController.IsNextDay(dayFellAsleep) &&
            TimeController.IsTimeAfter(7) &&
            PlayerStatistics.energyLevel > 99)
        {
            PetWakesUpOnNewDay();
        }

        if (!isEnergyIncreasing &&
            (isPetNapping || isPetSleeping))
        {
            Debug.Log("Called coroutine");
            PlayerStatistics.IsPetAsleep(true);
            isEnergyIncreasing = true;
        }

        if (isEnergyIncreasing &&
            !isPetSleeping &&
            !isPetNapping)
        {
            Debug.Log("Called coroutine");
            PlayerStatistics.IsPetAsleep(false);
            isEnergyIncreasing = false;
        }
            
        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("SleepBehaviour End called");
        
        SetUI();
        
        base.EndBehaviour();
    }

    public override void SendToBedForTheDay()
    {
        if (PlayerStatistics.energyLevel > PlayerStatistics.energyLevelSleepThreshold)
        {
            Debug.Log("i'm not tired");
            DialogueManager.PetConversation("DontNeedSleep");
        }
        else if (TimeController.IsTimeAfter(7) && TimeController.IsTimeBefore(18))
        {
            Debug.Log("too early to call it a day");
            DialogueManager.PetConversation("BedroomTooEarlyToCallItADay");
        }
        else
        {
            Debug.Log("Pet sent to bed");
            isPetSleeping = true;

            if (dayFellAsleep == -1)
            {
                if (TimeController.IsTimeBefore(7)) dayFellAsleep = TimeController.GetGameDate() - 1;
                else                                dayFellAsleep = TimeController.GetGameDate();
            }

            SendToBedSequence();

            UIManager.SendToBedBtnClicked();
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

    public override void TryToWakePetUpOnNewDay()
    {
        if (PlayerStatistics.energyLevel < 80)
        {
            Debug.Log("I'm not even at 80% energy yet, I need more sleep");
            DialogueManager.PetConversation("BedroomStillTooTired");
        }
        else if (TimeController.IsTimeAfter(18) || TimeController.IsTimeBefore(5))
        {
            Debug.Log("It's not even 5 yet, I want to stay in bed for awhile");
            DialogueManager.PetConversation("BedroomItsTooEarlyToWakeUp");

        }
        else
        {
            PetWakesUpOnNewDay();
        }
    }

    private void PetWakesUpOnNewDay()
    {
        WakeUpSequence();
        UIManager.WakeUpNextDayBtnClicked();
        PlayerController.IsReportDelivered(false);
        isPetSleeping = false;
        dayFellAsleep = -1;
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
        WakeUpSequence();
        PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));
        isPetSleeping = false;

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

