using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : Behaviour
{
    private Vector3 targetPosition;
    private string targetPos;
    private bool isTargetReached;

    public SeekBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("SeekBehaviour Start called");

        UIManager.UIManagerInstance.SetNavigationUIListeners(this);
        
        SetUI();

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {

    }

    public override void EndBehaviour()
    {
        Debug.Log("SeekBehaviour End called");
        base.EndBehaviour();
    }

    public override void SeekKitchen()
    {
        if (PlayerController.IsPetAbleToSeek())
            PlayerController.SetPlayerDestination(FindWaypointHelper("kitchen"));
    }

    public override void SeekGym()
    {
        if (PlayerController.IsPetAbleToSeek())
            PlayerController.SetPlayerDestination(FindWaypointHelper("gym"));
    }

    public override void SeekBathroom()
    {
        if (PlayerController.IsPetAbleToSeek())
            PlayerController.SetPlayerDestination(FindWaypointHelper("bathroom"));
    }
    public override void SeekBedroom()
    {
        if (PlayerController.IsPetAbleToSeek())
            PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));
    }

    public override void SeekTrophyCabinet()
    {
        if (PlayerController.IsPetAbleToSeek()) 
            PlayerController.SetPlayerDestination(FindWaypointHelper("trophycabinet"));
    }

    public override void SeekLivingRoom()
    {
        if (PlayerController.IsPetAbleToSeek())
            PlayerController.SetPlayerDestination(FindWaypointHelper("livingroom"));
    }
}

