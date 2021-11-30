using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMorningReport : MonoBehaviour
{
    [Header("Time Entry Objects")]
    [SerializeField] private GameObject timeEntryPanelObj;
    [HideInInspector] private int bedTime = -1;
    [HideInInspector] private int wakeUpTime = -1;
    [SerializeField] private GameObject submitTimesBtnObj;
    [HideInInspector] public bool hasTimesBeenSubmitted;

    [Header("Morning Report Panel")]
    [SerializeField] private GameObject reportPanelObj;
    [SerializeField] private TextMeshProUGUI goalBedTimeTxt;
    [SerializeField] private TextMeshProUGUI goalWakeUpTimeTxt;
    [SerializeField] private TextMeshProUGUI hrsSleptNightOneText;
    [SerializeField] private TextMeshProUGUI hrsSleptNightTwoText;
    [SerializeField] private GameObject achievementUnlockedBtnObj;

    [Header("View Goals Panel")]
    [SerializeField] private GameObject viewSleepGoalsPanelObj;
    [SerializeField] private TextMeshProUGUI[] goalsText;
    [SerializeField] private GameObject toggleBetweenGoalsAndSleepTimes;

    [Header("Trophy Unlocked Panel")]
    [SerializeField] private GameObject trophyUnlockedPanel;
    [SerializeField] private TextMeshProUGUI achievementTxt;
    [SerializeField] private TextMeshProUGUI trophyExplainerTxt;
    private bool isTrophyUnlockedPanelActive = false;

    [Header("Close Report")]
    [SerializeField] private GameObject closeReportBtnObj;
    private bool isTrophyUnlocked = false;
    [HideInInspector] public bool IsMorningReportClosed = false;

    // Start is called before the first frame update
    void Start()
    {
        hrsSleptNightOneText = hrsSleptNightOneText.GetComponent<TextMeshProUGUI>();
        hrsSleptNightTwoText = hrsSleptNightTwoText.GetComponent<TextMeshProUGUI>();
    }

    //bedtime input field
    public void BedtimeInputField(string bedTimeInput)
    {
        bool isNumeric = int.TryParse(bedTimeInput, out _);
        if (isNumeric) bedTime = Int16.Parse(bedTimeInput);
        else Debug.Log("Only ints can be passed in");
    }

    //wake up time input field
    public void WakeUpTimeInputField(string wakeUpInput)
    {
        bool isNumeric = int.TryParse(wakeUpInput, out _);
        if (isNumeric) wakeUpTime = Int16.Parse(wakeUpInput);
        else Debug.Log("Only ints can be passed in");
    }

    public void ButtonSubmitTimes()
    {
        Debug.Log("submit times button clicked");
        if (wakeUpTime > 0 && bedTime > 0) hasTimesBeenSubmitted = true;
        else Debug.Log("invalid wakeup time and bed times submitted");        
    }

    //called by status report behaviour
    public void DeliverMorningReport(Vector2Int hrsSlept, Vector2Int sleepTimeGoals)
    {
        submitTimesBtnObj.SetActive(false);
        timeEntryPanelObj.SetActive(false);
        reportPanelObj.SetActive(true);
        toggleBetweenGoalsAndSleepTimes.SetActive(true);

        SetSleepTimeGoals(sleepTimeGoals);
        SetHoursSleptTextNightOneTwo(hrsSlept);

        if (isTrophyUnlocked)
        {
            achievementUnlockedBtnObj.SetActive(true);
        }
        else
        {
            closeReportBtnObj.SetActive(true);
        }

    }

    //called by status report behaviour if unachievement is unlocked
    public void IsAchieveUnlocked(bool value, string[] trophyTitles)
    {
        isTrophyUnlocked = value;
        achievementTxt.text = trophyTitles[0];
        trophyExplainerTxt.text = trophyTitles[1];
    }

    public void ButtonUnlockAchievement()
    {
        isTrophyUnlockedPanelActive = !isTrophyUnlockedPanelActive;
        trophyUnlockedPanel.SetActive(true);
        closeReportBtnObj.SetActive(true);
    }

    public void ButtonCloseReport()
    {
        hasTimesBeenSubmitted = false;


        ToggleViewSleepTimesGoals(false);
        toggleBetweenGoalsAndSleepTimes.SetActive(false);
        reportPanelObj.SetActive(false);
        viewSleepGoalsPanelObj.SetActive(false);
        trophyUnlockedPanel.SetActive(false);
        closeReportBtnObj.SetActive(false);

        submitTimesBtnObj.SetActive(true);
        timeEntryPanelObj.SetActive(true);

        if (isTrophyUnlocked)
        {
            achievementUnlockedBtnObj.SetActive(false);
            isTrophyUnlocked = false;
        }

        IsMorningReportClosed = true;
    }

    public void ToggleViewSleepTimesGoals(bool value)
    {
        reportPanelObj.SetActive(!value);
        viewSleepGoalsPanelObj.SetActive(value);
    }

    public void SetHoursSleptTextNightOneTwo(Vector2Int hrsSlept)
    {
        hrsSleptNightOneText.text = hrsSlept[0].ToString();
        hrsSleptNightTwoText.text = hrsSlept[1].ToString();
    }

    public void SetSleepTimeGoals(Vector2Int sleepTimeGoals)
    {
        goalBedTimeTxt.text = sleepTimeGoals[0].ToString();
        goalWakeUpTimeTxt.text = sleepTimeGoals[1].ToString();
    }

    public void SetGoalsText(string[] goals)
    {
        int goalsNum = goals.Length;

        for (int i = 0; i < goalsNum; i++)
        {
            goalsText[i].text = goals[i];
        }
    }

    public int GetBedtime()
    {
        int timeToReturn = bedTime;
        bedTime = -1;
        return timeToReturn;
    }

    public int GetWakeUpTime()
    {
        int timeToReturn = wakeUpTime;
        wakeUpTime = -1;
        return timeToReturn;
    }


}
