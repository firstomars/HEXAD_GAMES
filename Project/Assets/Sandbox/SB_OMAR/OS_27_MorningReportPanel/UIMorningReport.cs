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
    [SerializeField] private TextMeshProUGUI goalBedTimeTxt;
    [SerializeField] private TextMeshProUGUI goalWakeUpTimeTxt;
    [SerializeField] private TextMeshProUGUI hrsSleptNightOneText;
    [SerializeField] private TextMeshProUGUI hrsSleptNightTwoText;
    [SerializeField] private GameObject viewGoalsBtnObj;
    private Button viewGoalsBtn;
    [SerializeField] private GameObject achievementUnlockedBtnObj;
    //private Button achievementUnlockedBtn;

    [Header("View Goals Panel")]
    [SerializeField] private GameObject viewSleepGoalsPabelObj;
    [SerializeField] private GameObject viewSleepTimesBtnObj;
    private Button viewSleepTimesBtn;
    [SerializeField] private TextMeshProUGUI[] goalsText;

    //[SerializeField] private TextMeshProUGUI goalOneTxt;
    //[SerializeField] private TextMeshProUGUI goalTwoTxt;
    //[SerializeField] private TextMeshProUGUI goalThreeTxt;
    //[SerializeField] private TextMeshProUGUI goalFourTxt;
    //[SerializeField] private TextMeshProUGUI goalFiveTxt;
    //[SerializeField] private TextMeshProUGUI goalSixTxt;
    //[SerializeField] private TextMeshProUGUI goalSevenTxt;
    //[SerializeField] private TextMeshProUGUI goalEightTxt;
    //[SerializeField] private TextMeshProUGUI goalNineTxt;

    [Header("Trophy Unlocked Panel")]
    [SerializeField] private GameObject trophyUnlockedPanel;
    [SerializeField] private TextMeshProUGUI achievementTxt;
    [SerializeField] private TextMeshProUGUI trophyExplainerTxt;
    private bool isTrophyUnlockedPanelActive = false;

    [Header("Close Report")]
    [SerializeField] private GameObject closeReportBtnObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    public void ButtonUnlockAchievement()
    {
        isTrophyUnlockedPanelActive = !isTrophyUnlockedPanelActive;
        trophyUnlockedPanel.SetActive(isTrophyUnlockedPanelActive);
    }

    //called by status report behaviour if unachievement is unlocked
    public void ActivateUnlockAchievementButton(bool value, string[] trophyTitles)
    {
        achievementUnlockedBtnObj.SetActive(value);
        achievementTxt.text = trophyTitles[0];
        trophyExplainerTxt.text = trophyTitles[1];
    }

    //called by status report behaviour
    public void SetHoursSleptTextNightOneTwo(Vector2Int hrsSlept)
    {
        hrsSleptNightOneText.text = hrsSlept[0].ToString();
        hrsSleptNightTwoText.text = hrsSlept[1].ToString();
    }

    public void SetGoalsText(string[] goals)
    {
        int goalsNum = goalsText.Length;

        for (int i = 0; i < goalsNum; i++)
        {
            goalsText[i].text = goals[i];
        }
    }


}
