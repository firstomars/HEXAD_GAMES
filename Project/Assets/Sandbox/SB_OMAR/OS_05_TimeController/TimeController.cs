using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private Text actualTimeText;
    [SerializeField] private Text gameTimeText;

    private string gameTime;

    private int hoursToChange;
    //private int hoursToMinus;

    private bool hasTimeChanged = true;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = GetActualTime();
    }

    // Update is called once per frame
    void Update()
    {
        actualTimeText.text = GetActualTime();
        
        if (hasTimeChanged)
        {
            int hours = GetCurrentHours() + hoursToChange;

        }
        else 
        { 
            gameTimeText.text = GetActualTime(); 
        }
        
    }

    public string GetActualTime()
    {
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm");
        return time;
    }

    public int GetCurrentHours()
    {
        string currentHours = DateTime.UtcNow.ToLocalTime().ToString("HH");

        return Int16.Parse(currentHours);
    }

    public void GetHoursToChange(string hoursInput)
    {
        hoursToChange += Int16.Parse(hoursInput);
    }

    public string GetMinutes()
    {
        return DateTime.UtcNow.ToLocalTime().ToString("mm");
    }

    public void ResetGameTime()
    {
        hasTimeChanged = false;
    }



    //debug rewind time

    //debug fast fwd time


}
