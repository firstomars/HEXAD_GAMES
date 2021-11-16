using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour
{
    private UIManager UIManager;
    
    [Header("UI Toggle")]
    //[SerializeField] private GameObject playerStatsUI;

    [Header("Debug Input Variables")]
    //[SerializeField] private int DebugSleepHoursNightOne;
    //[SerializeField] private int DebugSleepHoursNightTwo;
    //[SerializeField] private GameObject inputField;
    //private InputField hrsSleptInputField;

    private int NEWhrsSleptNightOne;

    [Header("STAT: Spirit")]
    [SerializeField] private float minSpiritLevel;
    [SerializeField] private float maxSpiritLevel;
    [SerializeField] private float spiritLevel;

    [Header("STAT: Energy")]
    [SerializeField] public float energyLevel; //reduces slowly overtime
    [SerializeField] private float maxEnergyLevel;
    [SerializeField] private float minEnergyLevel;
    [SerializeField] private float energyReductionAmount;
    [SerializeField] private float secsToWaitBeforeEnergyReduction;
    [SerializeField] private int energyGainedFromFood = 5;
    [SerializeField] private int energyReducedFromBenchPress = 10;
    //[SerializeField] private int energyGainedFromJunkFood = 0;

    [Header("STAT: Fulfilment")]
    [SerializeField] private float fulfilLevel; //reduces slowly overtime
    [SerializeField] private float maxfulfilLevel;
    [SerializeField] private float minfulfilLevel;
    [SerializeField] private int fulfilGainedPerGame;
    [SerializeField] private int numGamesPlayed;
    [SerializeField] private int fulfilGainedFromFood = 5;
    [SerializeField] private int fulfilGainedFromJunkFood = 10;
    [SerializeField] private int fulfilGainedFromBenchPress = 10;

    [Header("SLEEP DOLLARS")]
    [SerializeField] private int sleepDollarsLevel = 0;
    [SerializeField] private int sleepDollarsIncrementPerGame = 10;


    //[Header("GameObjects")]
    //[SerializeField] private Text energyLevelText;
    //[SerializeField] private Text fulfilLevelText;
    //[SerializeField] private Text spiritLevelText;
    //[SerializeField] private Text hrsSleptNightOneText;
    //[SerializeField] private Text hrsSleptNightTwoText;

    //connect with timecontroller
    private int hrsSleptNightOne;
    private int hrsSleptNightTwo;

    //set by intro scene
    private int wakeUpTime;
    private int bedTime;


    // Start is called before the first frame update
    void Start()
    {
        //hrsSleptInputField = inputField.GetComponent<InputField>();

        UIManager = UIManager.UIManagerInstance;

        StartCoroutine(ReduceEnergyOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        //hrsSleptNightOneText.text = hrsSleptNightOne.ToString();
        //hrsSleptNightTwoText.text = hrsSleptNightTwo.ToString();

        UIManager.StatsUpdate(
            energyLevel.ToString(), 
            fulfilLevel.ToString(), 
            CalculateSpiritLevel().ToString(), 
            sleepDollarsLevel.ToString());
    }

    IEnumerator ReduceEnergyOverTime()
    {
        while (energyLevel > minEnergyLevel)
        {
            energyLevel -= energyReductionAmount;
            //Debug.Log("energy is now: " + energyLevel);
            yield return new WaitForSeconds(secsToWaitBeforeEnergyReduction);
        }
    }

    //sleep button
    //public void RefreshEnergyLevels()
    //{
    //    energyLevel = maxEnergyLevel;

    //    ResetFulfilment();
    //}

    //NEW
    public void SetNewEnergyLevels()
    {
        energyLevel = maxEnergyLevel;

        ResetFulfilment();
    }

    //how many games played?
    public void MinigameStatsImpact()
    {
        if (fulfilLevel + fulfilGainedPerGame < maxfulfilLevel && energyLevel > 26)
        {
            numGamesPlayed++;
            fulfilLevel = numGamesPlayed * fulfilGainedPerGame;
            sleepDollarsLevel += sleepDollarsIncrementPerGame;
            energyLevel -= 25;
        }
        else Debug.Log("Unable to play any more games");
    }

    public void FoodEatenStatsImpact()
    {
        if (energyLevel < maxEnergyLevel - 6) energyLevel += energyGainedFromFood;
        else energyLevel = maxEnergyLevel;

        if (fulfilLevel < maxfulfilLevel - 6) fulfilLevel += fulfilGainedFromFood;
        else fulfilLevel = maxfulfilLevel;
    }

    public void JunkFoodEatenStatsImpact()
    {
        //Debug.Log(fulfilGainedFromJunkFood);
        
        if (fulfilLevel < maxfulfilLevel - 11) 
            fulfilLevel += fulfilGainedFromJunkFood;
        else fulfilLevel = maxfulfilLevel;
    }

    public void BenchPressStatsImpact()
    {
        if (energyLevel > minEnergyLevel + energyReducedFromBenchPress)
        {
            energyLevel -= energyReducedFromBenchPress;
        }

        if (fulfilLevel < maxfulfilLevel - fulfilGainedFromBenchPress)
        {
            fulfilLevel += fulfilGainedFromBenchPress;
        }
    }

    //hrs slept input field
    //public void SetHoursSlept(string hrsSlept)
    //{
    //    hrsSleptNightTwo = hrsSleptNightOne;

    //    bool isNumeric = int.TryParse(hrsSlept, out _);
    //    if (isNumeric) hrsSleptNightOne = Int16.Parse(hrsSlept);
    //    else Debug.Log("Only ints can be passed in");

    //    //hrsSleptInputField.text = "";
    //    //RefreshEnergyLevels();
    //}

    private void ResetFulfilment()
    {
        numGamesPlayed = 0;
        fulfilLevel = 0;
    }

    //private void UpdateSleepHours() // takes int parameter - called from Converse class
    //{
    //    hrsSleptNightTwo = DebugSleepHoursNightTwo;
    //    hrsSleptNightOne = DebugSleepHoursNightOne;
    //}

    private string CalculateSpiritLevel()
    {
        //Calculated in real time based on the values of:
        //Energy level
        //Hours slept previous night
        //Pet fullilment(has he played?)

        spiritLevel = (energyLevel - 10) + fulfilLevel;

        if (spiritLevel > 100) spiritLevel = 100;

        //spiritLevel = (energyLevel + fulfilLevel + hrsSleptNightOne) / 3;

        return spiritLevel.ToString();
    }

    public int CalculateHoursSlept(int bedTime, int wakeUpTime)
    {
        NEWhrsSleptNightOne = (24 - bedTime) + wakeUpTime;
        energyLevel = maxEnergyLevel;

        Debug.Log("PlayerStats " + NEWhrsSleptNightOne);

        //add if statements to see what new energy levels are
        SetNewEnergyLevels();

        //int debugTimeSlept = (24 - bedTime) + wakeUpTime;

        //hrsSleptNightOneText.text = debugTimeSlept.ToString();

        return NEWhrsSleptNightOne;
    }

    public int GetHrsSleptNightOne()
    {
        return NEWhrsSleptNightOne;
    }
}
