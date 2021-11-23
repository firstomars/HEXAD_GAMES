using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : Behaviour
{
    private bool isWanderDestinationSet = false;
    private bool isCoroutineCalled = false;

    public WanderBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        //Debug.Log("WanderBehaviour Start called - press W to test update");
        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        if(PlayerController.HasPlayerReachedDestination() && !isCoroutineCalled)
        {
            isCoroutineCalled = true;
            PlayerController.StartCoroutine(WaitBeforeWander());
            Debug.Log("Coroutine called");
        }

        if (!isWanderDestinationSet)
        {
            SetWanderDestination();
            isWanderDestinationSet = true;
            isCoroutineCalled = false;
        }

        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("WanderBehaviour End called");
        base.EndBehaviour();
    }

    private void SetWanderDestination()
    {
        int min = 0;
        //int max = houseWaypoints.Length;
        int max = PlayerController.HouseWaypoints.waypoints.Length;

        System.Random randomNum = new System.Random();

        int randomIndex = randomNum.Next(min, max);

        PlayerController.SetPlayerDestination(PlayerController.HouseWaypoints.waypoints[randomIndex].position);
        Debug.Log("random destination set to " + PlayerController.HouseWaypoints.waypoints[randomIndex].name);
    }

    private IEnumerator WaitBeforeWander()
    {
        yield return new WaitForSeconds(7);
        isWanderDestinationSet = false;
    }
}

