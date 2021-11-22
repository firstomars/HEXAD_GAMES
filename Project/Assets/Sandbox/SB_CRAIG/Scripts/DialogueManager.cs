using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Classes for dialogue database
    [System.Serializable] public class PetTips
    {
        public string tipType;
        public string[] tipList;
    }

    [System.Serializable] public class GameIntroSteps
    {
        public string dialogueText;
        public string[] playerResponses;
    }

    [Header("Game Objects")]
    [SerializeField] private UIScript UIManager;

    [Header("Dialogue Database")]
    [SerializeField] private PetTips[] petTips;
    [SerializeField] private GameIntroSteps[] gameIntro;

    // Local variables
    private bool gameIntroComplete = false;

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
        return 0;
    }

    // Display random tip from dialogue database depending on passed string
    public void DisplayTip(string tipType)
    {
        int tipDepth = GetTipDepth(tipType);
        int tipID = Random.Range(0, petTips[tipDepth].tipList.Length);
        UIManager.EnablePetDialogueText(petTips[tipDepth].tipList[tipID], 10);
    }

    #endregion

    // Pet conversations main function
    public void StartConversation()
    {
        if (!gameIntroComplete)
        {
            for (int i = 0; i < gameIntro.Length; i++)
            {
                DisplayDialogueLine(i);
                break;
            }
        }
    }

    // Pet conversation display dialogue
    private void DisplayDialogueLine(int index)
    {
        UIManager.EnablePetDialogueText(gameIntro[index].dialogueText);
        UIManager.DisplayPlayerResponses(gameIntro[index].playerResponses);
    }

    // Temporary functions to test functionality
    private void Start()
    {
        //DisplayTip("RandomTips");
        StartConversation();
    }

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Player pressed T");
            DisplayTip("RandomTips");
        }
    }
}
