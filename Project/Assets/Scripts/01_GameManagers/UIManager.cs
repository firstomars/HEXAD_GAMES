using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singelton

    private static UIManager _UIManagerInstance;

    public static UIManager UIManagerInstance { get { return _UIManagerInstance; } }

    private void Awake()
    {
        if (_UIManagerInstance != null) Destroy(gameObject);
        else _UIManagerInstance = this;
    }
    #endregion

    #region Inspector - Panels
    [Header("Panels")]
    [SerializeField] private GameObject backgrdPnl;
    [SerializeField] private GameObject responsePnl;
    [SerializeField] private GameObject colourPnl;
    [SerializeField] private GameObject entryPnl;
    [SerializeField] private GameObject petSpeechPnl;
    [SerializeField] private GameObject spiritLevelSldr;
    #endregion

    #region Inspector - Scene Buttons
    [Header("Scene Buttons")]
    [SerializeField] private GameObject menuBtn;
    [SerializeField] private GameObject playBtn;
    [SerializeField] private GameObject stagingBtn;
    [SerializeField] private GameObject timeBtn;
    [SerializeField] private GameObject playerStatsBtn;
    [SerializeField] private GameObject playerPrefsBtn;
    [SerializeField] private GameObject quitBtn;
    #endregion

    //===
    //NEW

    //classes set during runtime
    //[HideInInspector] public SleepBehaviour SleepBehaviour; -- DELETE
    [HideInInspector] public Behaviour CurrentBehaviour;


    [Header("PLAY SCENE INTERACTABLES - UI")]
    [SerializeField] private GameObject interactablesUIGO;

    [Header("Bedroom Interactables UI")]
    [SerializeField] private GameObject sendToBedBtnGO;
    private Button sendToBedBtn;
    [SerializeField] private GameObject wakeUpBtnGO;
    private Button wakeUpBtn;

    //===

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //===
        //CLARIFY BELOW
        SetResponsePnlUI(false);
        SetColourPnlUI(false);
        SetEntryPnlUI(false);
        SetPetPnlUI(false);
        //===


        //===
        //NEW

        //setup play scene buttons in runtime
        sendToBedBtn = sendToBedBtnGO.GetComponent<Button>();
        wakeUpBtn = wakeUpBtnGO.GetComponent<Button>();
        //===
    }

    public void SwitchSceneUI(string sceneName)
    {
        switch(sceneName)
        {
            case "01_Splash":
                SetSplashScreenUI(true);    //sceneIndex 0
                SetMenuScreenUI(false);     //sceneIndex 1
                SetPlayUI(false);           //sceneIndex 2
                SetAssetStagingUI(false);   //sceneIndex 3
                SetTimeUI(false);           //sceneIndex 4
                SetPlayerStatsUI(false);    //sceneIndex 5
                SetPlayerPrefsUI(false);    //sceneIndex 6
                break;

            case "02_Menu":
                SetSplashScreenUI(false);   //sceneIndex 0
                SetMenuScreenUI(true);      //sceneIndex 1
                SetPlayUI(false);           //sceneIndex 2
                SetAssetStagingUI(false);   //sceneIndex 3
                SetTimeUI(false);           //sceneIndex 4
                SetPlayerStatsUI(false);    //sceneIndex 5
                SetPlayerPrefsUI(false);    //sceneIndex 6
                break;

            case "03_Play_V02":
                SetSplashScreenUI(false);   //sceneIndex 0
                SetMenuScreenUI(false);     //sceneIndex 1
                SetPlayUI(true);            //sceneIndex 2
                SetAssetStagingUI(false);   //sceneIndex 3
                SetTimeUI(false);           //sceneIndex 4
                SetPlayerStatsUI(false);    //sceneIndex 5
                SetPlayerPrefsUI(false);    //sceneIndex 6
                break;

            case "04_AssetStaging":
                SetSplashScreenUI(false);   //sceneIndex 0
                SetMenuScreenUI(false);     //sceneIndex 1
                SetPlayUI(false);           //sceneIndex 2
                SetAssetStagingUI(true);    //sceneIndex 3
                SetTimeUI(false);           //sceneIndex 4
                SetPlayerStatsUI(false);    //sceneIndex 5
                SetPlayerPrefsUI(false);    //sceneIndex 6
                break;

            case "05_Time":
                SetSplashScreenUI(false);   //sceneIndex 0
                SetMenuScreenUI(false);     //sceneIndex 1
                SetPlayUI(false);           //sceneIndex 2
                SetAssetStagingUI(false);   //sceneIndex 3
                SetTimeUI(true);            //sceneIndex 4
                SetPlayerStatsUI(false);    //sceneIndex 5
                SetPlayerPrefsUI(false);    //sceneIndex 6
                break;

            case "06_PlayerStats":
                SetSplashScreenUI(false);   //sceneIndex 0
                SetMenuScreenUI(false);     //sceneIndex 1
                SetPlayUI(false);           //sceneIndex 2
                SetAssetStagingUI(false);   //sceneIndex 3
                SetTimeUI(false);           //sceneIndex 4
                SetPlayerStatsUI(true);     //sceneIndex 5
                SetPlayerPrefsUI(false);    //sceneIndex 6
                break;

            case "07_PlayerPrefs":
                SetSplashScreenUI(false);   //sceneIndex 0
                SetMenuScreenUI(false);     //sceneIndex 1
                SetPlayUI(false);           //sceneIndex 2
                SetAssetStagingUI(false);   //sceneIndex 3
                SetTimeUI(false);           //sceneIndex 4
                SetPlayerStatsUI(false);    //sceneIndex 5
                SetPlayerPrefsUI(true);     //sceneIndex 6
                break;
        }
    }

    private void SetSplashScreenUI(bool value)
    {
        menuBtn.SetActive(value);
    }

    private void SetMenuScreenUI(bool value)
    {
        playBtn.SetActive(value);
    }

    private void SetAssetStagingUI(bool value)
    {
        timeBtn.SetActive(value);
    }

    private void SetTimeUI(bool value)
    {
        playerStatsBtn.SetActive(value);
    }

    private void SetPlayerStatsUI(bool value)
    {
        playerPrefsBtn.SetActive(value);
    }

    private void SetPlayerPrefsUI(bool value)
    {
        quitBtn.SetActive(value);
    }

    private void SetPlayUI(bool value)
    {
        stagingBtn.SetActive(value);

        spiritLevelSldr.SetActive(false); // set to false for debugging purposes
        backgrdPnl.SetActive(false); // set to false for debugging purposes

        //NEW
        interactablesUIGO.SetActive(value);
        if(value) SwitchPlayRoomUI();
    }

    public void SwitchPlayRoomUI(string room = default)
    {
        ManageRoomUIListeners(room);

        switch(room)
        {
            case "bedroom":
                SetBedroomUI(true);
                break;

            default:
                SetBedroomUI(false);
                break;
        }
    }

    private void ManageRoomUIListeners(string room = default)
    {
        if (CurrentBehaviour == null) return;

        switch (room)
        {
            case "bedroom":
                sendToBedBtn.onClick.AddListener(CurrentBehaviour.SendToBed);
                wakeUpBtn.onClick.AddListener(CurrentBehaviour.WakePetUp);
                break;

            //add other cases

            default:
                sendToBedBtn.onClick.RemoveAllListeners();
                wakeUpBtn.onClick.RemoveAllListeners();
                break;
        }
    }

    #region BedroomUI

    private void SetBedroomUI(bool value)
    {
        Debug.Log("bedroom UI set to " + value);
        sendToBedBtnGO.SetActive(value);
        if (value == false) wakeUpBtnGO.SetActive(value);
    }

    public void SendToBedBtnClicked()
    {
        sendToBedBtnGO.SetActive(false);
        wakeUpBtnGO.SetActive(true);
    }

    public void WakeUpBtnClicked()
    {
        sendToBedBtnGO.SetActive(false);
        wakeUpBtnGO.SetActive(false);
    }

    #endregion

    //CLARIFY ON BEST SPORT
    #region Panel UI
    private void SetResponsePnlUI(bool value)
    {
        responsePnl.SetActive(value);
    }

    private void SetColourPnlUI(bool value)
    {
        colourPnl.SetActive(value);
    }

    private void SetEntryPnlUI(bool value)
    {
        entryPnl.SetActive(value);
    }

    private void SetPetPnlUI(bool value)
    {
        petSpeechPnl.SetActive(value);
    }

    #endregion

    private void UpdateSpiritLevelUI(bool value)
    {
        
    }

    #region Scene Management

    public void GoToMenuScene()
    {
        GameManager.Instance.MenuScene();
        SetSplashScreenUI(false);
        SetMenuScreenUI(true);
    }

    public void GoToPlayScene()
    {
        GameManager.Instance.PlayScene();
        SetSplashScreenUI(false);
        SetMenuScreenUI(false);
        SetPlayUI(true);
    }

    public void GoToStagingScene()
    {
        GameManager.Instance.StagingScene();
        SetSplashScreenUI(false);
        SetMenuScreenUI(false);
        SetAssetStagingUI(true);
        SetPlayUI(false);
    }

    public void GoToTimeScene()
    {
        GameManager.Instance.TimeScene();
        SetSplashScreenUI(false);
        SetMenuScreenUI(false);
        SetTimeUI(true);
    }

    public void GoToPlayerStatsScene()
    {
        GameManager.Instance.PlayerStatsScene();
        SetSplashScreenUI(false);
        SetMenuScreenUI(false);
        SetTimeUI(false);
        SetPlayerStatsUI(true);
    }

    public void GoToPlayerPrefsScene()
    {
        GameManager.Instance.PlayerPrefsScene();
        SetSplashScreenUI(false);
        SetMenuScreenUI(false);
        SetTimeUI(false);
        SetPlayerStatsUI(false);
        SetPlayerPrefsUI(true);
    }

    #endregion

    // Methods called from converse class

    public void DisplayPetSpeechBubble(string textToDisplay)
    {
        SetPetPnlUI(true);
    }

    /*
     * Testing functions to show UI in build ONLY!!!!
     * Not Final
     * Do not fuick with!!!
     */

    [Header("Testing only -- not final")]
    [SerializeField] private GameObject mainFlyoutPanel;
    [SerializeField] private GameObject walkFlyoutPanel;

    // Local class variables
    private bool mainFlyoutActivated = false;
    private bool walkFlyoutActivated = false;

    // Main flyout button pressed
    public void ActivateMainFlyoutMenu()
    {
        if (!mainFlyoutActivated)
        {
            mainFlyoutPanel.SetActive(true);
            mainFlyoutActivated = true;
        }
        else
        {
            CloseAllFlyouts();
        }
    }

    // Walk flyout button pressed
    public void ActivateWalkFlyoutMenu()
    {
        if (!walkFlyoutActivated)
        {
            walkFlyoutPanel.SetActive(true);
            walkFlyoutActivated = true;
        }
        else
        {
            walkFlyoutPanel.SetActive(false);
            walkFlyoutActivated = false;
        }
    }

    private void CloseAllFlyouts()
    {
        if (walkFlyoutActivated)
        {
            walkFlyoutPanel.SetActive(false);
            walkFlyoutActivated = false;
        }
        mainFlyoutPanel.SetActive(false);
        mainFlyoutActivated = false;
    }

    /*
     * End testing code
     * Fuck away!!!
     */
}
