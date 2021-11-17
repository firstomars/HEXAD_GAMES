using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCheckBehaviour : Behaviour
{
    public StatusCheckBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("StatusCheckBehaviour Start called");
        PlayerController.SetPlayerDestination(FindWaypointHelper("trophycabinet"));
        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetUI("report");
            UIManager.UIManagerInstance.SetGoalsText(
                PlayerController.TrophyController.GetGoalsText());
        }
            

        if (UIManager.UIManagerInstance.bedTime != -1 && UIManager.UIManagerInstance.wakeUpTime != -1)
        {
            int bedTime = UIManager.UIManagerInstance.GetBedtime();
            int wakeUpTime = UIManager.UIManagerInstance.GetWakeUpTime();

            Vector2Int hrsSlept = PlayerController.PlayerStatistics.CalculateHoursSleptNightOneTwo(bedTime, wakeUpTime);

            //int hrsSlept = PlayerController.PlayerStatistics.CalculateHoursSlept(bedTime, wakeUpTime);

            //UIManager.UIManagerInstance.SetHoursSleptText(hrsSlept);
            UIManager.UIManagerInstance.SetHoursSleptTextNightOneTwo(hrsSlept);
            
            //REFACTOR BELOW
            PlayerController.TrophyController.TrophyConditionCheck();
            PlayerController.TrophyController.InstantiateTrophy();

            PlayerController.IsReportDelivered(true);
        }

        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("StatusCheckBehaviour End called");
        SetUI();
        base.EndBehaviour();
    }
}
