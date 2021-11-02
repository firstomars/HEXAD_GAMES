using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sandbox.Omar.PlayerStats
{
    public class PlayerStatistics : MonoBehaviour
    {
        [Header("Debug Input Variables")]
        [SerializeField] private int DebugSleepHoursNightOne;
        [SerializeField] private int DebugSleepHoursNightTwo;
        [SerializeField] private GameObject inputField;
        private InputField hrsSleptInputField;

        [Header("STAT: Spirit")]
        [SerializeField] private float minSpiritLevel;
        [SerializeField] private float maxSpiritLevel;
        [SerializeField] private float spiritLevel;

        [Header("STAT: Energy")]
        [SerializeField] private float energyLevel; //reduces slowly overtime
        [SerializeField] private float maxEnergyLevel;
        [SerializeField] private float minEnergyLevel;
        [SerializeField] private float energyReductionAmount;
        [SerializeField] private float secsToWaitBeforeEnergyReduction;

        [Header("STAT: Fulfilment")]
        [SerializeField] private float fulfilLevel; //reduces slowly overtime
        [SerializeField] private float maxfulfilLevel;
        [SerializeField] private float minfulfilLevel;
        [SerializeField] private int fulfilGainedPerGame;
        [SerializeField] private int numGamesPlayed;

        [Header("GameObjects")]
        [SerializeField] private Text energyLevelText;
        [SerializeField] private Text fulfilLevelText;
        [SerializeField] private Text spiritLevelText;
        [SerializeField] private Text hrsSleptNightOneText;
        [SerializeField] private Text hrsSleptNightTwoText;

        //connect with timecontroller
        private int hrsSleptNightOne;
        private int hrsSleptNightTwo;

        //set by intro scene
        private int wakeUpTime;
        private int bedTime;


        // Start is called before the first frame update
        void Start()
        {
            hrsSleptInputField = inputField.GetComponent<InputField>();

            StartCoroutine(ReduceEnergyOverTime());
        }

        // Update is called once per frame
        void Update()
        {
            hrsSleptNightOneText.text = hrsSleptNightOne.ToString();
            hrsSleptNightTwoText.text = hrsSleptNightTwo.ToString();

            energyLevelText.text = energyLevel.ToString();
            fulfilLevelText.text = fulfilLevel.ToString();
            spiritLevelText.text = CalculateSpiritLevel();
        }

        IEnumerator ReduceEnergyOverTime()
        {
            while (energyLevel > minEnergyLevel)
            {
                energyLevel -= energyReductionAmount;
                Debug.Log("energy is now: " + energyLevel);
                yield return new WaitForSeconds(secsToWaitBeforeEnergyReduction);
            }
        }

        //sleep button
        public void RefreshEnergyLevels()
        {
            energyLevel = maxEnergyLevel;

            ResetFulfilment();
        }

        //how many games played?
        public void CalculateFulfilmentMeter() 
        {
            if (fulfilLevel + fulfilGainedPerGame < maxfulfilLevel)
            {
                numGamesPlayed++;
                fulfilLevel = numGamesPlayed * fulfilGainedPerGame;
            }
            else Debug.Log("Unable to play any more games");
        }

        //hrs slept input field
        public void SetHoursSlept(string hrsSlept)
        {
            hrsSleptNightTwo = hrsSleptNightOne;

            bool isNumeric = int.TryParse(hrsSlept, out _);
            if (isNumeric) hrsSleptNightOne = Int16.Parse(hrsSlept);
            else Debug.Log("Only ints can be passed in");

            hrsSleptInputField.text = "";
            RefreshEnergyLevels();
        }

        private void ResetFulfilment()
        {
            numGamesPlayed = 0;
            fulfilLevel = 0;
        }

        private void UpdateSleepHours() // takes int parameter - called from Converse class
        {
            hrsSleptNightTwo = DebugSleepHoursNightTwo;
            hrsSleptNightOne = DebugSleepHoursNightOne;
        }

        private string CalculateSpiritLevel()
        {
            //Calculated in real time based on the values of:
            //Energy level
            //Hours slept previous night
            //Pet fullilment(has he played?)

            spiritLevel = (energyLevel + fulfilLevel + hrsSleptNightOne) / 3;
            
            return spiritLevel.ToString();
        }
    }
}

