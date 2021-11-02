using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Sandbox.Omar.FileManager
{
    public class HouseController : MonoBehaviour
    {
        [SerializeField] private GameObject bedLevelObj;
        private InputField bedLevelInputField;
        private int bedLevel;

        [SerializeField] private Text bedLevelText;

        // Start is called before the first frame update
        void Start()
        {
            bedLevelInputField = bedLevelObj.GetComponent<InputField>();
        }

        // Update is called once per frame
        void Update()
        {
            bedLevelText.text = bedLevel.ToString();
        }

        public void SetBedLevel(string level)
        {
            FileManager.Instance.SaveBedLevel(level);
            bedLevelInputField.text = "";
        }

        public void LoadBedLevel()
        {
            bedLevel = Int16.Parse(FileManager.Instance.LoadBedLevel());
        }
    }
}