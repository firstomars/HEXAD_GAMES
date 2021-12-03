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

    [HideInInspector] public Behaviour CurrentBehaviour;

    private string previousConversationRoom = " ";

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
            //else
            //{
            //    return 0;
            //}
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
        if (room != previousConversationRoom) conversationStarted = false;
        
        if (!conversationStarted)
        {
            conversationStarted = true;
            previousConversationRoom = room;
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

                case "BedroomWakeUp":
                    currentConversationID = 10;
                    break;

                case "BedroomMinigame":
                    currentConversationID = 11;
                    break;

                case "BedroomTooTiredForGames":
                    currentConversationID = 12;
                    break;

                case "GymStopWorkout":
                    currentConversationID = 13;
                    break;

                case "BedroomWakeUpFromNap":
                    currentConversationID = 14;
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

        int conversationIndexLength = petConversations[currentConversationID].conversationChain.Length;

        if (currentLineIndex == conversationIndexLength)
        {
            currentConversationComplete = false;

            // Call back to behaviour to save user response if required
            if (response != "_") ChooseAction(response);

            // Display the next line of dialogue
            conversationStarted = false;
            currentLineIndex = 0;
            currentConversationComplete = true;
        }
        else if (currentLineIndex < conversationIndexLength)
        {
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

    private void ChooseAction(string response)
    {
        switch (response)
        {
            case "Healthy...":
                Debug.Log("healthy food option chosen");
                CurrentBehaviour.EatFood();
                break;

            case "I want pizza!":
                Debug.Log("unhealthy food option chosen");
                CurrentBehaviour.EatJunkFood();
                break;

            case "Yeah buddy, grab those dumbbells.":
                Debug.Log("pet asked to use dumbbells");
                CurrentBehaviour.StartWorkout();
                break;

            case "Time for bed!":
                Debug.Log("pet asked to go to sleep");
                CurrentBehaviour.SendToBed();
                break;

            case "I'll take a quick nap.":
                Debug.Log("pet asked to take a nap");
                CurrentBehaviour.SendToBedForNap();
                break;

            case "Yep - nap time over!":
                Debug.Log("pet wakes from nap");
                CurrentBehaviour.WakePetUpFromNap();
                break;

            case "Let's play a minigame.":
                Debug.Log("pet asked to play minigame");
                CurrentBehaviour.PlayMiniGame(); //starts minigame
                break;

            case "Yeah that's enough.":
                Debug.Log("pet asked to stop playing minigame");
                CurrentBehaviour.StopMiniGame(); //stops minigame
                break;

            case "Yep it's time to get up.":
                Debug.Log("pet to wake up");
                CurrentBehaviour.WakePetUp();
                break;

            case "Alright let's take a break.":
                Debug.Log("pet stops working out");
                CurrentBehaviour.StopWorkout();
                break;

            default:
                Debug.Log("Response did not match actions - no action taken");
                break;
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
}