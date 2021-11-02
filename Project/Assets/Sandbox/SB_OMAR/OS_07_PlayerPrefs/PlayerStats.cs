using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sandbox.Omar.FileManager
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private GameObject hrsSleptN1InputObj;
        private InputField hrsSleptN1InputField;
        private string hrsSleptN1;

        [SerializeField] private GameObject hrsSleptN2InputObj;
        private InputField hrsSleptN2InputField;
        private string hrsSleptN2;

        [SerializeField] private Text hrsSleptN1Text;
        [SerializeField] private Text hrsSleptN2Text;

        // Start is called before the first frame update
        void Start()
        {
            hrsSleptN1InputField = hrsSleptN1InputObj.GetComponent<InputField>();
            hrsSleptN2InputField = hrsSleptN2InputObj.GetComponent<InputField>();
        }

        // Update is called once per frame
        void Update()
        {
            hrsSleptN1Text.text = hrsSleptN1;
            hrsSleptN2Text.text = hrsSleptN2;
        }

        public void SetHrsSleptN1(string hours)
        {
            FileManager.Instance.SaveHrsSleptNightOne(hours);
            hrsSleptN1InputField.text = "";
        }

        public void LoadHrsSleptN1()
        {
            hrsSleptN1 = FileManager.Instance.LoadHrsSleptNightOne().ToString();
        }        
        
        public void SetHrsSleptN2(string hours)
        {
            FileManager.Instance.SaveHrsSleptNightTwo(hours);
            hrsSleptN2InputField.text = "";
        }

        public void LoadHrsSleptN2()
        {
            hrsSleptN2 = FileManager.Instance.LoadHrsSleptNightTwo().ToString();
        }
    }
}
