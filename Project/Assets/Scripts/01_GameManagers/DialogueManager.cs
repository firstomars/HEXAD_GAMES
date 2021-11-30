using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DialogueManager : MonoBehaviour
{
    #region Singelton

    private static DialogueManager _DialogueManagerInstance;

    public static DialogueManager DialogueManagerInstance { get { return _DialogueManagerInstance; } }

    private void Start()
    {
        if (_DialogueManagerInstance != null) Destroy(gameObject);
        else
        {
            _DialogueManagerInstance = this;

            UIManager = UIManager.UIManagerInstance;
            UIManager.DialogueManager = this;
        }
    }
    #endregion

    #region Inspector Fields
    // Helper classes for dialogue database
    [System.Serializable]
    public class PetTips
    {
        public string tipType;
        public string[] tipList;
    }

    [System.Serializable]
    public class Conversation
    {
        public string dialogueText;
        public string[] playerResponses;
    }

    [System.Serializable]
    public class ConversationLibrary
    {
        public string conversationRoom;
        public Conversation[] conversationChain;
    }

    private UIManager UIManager;
    [Header("Dialogue Database")]
    [SerializeField] private PetTips[] petTips;
    [SerializeField] private ConversationLibrary[] petConversations;

    // Local variables
    //private bool gameIntroComplete = false;
    private bool conversationStarted = false;
    private int currentConversationID = 0;
    private int currentLineIndex = 0;
    private readonly List<int> tipsDisplayed = new List<int>();

    [HideInInspector] public bool currentConversationComplete = false;

    #endregion

    #region Display Pet Tips

    // Find the tip type in tips class depending on passed string
    private int GetTipDepth(string tipType)
    {
        for (int i = 0; i < petTips.Length; i++)
        {
            if (petTips[i].tipType == tipType)
            {
                return i;
            }
            else
            {
                return 0;
            }
        }
        Debug.LogError("Error displaying tip. Check that " + tipType + " tip type exists in the dialogue database");
        return 99;
    }

    // Display random tip from dialogue database depending on passed string
    public void DisplayTip(string tipType)
    {
        int tipDepth = GetTipDepth(tipType);
        if (tipDepth == 99) return;
        int tipListLength = petTips[tipDepth].tipList.Length;
        int tipID = Random.Range(0, tipListLength);
        // Check if the tip has been displayed recently and reroll if true
        while (tipsDisplayed.Exists(x => x == tipID))
        {
            Debug.Log("Tip ID " + tipID + " already shown in last " + (tipListLength / 2) + " tip displays.");
            tipID = Random.Range(0, tipListLength);
        }
        // Clear the already displayed tips once half the tips have been displayed
        if (tipsDisplayed.Count > (tipListLength / 2))
        {
            Debug.Log("Tips already displayed list has been cleared");
            tipsDisplayed.Clear();
            tipsDisplayed.Add(tipID);
        }
        else
        {
            tipsDisplayed.Add(tipID);
        }
        // Display the tip for 3 seconds
        UIManager.EnablePetDialogueText(petTips[tipDepth].tipList[tipID], 7);
    }

    #endregion

    #region Pet Conversations
    // Pet conversations main function
    public void PetConversation(string room = default)
    {
        if (!conversationStarted)
        {
            conversationStarted = true;
            Debug.Log("Player started " + room + " conversation");
            /* Object Key
             * 0 - Game Intro
             * 1 - Kitchen
             * 2 - Lounge
             * 3 - Bedroom
             * 4 - Gym
             * 5 - Daily Login
             * 6 - Update Settings
        */
            switch (room)
            {
                case "Intro":
                    currentConversationID = 0;
                    break;
                case "Kitchen":
                    currentConversationID = 1;
                    break;
                case "Lounge":
                    currentConversationID = 2;
                    break;
                case "Bedroom":
                    currentConversationID = 3;
                    break;
                case "Gym":
                    currentConversationID = 4;
                    break;
                case "Daily":
                    currentConversationID = 5;
                    break;
                case "Settings":
                    currentConversationID = 6;
                    break;

                case "NewBedroom":
                    currentConversationID = 7;
                    break;

                case "DontNeedSleep":
                    currentConversationID = 8;
                    break;

                case "AlreadyEaten":
                    currentConversationID = 9;
                    break;

                case "default":
                    conversationStarted = false;
                    break;
            }
            StartConversation(currentConversationID);
        }
    }

    // Start conversation with current conversation ID
    private void StartConversation(int conversationID)
    {
        DisplayDialogueLine(petConversations[conversationID].conversationChain[0].dialogueText, petConversations[conversationID].conversationChain[0].playerResponses);
    }

    // Advance to the next line in the current conversation chain if required
    // Called from UI manager with player response
    public void AdvanceLine(string response)
    {
        //Debug.Log("Player responded with " + response);
        //Debug.Log("Current conversation node count is " + petConversations[currentConversationID].conversationChain.Length);
        currentLineIndex++;
        // Check if the conversation chain has more nodes
        if (currentLineIndex < petConversations[currentConversationID].conversationChain.Length)
        {
            currentConversationComplete = false;

            // Call back to behaviour to save user response if required
            if (response != "_")
            {
                // Call back here

                //if healthy food
                //EatBehaviour.EatHealthFood();
            }
            // Display the next line of dialogue
            DisplayDialogueLine(petConversations[currentConversationID].conversationChain[currentLineIndex].dialogueText, petConversations[currentConversationID].conversationChain[currentLineIndex].playerResponses);
        }
        else
        {
            // Reset the conversation status
            conversationStarted = false;
            currentLineIndex = 0;
            currentConversationComplete = true;
        }
    }

    // Pet conversation display dialogue
    private void DisplayDialogueLine(string petText, string[] playerResponses)
    {
        UIManager.EnablePetDialogueText(petText);
        if (playerResponses[0] == "colour")
        {
            UIManager.DisplayColourSelections();
        }
        else if (playerResponses[0] == "time")
        {
            UIManager.DisplayTimeEntry();
        }
        else
        {
            UIManager.DisplayPlayerResponses(playerResponses);
        }
    }

    #endregion

    // Temporary functions to test functionality
    //private void Update()
    //{        
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        Debug.Log("Display a tip from Random Tips");
    //        DisplayTip("RandomTips");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        PetConversation("Intro");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        PetConversation("Kitchen");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        PetConversation("Gym");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        PetConversation("Lounge");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        PetConversation("Bedroom");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        PetConversation("Daily");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        PetConversation("Settings");
    //    }
    //}
}