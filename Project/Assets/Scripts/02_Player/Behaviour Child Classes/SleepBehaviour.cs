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
        Debug.Log("SleepBehaviour Start called - press L to test update");

        //petBedTime = PlayerController.petBedTime;

        UIManager.UIManagerInstance.CurrentBehaviour = this;

        SetUI("bedroom");

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //Debug.Log("SleepBehaviour Update called");

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (PlayerController.TimeController.IsTimeAfter(PlayerController.petBedTime))
            {
                Debug.Log("player goes to sleep");
            }
            else
            {
                Debug.Log("it's not pet's bed time yet!");
            }
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
        if (PlayerController.TimeController.IsTimeAfter(PlayerController.petBedTime))
        {
            Debug.Log("Pet sent to bed");
            if (dayFellAsleep == -1) dayFellAsleep = PlayerController.TimeController.GetGameDate();
            PlayerController.IsPetSleeping(true);
            PlayerController.SetPlayerDestination(PlayerController.bed.position);
            UIManager.UIManagerInstance.SendToBedBtnClicked();
        }
        else
        {
            Debug.Log("it's not pet's bed time yet!");
        }
    }

    public override void WakePetUp()
    {
        Debug.Log("pet woken up");
        PlayerController.IsPetSleeping(false);

        if (PlayerController.TimeController.IsNextDay(dayFellAsleep) && 
            PlayerController.TimeController.IsTimeAfter(PlayerController.petWakeUpTime))
        {
            UIManager.UIManagerInstance.WakeUpBtnClicked();
            PlayerController.SetPlayerDestination(PlayerController.trophyCabinetPosition.position);
            dayFellAsleep = -1;
        }
    }
}

