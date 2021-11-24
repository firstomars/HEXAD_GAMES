using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [Header("UI Toggle")]
    [SerializeField] private GameObject timeUI;

    //skyboxes
    [SerializeField] Material[] skyboxes;

    [Header("Key Skybox Times (int)")]
    [SerializeField] private int sunriseTime;
    [SerializeField] private int dayTime;
    [SerializeField] private int sunsetTime;
    [SerializeField] private int nightTime;

    [Header("Debug Text Fields")]
    [SerializeField] private Text actualTimeText;
    [SerializeField] private Text gameTimeText;
    [SerializeField] private Text actualDateText;
    [SerializeField] private Text gameDateText;
    [SerializeField] private GameObject inputField;
    private InputField hoursInputField;

    //tracking changed variables
    private int daysToChange;
    private int hoursToChange;
    private bool hasTimeChanged = false;
    private bool hasDateChanged = false;
    private int gameTimeHours;
    private int gameDate;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.UIManagerInstance.TimeController = this;
        
        hoursInputField = inputField.GetComponent<InputField>();

        gameTimeHours = GetCurrentHours();
        gameDate = GetCurrentDay();
    }

    // Update is called once per frame
    void Update()
    {
        //set actual time / date
        actualTimeText.text = GetActualTime();
        actualDateText.text = GetActualDate();

        //debug game time
        DebugTime();

        //check time and set skybox
        CheckTimeForSkybox();
    }

    private void CheckTimeForSkybox()
    {
        if (IsTimeAfter(sunriseTime) && IsTimeBefore(dayTime)) SwitchSkybox(0); //morning
        else if (IsTimeAfter(dayTime) && IsTimeBefore(sunsetTime)) SwitchSkybox(1); //day
        else if (IsTimeAfter(sunsetTime) && IsTimeBefore(nightTime)) SwitchSkybox(2); //sunset
        else if (IsTimeAfter(nightTime) || IsTimeBefore(sunriseTime)) SwitchSkybox(3); //night
    }

    private void SwitchSkybox(int skyboxIndex)
    {
        RenderSettings.skybox = skyboxes[skyboxIndex];
    }

    private void DebugTime()
    {
        //check if game date / time has changed
        if (hasTimeChanged)     CalculateNewGameTime();
        else                    gameTimeText.text = GetActualTime();
        if (hasDateChanged)     CalculateNewGameDate();
        else                    gameDateText.text = GetActualDate();
    }

    private void CalculateNewGameDate()
    {
        //calculate new date
        int newDate = gameDate + daysToChange;

        //debug print
        gameDateText.text = newDate.ToString() + "/" + GetMonthYear();

        //reset day change counter
        daysToChange = 0;

        //store new date for game
        gameDate = newDate;
    }

    private void CalculateNewGameTime()
    {
        //calculate new time
        int hours = gameTimeHours + hoursToChange;

        //check if date changes
        if (hours > 23)
        {
            hours -= 24;
            daysToChange += 1;
            hasDateChanged = true;
        }

        if (hours < 0)
        {
            hours += 24;
            daysToChange -= 1;
            hasDateChanged = true;
        }

        //debug print
        gameTimeText.text = hours.ToString() + ":" + GetCurrentMinutes();

        //reset hours counter
        hoursToChange = 0;

        //store new game time
        gameTimeHours = hours;
    }

    //input field function
    public void GetHoursToChange(string hoursInput)
    {
        //check if input is an int
        bool isNumeric = int.TryParse(hoursInput, out _);
        if (isNumeric) hoursToChange = Int16.Parse(hoursInput);
        else hoursToChange = 0;

        //error message
        if (hoursToChange == 0 || hoursToChange > 23 || hoursToChange < -23)
        {
            Debug.Log("invalid input - must be an int, between -23 and 23, and not 0");
        }
        else
        {
            hasTimeChanged = true;
            hoursInputField.text = "";
        }
    }

    public int GetGameTime()
    {
        return gameTimeHours;
    }

    //reset button function
    public void ResetGameTime()
    {
        hasTimeChanged = false;
        hasDateChanged = false;

        gameTimeHours = GetCurrentHours();
        gameDate = GetCurrentDay();
    }

    public bool IsTimeAfter(int hour)
    {
        if (gameTimeHours >= hour) return true;

        return false;
    }

    public bool IsTimeBefore(int hour)
    {
        if (gameTimeHours < hour) return true;
        return false;
    }

    public int GetGameDate()
    {
        return gameDate;
    }

    public bool IsNextDay(int previousDay)
    {
        if (gameDate > previousDay) return true;
        else                        return false;
    }

    public void ToggleTimeUI(bool value)
    {
        timeUI.SetActive(value);
    }

    #region Helper Functions

    private string GetActualTime()
    {
        return DateTime.UtcNow.ToLocalTime().ToString("HH:mm");
    }

    private string GetActualDate()
    {
        return DateTime.UtcNow.ToString("dd/MM/yyyy");
    }

    private int GetCurrentHours()
    {
        string currentHours = DateTime.UtcNow.ToLocalTime().ToString("HH");
        return Int16.Parse(currentHours);
    }

    private string GetCurrentMinutes()
    {
        return DateTime.UtcNow.ToLocalTime().ToString("mm");
    }

    private int GetCurrentDay()
    {
        string currentDay = DateTime.UtcNow.ToString("dd");
        return Int16.Parse(currentDay);
    }

    private string GetMonthYear()
    {
        return DateTime.UtcNow.ToString("MM/yyyy");
    }

    #endregion

}