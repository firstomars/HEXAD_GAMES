using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour
{
    private UIManager UIManager;

    //set by intro scene
    private int wakeUpTime;
    private int bedTime;

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

    [Header("STAT: Fulfilment")]
    [SerializeField] private float fulfilLevel;
    [SerializeField] private float maxfulfilLevel;
    [SerializeField] private float minfulfilLevel;
    [SerializeField] private int fulfilGainedPerGame;
    [SerializeField] private int fulfilGainedFromFood = 5;
    [SerializeField] private int fulfilGainedFromJunkFood = 10;
    [SerializeField] private int fulfilGainedFromBenchPress = 10;

    [Header("SLEEP DOLLARS")]
    [SerializeField] private int sleepDollarsLevel = 0;
    [SerializeField] private int sleepDollarsIncrementPerGame = 10;
    private int minigamesPlayed = 0;
    private int mealsEatenToday = 0;

    [Header("SLEEP TIMES")]
    [SerializeField] private int bedTimeGoal;
    [SerializeField] private int wakeUpTimeGoal;
    [SerializeField] private int hrsSleptNightOne;
    [SerializeField] private int hrsSleptNightTwo;
    [SerializeField] private int hrsSleptNightThree;
    [SerializeField] private int hrsSleptNightFour;
    [SerializeField] private int hrsSleptNightFive;

    [HideInInspector] public int SleepTrophyGoals { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        UIManager = UIManager.UIManagerInstance;
        StartCoroutine(ReduceEnergyOverTime());

        SleepTrophyGoals = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.StatisticsUpdate(
            CalculateEnergySliderLevel(),
            CalculateFulfillmentSliderLevel(),
            CalculateSpiritSliderLevel(),
            hrsSleptNightOne.ToString(),
            minigamesPlayed.ToString(),
            mealsEatenToday.ToString(),
            SleepTrophyGoals.ToString(),
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

    public void SetNewEnergyLevels()
    {
        energyLevel = maxEnergyLevel;
        
        mealsEatenToday = 0;
        minigamesPlayed = 0;

        StartCoroutine(ReduceEnergyOverTime());

        ResetFulfilment();
    }

    public void MinigameStatsImpact()
    {
        if (fulfilLevel + fulfilGainedPerGame < maxfulfilLevel && energyLevel > 26)
        {
            minigamesPlayed++;
            fulfilLevel += fulfilGainedPerGame;
            sleepDollarsLevel += sleepDollarsIncrementPerGame;
            energyLevel -= 25;
            Debug.Log("played minigame");
        }
        else Debug.Log("Unable to play any more games");
    }

    public void FoodEatenStatsImpact()
    {
        bool mealEaten = false;

        if (energyLevel < maxEnergyLevel - 6)
        {
            energyLevel += energyGainedFromFood;
            mealEaten = true;
        }
        else energyLevel = maxEnergyLevel;

        if (fulfilLevel < maxfulfilLevel - 6)
        {
            fulfilLevel += fulfilGainedFromFood;
            mealEaten = true;
        }
        else fulfilLevel = maxfulfilLevel;

        if (mealEaten) mealsEatenToday++;
    }

    public void JunkFoodEatenStatsImpact()
    {
        bool mealEaten = false;

        if (fulfilLevel < maxfulfilLevel - 11)
        {
            fulfilLevel += fulfilGainedFromJunkFood;
            mealEaten = true;
        }
        else fulfilLevel = maxfulfilLevel;

        if (mealEaten) mealsEatenToday++;
    }

    public void BenchPressStatsImpact()
    {
        if (energyLevel > minEnergyLevel + energyReducedFromBenchPress)
        {
            energyLevel -= energyReducedFromBenchPress;
        }
        else Debug.Log("energy level at max");

        if (fulfilLevel < maxfulfilLevel - fulfilGainedFromBenchPress)
        {
            fulfilLevel += fulfilGainedFromBenchPress;
        }
        else Debug.Log("fulfil level at max");
    }

    private void ResetFulfilment()
    {
        fulfilLevel = 0;
    }

    private float CalculateEnergySliderLevel()
    {
        return (energyLevel / maxEnergyLevel);
    }

    private float CalculateFulfillmentSliderLevel()
    {
        return (fulfilLevel / maxfulfilLevel);
    }


    private float CalculateSpiritSliderLevel()
    {
        spiritLevel = (energyLevel - 10) + fulfilLevel;
        if (spiritLevel > 100) spiritLevel = 100;

        return spiritLevel / maxSpiritLevel;
    }


    private string CalculateSpiritLevel()
    {
        spiritLevel = (energyLevel - 10) + fulfilLevel;
        if (spiritLevel > 100) spiritLevel = 100;

        return spiritLevel.ToString();
    }

    //public int CalculateHoursSlept(int bedTime, int wakeUpTime)
    //{
    //    hrsSleptNightTwo = hrsSleptNightOne;
        
    //    hrsSleptNightOne = (24 - bedTime) + wakeUpTime; //REFACTOR
    //    energyLevel = maxEnergyLevel;

    //    Debug.Log("PlayerStats " + hrsSleptNightOne);

    //    //add if statements to see what new energy levels are
    //    SetNewEnergyLevels();

    //    return hrsSleptNightOne;
    //}

    public Vector2Int CalculateHoursSleptNightOneTwo(int bedTime, int wakeUpTime)
    {
        //REFACTOR
        hrsSleptNightFive = hrsSleptNightFour;
        hrsSleptNightFour = hrsSleptNightThree;
        hrsSleptNightThree = hrsSleptNightTwo;        
        hrsSleptNightTwo = hrsSleptNightOne;


        hrsSleptNightOne = CalculateHoursSlept(bedTime, wakeUpTime);
        //hrsSleptNightOne = (24 - bedTime) + wakeUpTime; //REFACTOR
        energyLevel = maxEnergyLevel;

        //Debug.Log("PlayerStats " + hrsSleptNightOne);

        //add if statements to see what new energy levels are
        SetNewEnergyLevels();

        Vector2Int hrsSlept = new Vector2Int(hrsSleptNightOne, hrsSleptNightTwo);

        return hrsSlept;
    }

    private int CalculateHoursSlept(int bedTime, int wakeUpTime)
    {
        return (24 - bedTime) + wakeUpTime;
    }

    public int GetHrsSleptNightOne()
    {
        return hrsSleptNightOne;
    }

    public int[] GetHoursSleptLastFiveNights()
    {
        int[] hrsSleptFiveNight = { hrsSleptNightOne, hrsSleptNightTwo, hrsSleptNightThree, hrsSleptNightFour, hrsSleptNightFive };
        return hrsSleptFiveNight;
    }

    public void SleepDollarsReward(int rewardAmt)
    {
        sleepDollarsLevel += rewardAmt;
    }

    public int GetSleepDollars()
    {
        return sleepDollarsLevel;
    }

    public void ReduceSleepDollars(int reduceAmt)
    {
        Debug.Log("Sleep dollars " + sleepDollarsLevel + " reduced by " + reduceAmt);
        sleepDollarsLevel -= reduceAmt;
        Debug.Log("New sleep dollars is " + sleepDollarsLevel);
    }

    public void SetInitialSleepTimesAndGoals(List<string> sleepTimeEntries)
    {
        bedTimeGoal = Int32.Parse(sleepTimeEntries[0].Substring(0, 2));
        wakeUpTimeGoal = Int32.Parse(sleepTimeEntries[1].Substring(0, 2));

        Debug.Log("Bed Time Goal: " + bedTimeGoal);
        Debug.Log("Wake Time Goal: " + wakeUpTimeGoal);

        int firstBedTime = Int32.Parse(sleepTimeEntries[2].Substring(0, 2));
        int firstWakeUpTime = Int32.Parse(sleepTimeEntries[3].Substring(0, 2));
        hrsSleptNightOne = CalculateHoursSlept(firstBedTime, firstWakeUpTime);

        //Debug.Log("First Bed Time: " + firstBedTime);
        //Debug.Log("First Wake Time: " + firstWakeUpTime);
        //Debug.Log("Hours slept on first night " + hrsSleptNightOne);
    }
}
