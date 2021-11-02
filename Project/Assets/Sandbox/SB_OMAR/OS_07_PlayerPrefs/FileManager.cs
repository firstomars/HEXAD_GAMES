using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.FileManager
{
    public class FileManager : MonoBehaviour
    {
        #region Singelton

        private static FileManager _instance;

        public static FileManager Instance
        {
            get
            {
                //if (_instance == null)
                //{
                //    GameObject go = new GameObject("GameManager");
                //    go.AddComponent<GameManager>();
                //}
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }
        #endregion
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SavePlayerName(string playerName)
        {
            PlayerPrefs.SetString("name", playerName);
        }

        public string LoadPlayerName()
        {
            return PlayerPrefs.GetString("name");
        }

        public void SaveHrsSleptNightOne(string hrsSlept)
        {
            bool isNumeric = Int16.TryParse(hrsSlept, out _);
            if (isNumeric) PlayerPrefs.SetString("hrsSleptN1", hrsSlept);
            else Debug.Log("hrs slept for n1 was not a string");
        }


        public int LoadHrsSleptNightOne()
        {
            return Int16.Parse(PlayerPrefs.GetString("hrsSleptN1"));
        }

        public void SaveHrsSleptNightTwo(string hrsSlept)
        {
            bool isNumeric = Int16.TryParse(hrsSlept, out _);
            if (isNumeric) PlayerPrefs.SetString("hrsSleptN2", hrsSlept);
            else Debug.Log("hrs slept for n2 was not a string");
        }

        public int LoadHrsSleptNightTwo()
        {
            return Int16.Parse(PlayerPrefs.GetString("hrsSleptN2"));
        }

        public void SaveBedLevel(string level)
        {
            bool isNumeric = Int16.TryParse(level, out _);
            if (isNumeric) PlayerPrefs.SetString("bedLevel", level);
            else Debug.Log("Error - bed level is not an int");
        }

        public string LoadBedLevel()
        {
            return PlayerPrefs.GetString("bedLevel");
        }
    }
}

