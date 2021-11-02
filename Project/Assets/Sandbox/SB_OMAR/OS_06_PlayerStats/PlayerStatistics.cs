using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sandbox.Omar.PlayerStats
{
    public class PlayerStatistics : MonoBehaviour
    {
        //connect with timecontroller
        private int hrsSleptNightOne;
        private int hrsSleptNightTwo;

        //set by intro scene
        private int wakeUpTime;
        private int bedTime;

        [SerializeField] private float energyLevel;
        [SerializeField] private float spiritLevel;

        [SerializeField] private Text energyLevelText;
        [SerializeField] private Text spiritLevelText;


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private int CalculateFulfilmentMeter()
        {
            return 0;
        }

        private void UpdateSleepHours()
        {
        
        }
    }
}

