using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Classes for dialogue database
    [System.Serializable]
    public class PetTips
    {
        public string tipType;
        public string[] tipList;
    }

    [System.Serializable]
    public class Conversations
    {
        public string dialogueText;
        public string[] playerResponses;
    }

    [System.Serializable]
    public class Test
    {
        public Conversations[] conversationList;
    }

    [Header("Game Objects")]
    [SerializeField] private UIScript UIManager;

    [Header("Dialogue Database")]
    [SerializeField] private PetTips[] petTips;
    [SerializeField] private Conversations[] gameIntro;
    [SerializeField] private Conversations[] roomDialogue;
    [SerializeField] private Conversations[] tiredPet;
    [SerializeField] private Test[] testingArrays;

    // Local variables
    private bool gameIntroComplete = false;
    private bool conversationStarted = false;
    private int currentConversationID = 0;
    private int currentLineIndex = 0;
    private readonly List<int> tipsDisplayed = new List<int>();

    #region Display Pet Tips

    // Find tip type in tips class depending on passed string
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
            Debug.Log("Tip ID " + tipID + " already shown in last " + (tipListLength / 2) + " displays.");
            tipID = Random.Range(0, tipListLength);
        }
        // Clear the already displayed tips once half the tips have been displayed
        if (tipsDisplayed.Count > (tipListLength / 2))
        {
            Debug.Log("Tips already displayed has been cleared");
            tipsDisplayed.Clear();
            tipsDisplayed.Add(tipID);
        }
        else
        {
            tipsDisplayed.Add(tipID);
        }
        // Display the tip for 3 seconds
        UIManager.EnablePetDialogueText(petTips[tipDepth].tipList[tipID], 3);
    }

    #endregion

    // Game introduction conversation
    public void GameIntroConversation()
    {
        if (!gameIntroComplete)
        {
            DisplayDialogueLine(gameIntro[currentLineIndex].dialogueText, gameIntro[currentLineIndex].playerResponses);
        }
        else
        {
            DisplayDialogueLine("We already went through the game setup", new[] { "Ok then" });
        }
    }

    // Pet conversations main function
    public void PetConversation(string room = default)
    {
        if (!conversationStarted)
        {
            conversationStarted = true;
            Debug.Log("Player started " + room + " conversation");
            /* Object Key
             * 0 - Kitchen
             * 1 - Gym
             * 2 - Lounge
             * 3 - Bedroom
             * 4 - Game Intro
        */
            switch (room)
            {
                case "Kitchen":
                    currentConversationID = 0;
                    break;
                case "Gym":
                    currentConversationID = 1;
                    break;
                case "Lounge":
                    currentConversationID = 2;
                    break;
                case "Bedroom":
                    currentConversationID = 3;
                    break;
                case "Intro":
                    currentConversationID = 0;
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
        //DisplayDialogueLine(roomDialogue[conversationID].dialogueText, roomDialogue[conversationID].playerResponses);
        DisplayDialogueLine(testingArrays[conversationID].conversationList[0].dialogueText, testingArrays[conversationID].conversationList[0].playerResponses);
    }

    // Advance to the next line in the current dialogue chain if required
    // Called from UI manager with player response
    public void AdvanceLine(string response)
    {
        Debug.Log("player responded with " + response);
        currentLineIndex++;
        if (currentLineIndex < testingArrays[currentConversationID].conversationList.Length)
        {
            DisplayDialogueLine(testingArrays[currentConversationID].conversationList[currentLineIndex].dialogueText, testingArrays[currentConversationID].conversationList[currentLineIndex].playerResponses);
            Debug.Log("There is more conversation");
            ///conversationStarted = false;
        }
        else
        {
            conversationStarted = false;
            currentLineIndex = 0;
        }
        

    }

    // Pet conversation display dialogue
    private void DisplayDialogueLine(string petText, string[] playerResponses)
    {
        UIManager.EnablePetDialogueText(petText);
        UIManager.DisplayPlayerResponses(playerResponses);
        //recompile
    }

    // Temporary functions to test functionality
    private void Start()
    {
        //DisplayTip("RandomTips");
        //StartConversation();
    }

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Display a tip from Random Tips");
            DisplayTip("RandomTips");
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Player started the game intro conversation");
            //GameIntroConversation();
            PetConversation("Intro");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            PetConversation("Kitchen");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            PetConversation("Gym");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            PetConversation("Lounge");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            PetConversation("Bedroom");
        }
    }
}
