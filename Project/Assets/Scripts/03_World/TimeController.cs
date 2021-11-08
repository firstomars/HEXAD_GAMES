using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [Header("UI Toggle")]
    [SerializeField] private GameObject timeUI;

    [Header("SkyBox")]
    [SerializeField] private GameObject skyboxDay;
    [SerializeField] private GameObject skyboxNight;

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
        hoursInputField = inputField.GetComponent<InputField>();

        gameTimeHours = GetCurrentHours();
        gameDate = GetCurrentDay();

        skyboxDay.SetActive(false);
        skyboxNight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //set actual time / date
        actualTimeText.text = GetActualTime();
        actualDateText.text = GetActualDate();

        //debug game time
        DebugTime();

        if (IsTimeAfter(20) || IsTimeBefore(4)) SetNight();
        else SetDay();
    }

    private void SetNight()
    {
        skyboxNight.SetActive(true);
        skyboxDay.SetActive(false);
    }

    private void SetDay()
    {
        skyboxNight.SetActive(false);
        skyboxDay.SetActive(true);
    }

    private void DebugTime()
    {
        //check if game date / time has changed
        if (hasTimeChanged) GetNewGameTime();
        else gameTimeText.text = GetActualTime();
        if (hasDateChanged) GetNewGameDate();
        else gameDateText.text = GetActualDate();
    }

    private void GetNewGameDate()
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

    private void GetNewGameTime()
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
        if (gameTimeHours > hour) return true;

        return false;
    }

    public bool IsTimeBefore(int hour)
    {
        if (gameTimeHours < hour) return true;
        return false;
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

    private int GetCurrentDay()
    {
        string currentDay = DateTime.UtcNow.ToString("dd");
        return Int16.Parse(currentDay);
    }

    private string GetCurrentMinutes()
    {
        return DateTime.UtcNow.ToLocalTime().ToString("mm");
    }

    private string GetMonthYear()
    {
        return DateTime.UtcNow.ToString("MM/yyyy");
    }

    #endregion

}