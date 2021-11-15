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

        UIManager.UIManagerInstance.CurrentBehaviour = this;
        SetUI();

        //SetTargetPos(); //DELETE

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //set seek destination
        //if (targetPosition != PlayerController.targetPosition) SetPlayerDestination(targetPosition);
        //base.RunBehaviour();

        //if (Vector3.Distance(PlayerController.gameObject.transform.position, targetPosition) > 2.0f)
        //    MovePet();
    }

    public override void EndBehaviour()
    {
        Debug.Log("SeekBehaviour End called");
        base.EndBehaviour();
    }

    //private void SetTargetPos()
    //{
    //    targetPos = "kitchen";
    //}

    //private void SetPlayerDestination(Vector3 targetPos)
    //{
    //    targetPosition = PlayerController.targetPosition;
    //    PlayerController.SetPlayerDestination(targetPosition);
    //}

    //private void MovePet()
    //{
    //    foreach(var waypoint in PlayerController.HouseWaypoints.waypoints)
    //    {
    //        if (waypoint.name == targetPos)
    //        {
    //            PlayerController.SetPlayerDestination(waypoint.position);
    //        }
    //    }
    //}

    public override void SeekKitchen()
    {
        Debug.Log("button pressed");

        PlayerController.SetPlayerDestination(FindWaypointHelper("kitchen"));

        //foreach (var waypoint in PlayerController.HouseWaypoints.waypoints)
        //{
        //    if (waypoint.name == "kitchen")
        //    {
        //        PlayerController.SetPlayerDestination(waypoint.position);
        //    }
        //}
    }

    public override void SeekGym()
    {
        PlayerController.SetPlayerDestination(FindWaypointHelper("gym"));
    }

    public override void SeekBathroom()
    {
        PlayerController.SetPlayerDestination(FindWaypointHelper("bathroom"));
    }
    public override void SeekBedroom()
    {
        PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));
    }

    public override void SeekTrophyCabinet()
    {
        PlayerController.SetPlayerDestination(FindWaypointHelper("trophycabinet"));
    }

    public override void SeekLivingRoom()
    {
        PlayerController.SetPlayerDestination(FindWaypointHelper("livingroom"));
    }

    private Vector3 FindWaypointHelper(string room)
    {
        foreach (var waypoint in PlayerController.HouseWaypoints.waypoints)
        {
            if (waypoint.name == room)
            {
                return waypoint.position;
            }
        }

        return Vector3.zero;
    }

}

