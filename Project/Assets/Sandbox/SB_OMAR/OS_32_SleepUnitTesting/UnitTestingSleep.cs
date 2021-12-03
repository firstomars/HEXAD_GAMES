using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sandbox.Omar.UnitTestingSleep
{
    public static class Extension
    {
        public static void clear(this InputField inputfield)
        {
            inputfield.Select();
            inputfield.text = "";
        }
    }

    public class UnitTestingSleep : MonoBehaviour
    {
        private int bedTime = -1;
        private int wakeUpTime = -1;


        // Start is called before the first frame update
        void Start()
        {

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) Debug.Log(bedTime);
            if (Input.GetKeyDown(KeyCode.W)) Debug.Log(wakeUpTime);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int hrsSlept = CalculateHoursSlept();
                if (hrsSlept == -1) Debug.Log("Bed time or wake-up time not correctly set");
                else Debug.Log("Hours slept is: " + hrsSlept);
            }
        }

        public void BedTimeInputField(string bedTimeInput)
        {
            bool isNumeric = int.TryParse(bedTimeInput, out _);
            if (isNumeric)
            {
                bedTime = Int16.Parse(bedTimeInput);
                //Debug.Log("Bedtime input = " + bedTime);
            }
            else Debug.Log("Only ints can be passed in");
        }

        public void WakeUpTimeInputField(string wakeUpTimeInput)
        {
            bool isNumeric = int.TryParse(wakeUpTimeInput, out _);
            if (isNumeric)
            {
                wakeUpTime = Int16.Parse(wakeUpTimeInput);
                //Debug.Log("Bedtime input = " + bedTime);
            }
            else Debug.Log("Only ints can be passed in");
        }

        private int CalculateHoursSlept()
        {
            int hrsSlept = -1;

            if (bedTime < 0 || wakeUpTime < 0) return -1;

            if (wakeUpTime > bedTime) hrsSlept = wakeUpTime - bedTime;
            else hrsSlept = (24 - bedTime) + wakeUpTime;

            bedTime = -1;
            wakeUpTime = -1;

            return hrsSlept;
        }

    }

}
