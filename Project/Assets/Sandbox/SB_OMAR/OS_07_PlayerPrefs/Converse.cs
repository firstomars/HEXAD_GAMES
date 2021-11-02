using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sandbox.Omar.FileManager
{
    public class Converse : MonoBehaviour
    {
        //player name
        [SerializeField] private GameObject nameInputObj;
        private InputField nameInputField;

        [SerializeField] private Text nameText;
        private string playerName;

        
        // Start is called before the first frame update
        void Start()
        {
            nameInputField = nameInputObj.GetComponent<InputField>();
        }

        // Update is called once per frame
        void Update()
        {
            nameText.text = playerName;
        }

        public void SetPlayerName(string nameInput)
        {
            FileManager.Instance.SavePlayerName(nameInput);
            nameInputField.text = "";
        }

        public void LoadPlayerName()
        {
            

            playerName = FileManager.Instance.LoadPlayerName();
            //Debug.Log("name loaded");
        }

    }
}