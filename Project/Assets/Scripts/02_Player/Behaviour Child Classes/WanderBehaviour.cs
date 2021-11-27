using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : Behaviour
{
    private bool isWanderDestinationSet = true;
    private bool isCoroutineCalled = false;

    public WanderBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("WanderBehaviour Started");
        PlayerController.WanderWaypoints.SetWanderWaypoints();
        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        if(PlayerController.HasPlayerReachedDestination() && !isCoroutineCalled)
        {
            isCoroutineCalled = true;
            PlayerController.StartCoroutine(WaitBeforeWander());
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
        PlayerController.WanderWaypoints.SetWanderWaypoints();
        base.EndBehaviour();
    }

    private void SetWanderDestination()
    {
        int min = 0;
        int max = PlayerController.WanderWaypoints.wanderWaypoints.Length;

        System.Random randomNum = new System.Random();

        int randomIndex = randomNum.Next(min, max);

        PlayerController.SetPlayerDestination(PlayerController.WanderWaypoints.wanderWaypoints[randomIndex].position);
        //Debug.Log("random destination set to " + PlayerController.WanderWaypoints.wanderWaypoints[randomIndex].name);
    }

    private IEnumerator WaitBeforeWander()
    {
        yield return new WaitForSeconds(7);
        isWanderDestinationSet = false;
    }
}

