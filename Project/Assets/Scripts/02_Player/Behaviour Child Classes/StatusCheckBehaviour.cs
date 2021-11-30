using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCheckBehaviour : Behaviour
{
    private bool isReportUIActivated = false;
    private bool isMorningReportDelivered = false;

    private UIMorningReport UIMorningReport;

    public StatusCheckBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("StatusCheckBehaviour Start called");
        PlayerController.SetPlayerDestination(FindWaypointHelper("trophycabinet"));

        UIMorningReport = UIManager.UIManagerInstance.UIMorningReportObj.GetComponent<UIMorningReport>();

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        if (!isReportUIActivated && Vector2.Distance(PlayerController.GetAgentPosition(), FindWaypointHelper("trophycabinet")) < 2.0f)
        {
            SetUI("report");
            isReportUIActivated = true;
        }

        if (UIMorningReport.hasTimesBeenSubmitted && !isMorningReportDelivered) //&& hasAlreadyBeenCalled ?
        {
            //get and set hours slept
            int bedTime = UIMorningReport.GetBedtime();
            int wakeUpTime = UIMorningReport.GetWakeUpTime();

            //Debug.Log("Bedtime " + bedTime + "WakeUpTime " + wakeUpTime);

            Vector2Int hrsSlept = PlayerController.PlayerStatistics.CalculateHoursSleptNightOneTwo(bedTime, wakeUpTime);
            Vector2Int sleepTimeGoals = PlayerController.PlayerStatistics.GetSleepTimeGoals();

            //if achievement unlcoked set trophy titles
            string[] trophyReceivedTitles = PlayerController.TrophyController.NEWTrophyConditionCheck();

            if (trophyReceivedTitles[0] != "null")
                UIMorningReport.IsAchieveUnlocked(true, trophyReceivedTitles);

            UIMorningReport.DeliverMorningReport(hrsSlept, sleepTimeGoals);

            //set goals text
            UIMorningReport.SetGoalsText(PlayerController.TrophyController.GetGoalsText());

            isMorningReportDelivered = true;
        }

        if(UIMorningReport.IsMorningReportClosed)
        {
            PlayerController.TrophyController.InstantiateTrophy();

            isReportUIActivated = false;
            isMorningReportDelivered = false;

            UIMorningReport.IsMorningReportClosed = false;

            if (DeliverTipType() != "null")
            {
                DialogueManager.DialogueManagerInstance.DisplayTip(DeliverTipType());
                PlayerController.IsReportDelivered(true); //leads to end behaviour
            }
            else
            {
                PlayerController.IsReportDelivered(true); //leads to end behaviour
            }
                
        }

        #region OLD REPORT UI

        //if (PlayerController.HasPlayerReachedDestination())
        //{
        //    SetUI("report");
        //    UIManager.UIManagerInstance.SetGoalsText(
        //        PlayerController.TrophyController.GetGoalsText());
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SetUI("report");
        //    UIManager.UIManagerInstance.SetGoalsText(
        //        PlayerController.TrophyController.GetGoalsText());
        //}

        //if (UIManager.UIManagerInstance.bedTime != -1 && UIManager.UIManagerInstance.wakeUpTime != -1)
        //{
        //    int bedTime = UIManager.UIManagerInstance.GetBedtime();
        //    int wakeUpTime = UIManager.UIManagerInstance.GetWakeUpTime();

        //    Vector2Int hrsSlept = PlayerController.PlayerStatistics.CalculateHoursSleptNightOneTwo(bedTime, wakeUpTime);

        //    //int hrsSlept = PlayerController.PlayerStatistics.CalculateHoursSlept(bedTime, wakeUpTime);

        //    //UIManager.UIManagerInstance.SetHoursSleptText(hrsSlept);
        //    UIManager.UIManagerInstance.SetHoursSleptTextNightOneTwo(hrsSlept);

        //    //REFACTOR BELOW
        //    PlayerController.TrophyController.TrophyConditionCheck();
        //    PlayerController.TrophyController.InstantiateTrophy();

        //    PlayerController.IsReportDelivered(true);
        //}

        #endregion

        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("StatusCheckBehaviour End called");
        SetUI();
        base.EndBehaviour();
    }

    private string DeliverTipType()
    {
        int[] hrsSleptLastFiveNights = PlayerController.PlayerStatistics.GetHoursSleptLastFiveNights();

        if (hrsSleptLastFiveNights[0] < 5 && hrsSleptLastFiveNights[1] < 5) return "2NightBadSleep";
        else if (hrsSleptLastFiveNights[0] < 5) return "1NightBadSleep";
        else return "null";
    }
}
