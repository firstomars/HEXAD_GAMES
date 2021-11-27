using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTrophyBehaviour : Behaviour
{
    public ViewTrophyBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("ViewTrophyBehaviour Start called");

        SetUI();

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("ViewTrophyBehaviour End called");
        base.EndBehaviour();
    }
}
