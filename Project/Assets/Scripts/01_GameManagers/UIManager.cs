using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    //[Header("Panels")]
    //[SerializeField] private GameObject backgrdPnl;
    //[SerializeField] private GameObject responsePnl;
    //[SerializeField] private GameObject colourPnl;
    //[SerializeField] private GameObject entryPnl;
    //[SerializeField] private GameObject petSpeechPnl;
    //[SerializeField] private GameObject spiritLevelSldr;
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

    //classes set during runtime
    [HideInInspector] public Behaviour CurrentBehaviour;
    [HideInInspector] public UpgradeManager UpgradeManager;
    [HideInInspector] public TimeController TimeController;


    [Header("PLAY SCENE INTERACTABLES - UI")]
    [SerializeField] private GameObject interactablesUIGO;

    [Header("Navigation - UI Panels")]
    [SerializeField] private GameObject flyoutButtonPanel;
    [SerializeField] private GameObject mainFlyoutPanel;
    [SerializeField] private GameObject walkFlyoutPanel;

    [Header("Flyout Button Images")]
    [SerializeField] private Sprite activateFlyoutImage;
    [SerializeField] private Sprite deactivateFlyoutImage;

    // Local class variables
    private bool isMainFlyoutActivated = false;
    private bool isWalkFlyoutActivated = false;
    private bool isUpgradeFlyoutActivated = false;
    private bool isSettingsFlyoutActivated = false;

    [Header("Navigation - UI")]
    [SerializeField] private GameObject kitchenBtnGO;
    private Button kitchenBtn;
    [SerializeField] private GameObject gymBtnGO;
    private Button gymBtn;
    [SerializeField] private GameObject bedroomBtnGO;
    private Button bedroomBtn;
    [SerializeField] private GameObject trophyCabinetBtnGO;
    private Button trophyCabinetBtn;
    [SerializeField] private GameObject livingRoomBtnGO;
    private Button livingRoomBtn;

    [Header("Player Statistics - UI")]
    //[SerializeField] private Text energyLevelText;
    //[SerializeField] private Text fulfillmentLevelText;
    //[SerializeField] private Text spiritLevelText;
    //[SerializeField] private Text sleepDollarsLevelText;
    [SerializeField] private Slider spiritSlider;
    [SerializeField] private Slider energySlider;
    [SerializeField] private Slider fulfilmentSlider;
    [SerializeField] private TextMeshProUGUI hoursSleptText;
    [SerializeField] private TextMeshProUGUI minigamesPlayed;
    [SerializeField] private TextMeshProUGUI mealsEatenValue;
    [SerializeField] private TextMeshProUGUI sleepGoalsValue;
    [SerializeField] private TextMeshProUGUI sleepDollarsValue;
    [SerializeField] private TextMeshProUGUI completionValue;

    //NEW
    [SerializeField] private GameObject playerReportBackgroundPanel;
    [SerializeField] private GameObject playerStatisticsPanel;
    private bool spiritLevelPressed;
    private bool isRoomSet = false;
    private string currentRoom = "default";


    [Header("Bedroom Interactables - UI")]
    [SerializeField] private GameObject sendToBedBtnGO;
    private Button sendToBedBtn;
    [SerializeField] private GameObject wakeUpBtnGO;
    private Button wakeUpBtn;
    [SerializeField] private GameObject miniGameBtnGO;
    private Button miniGameBtn;

    [Header("Kitchen Interactables - UI")]
    [SerializeField] private GameObject eatFoodBtnGO;
    private Button eatFoodBtn;
    [SerializeField] private GameObject eatJunkFoodBtnGO;
    private Button eatJunkFoodBtn;

    [Header("Gym Interactables - UI")]
    [SerializeField] private GameObject benchPressBtnGO;
    private Button benchPressBtn;

    [Header("Report Interactables UI")]
    [SerializeField] private GameObject reportUiObj;
    [SerializeField] private GameObject reportText;
    [SerializeField] private GameObject bedTimeInputFieldObj;
    [SerializeField] private GameObject wakeUpTimeInputFieldObj;
    [SerializeField] private GameObject hrsSleptNightOneTitle;
    [SerializeField] private GameObject hrsSleptNightTwoTitle;
    [SerializeField] private GameObject hrsSleptNightOneTextObj;
    [SerializeField] private GameObject hrsSleptNightTwoTextObj;
    private Text hrsSleptNightOneText;
    private Text hrsSleptNightTwoText;
    [SerializeField] private GameObject closeReportBtnObj;
    private Button closeReportBtn;
    [SerializeField] private GameObject viewGoalsBtnObj;
    private Button viewGoalsBtn;
    [SerializeField] private GameObject goals;
    [SerializeField] private Text goalOneText;
    [SerializeField] private Text goalTwoText;
    [SerializeField] private Text goalThreeText;
    [SerializeField] private Text goalFourText;
    [SerializeField] private Text goalFiveText;
    [HideInInspector] public int bedTime = -1;
    [HideInInspector] public int wakeUpTime = -1;

    [Header("Upgrade Interactables UI")]
    [SerializeField] private GameObject upgradeBedBtnGO;
    private Button upgradeBedBtn;



    //===

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //===
        //CLARIFY BELOW
        //SetResponsePnlUI(false);
        //SetColourPnlUI(false);
        //SetEntryPnlUI(false);
        //SetPetPnlUI(false);
        //===

        #region PlayScene Buttons
        //setup play scene buttons in runtime

        //navigation UI
        kitchenBtn = kitchenBtnGO.GetComponent<Button>();
        gymBtn = gymBtnGO.GetComponent<Button>();
        bedroomBtn = bedroomBtnGO.GetComponent<Button>();
        trophyCabinetBtn = trophyCabinetBtnGO.GetComponent<Button>();
        livingRoomBtn = livingRoomBtnGO.GetComponent<Button>();

        //bedroom UI
        sendToBedBtn = sendToBedBtnGO.GetComponent<Button>();
        wakeUpBtn = wakeUpBtnGO.GetComponent<Button>();
        miniGameBtn = miniGameBtnGO.GetComponent<Button>();

        //kitchen UI
        eatFoodBtn = eatFoodBtnGO.GetComponent<Button>();
        eatJunkFoodBtn = eatJunkFoodBtnGO.GetComponent<Button>();

        //gym UI
        benchPressBtn = benchPressBtnGO.GetComponent<Button>();

        //report UI
        hrsSleptNightOneText = hrsSleptNightOneTextObj.GetComponent<Text>();
        hrsSleptNightTwoText = hrsSleptNightTwoTextObj.GetComponent<Text>();
        closeReportBtn = closeReportBtnObj.GetComponent<Button>();
        viewGoalsBtn = viewGoalsBtnObj.GetComponent<Button>();

        //upgrades UI
        upgradeBedBtn = upgradeBedBtnGO.GetComponent<Button>();
        //===

        #endregion
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
        //quitBtn.SetActive(value);
    }

    private void SetPlayUI(bool value)
    {
        stagingBtn.SetActive(false);

        quitBtn.SetActive(false);

        //spiritLevelSldr.SetActive(false); // set to false for debugging purposes
        //backgrdPnl.SetActive(false); // set to false for debugging purposes

        interactablesUIGO.SetActive(value);
    }

    public void SwitchPlayRoomUI(string room = default)
    {
        if (!isRoomSet) currentRoom = room;

        switch (room)
        {
            case "bedroom":
                SetBedroomUI(true);
                SetReportUI(false);
                SetKitchenUI(false);
                SetGymUI(false);
                break;

            case "report":
                SetBedroomUI(false);
                SetReportUI(true);
                SetKitchenUI(false);
                SetGymUI(false);
                break;

            case "kitchen":
                SetBedroomUI(false);
                SetReportUI(false);
                SetKitchenUI(true);
                SetGymUI(false);
                break;

            case "gym":
                SetBedroomUI(false);
                SetReportUI(false);
                SetKitchenUI(false);
                SetGymUI(true);
                break;

            default:
                SetBedroomUI(false);
                SetReportUI(false);
                SetKitchenUI(false);
                SetGymUI(false);
                break;
        }
    }

    #region Seek UI Listeners

    public void SetNavigationUIListeners(SeekBehaviour seekBehaviour)
    {          
        kitchenBtn.onClick.AddListener(seekBehaviour.SeekKitchen);
        gymBtn.onClick.AddListener(seekBehaviour.SeekGym);
        bedroomBtn.onClick.AddListener(seekBehaviour.SeekBedroom);
        trophyCabinetBtn.onClick.AddListener(seekBehaviour.SeekTrophyCabinet);
        livingRoomBtn.onClick.AddListener(seekBehaviour.SeekLivingRoom);
    }

    #endregion 

    #region Main Menu

    public void ActivateMainFlyoutMenu()
    {
        if (!isMainFlyoutActivated)
        {
            mainFlyoutPanel.SetActive(true);
            isMainFlyoutActivated = true;
            flyoutButtonPanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = deactivateFlyoutImage;
        }
        else
        {
            CloseAllFlyouts();
        }
    }

    // Walk flyout button pressed
    public void ActivateWalkFlyoutMenu()
    {
        if (!isWalkFlyoutActivated)
        {
            if (isUpgradeFlyoutActivated) ActivateUpgradeFlyoutMenu();
            if (isSettingsFlyoutActivated) ActivateSettingsFlyoutMenu();

            walkFlyoutPanel.SetActive(true);
            isWalkFlyoutActivated = true;
        }
        else
        {
            walkFlyoutPanel.SetActive(false);
            isWalkFlyoutActivated = false;
        }
    }

    //upgrade flyout button pressed
    public void ActivateUpgradeFlyoutMenu()
    {
        isUpgradeFlyoutActivated = !isUpgradeFlyoutActivated;
        upgradeBedBtnGO.SetActive(isUpgradeFlyoutActivated);

        if (isUpgradeFlyoutActivated)
        {
            if (isWalkFlyoutActivated) ActivateWalkFlyoutMenu();
            if (isSettingsFlyoutActivated) ActivateSettingsFlyoutMenu();
        }
    }

    //setting flyout button pressed
    public void ActivateSettingsFlyoutMenu()
    {
        isSettingsFlyoutActivated = !isSettingsFlyoutActivated;
        quitBtn.SetActive(isSettingsFlyoutActivated);
        if (TimeController != null) TimeController.ToggleTimeUI(isSettingsFlyoutActivated);

        if (isSettingsFlyoutActivated)
        {
            if (isWalkFlyoutActivated) ActivateWalkFlyoutMenu();
            if (isUpgradeFlyoutActivated) ActivateUpgradeFlyoutMenu();
        }
    }

    public void CloseAllFlyouts()
    {
        if (isWalkFlyoutActivated)
        {
            walkFlyoutPanel.SetActive(false);
            isWalkFlyoutActivated = false;
        }
        mainFlyoutPanel.SetActive(false);
        isMainFlyoutActivated = false;
        flyoutButtonPanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = activateFlyoutImage;
    }

    #endregion

    #region UpgradesUI

    public void UpgradeBed()
    {
        UpgradeManager.UpgradeObject("UpgradeBed (1)");
    }

    #endregion

    #region Stats

    public void StatisticsUpdate(float energyValue, float fulfilValue, float spiritValue, string hrsSleptLastNight, string minigamesPlayedToday, string mealsEatenToday, string sleepGoalsMet, string sleepDollarsAmt)
    {
        energySlider.value = energyValue;
        fulfilmentSlider.value = fulfilValue;
        spiritSlider.value = spiritValue;

        hoursSleptText.text = hrsSleptLastNight;
        minigamesPlayed.text = minigamesPlayedToday;
        mealsEatenValue.text = mealsEatenToday;
        sleepGoalsValue.text = sleepGoalsMet;
        sleepDollarsValue.text = sleepDollarsAmt;
        //add completion value
    }

    public void SpiritLevelPressed()
    {
        if (!spiritLevelPressed)
        {
            //close flyouts
            CloseAllFlyouts();

            //view stats
            ViewPlayerStatistics();
            spiritLevelPressed = true;

            //close room ui
            isRoomSet = true;
            SwitchPlayRoomUI();
        }
        else
        {
            //close stats
            StartCoroutine(DisableUIElementsAfterSeconds(0, new[] { playerStatisticsPanel, playerReportBackgroundPanel }));
            spiritLevelPressed = false;

            //reopen room ui
            isRoomSet = false;
            SwitchPlayRoomUI(currentRoom);
        }
    }

    private void ViewPlayerStatistics()
    {
        playerReportBackgroundPanel.SetActive(true);
        playerStatisticsPanel.SetActive(true);
    }

    #endregion

    #region ReportUI

    private void SetReportUI(bool value)
    {
        reportUiObj.SetActive(value);
        goals.SetActive(false);
    }

    public void CloseReportButton()
    {
        reportUiObj.SetActive(false);
    }

    public void ViewGoalsButton()
    {
        goals.SetActive(true);
    }

    public void SetGoalsText(string[] goalsText)
    {
        goalOneText.text = goalsText[0];
        goalTwoText.text = goalsText[1];
        goalThreeText.text = goalsText[2];
        goalFourText.text = goalsText[3];
        goalFiveText.text = goalsText[4];
    }

    //bedtime input field
    public void BedtimeInputField(string bedTimeInput)
    {
        bool isNumeric = int.TryParse(bedTimeInput, out _);
        if (isNumeric) bedTime = Int16.Parse(bedTimeInput);
        else Debug.Log("Only ints can be passed in");
    }

    //wake up time input field
    public void WakeUpTimeInputField(string wakeUpInput)
    {
        bool isNumeric = int.TryParse(wakeUpInput, out _);
        if (isNumeric) wakeUpTime = Int16.Parse(wakeUpInput);
        else Debug.Log("Only ints can be passed in");
    }

    public int GetBedtime()
    {
        int timeToReturn = bedTime;
        bedTime = -1;
        return timeToReturn;
    }

    public int GetWakeUpTime()
    {
        int timeToReturn = wakeUpTime;
        wakeUpTime= -1;
        return timeToReturn;
    }

    public void SetHoursSleptText(int hrsSlept)
    {
        hrsSleptNightOneText.text = hrsSlept.ToString();
    }

    public void SetHoursSleptTextNightOneTwo(Vector2Int hrsSlept)
    {
        hrsSleptNightOneText.text = hrsSlept[0].ToString();
        hrsSleptNightTwoText.text = hrsSlept[1].ToString();  
        
        //hrsSleptNightTwoText.text = hrsSleptNightTwo.ToString();
        //hrsSleptNightOneText.text = hrsSleptNightOne.ToString();
    }

    #endregion

    #region BedroomUI

    private void SetBedroomUI(bool value)
    {
        //Debug.Log("bedroom UI set to " + value);
        miniGameBtnGO.SetActive(value);
        sendToBedBtnGO.SetActive(value);

        if (value == false) wakeUpBtnGO.SetActive(value);

        SetBedroomUIListeners(value);
    }

    private void SetBedroomUIListeners(bool value)
    {
        if (value == true)
        {
            sendToBedBtn.onClick.AddListener(CurrentBehaviour.SendToBed);
            wakeUpBtn.onClick.AddListener(CurrentBehaviour.WakePetUp);
            miniGameBtn.onClick.AddListener(CurrentBehaviour.PlayMiniGame);
        }
        else
        {
            sendToBedBtn.onClick.RemoveAllListeners();
            wakeUpBtn.onClick.RemoveAllListeners();
            miniGameBtn.onClick.RemoveAllListeners();
        }
    }

    public void SendToBedBtnClicked()
    {
        sendToBedBtnGO.SetActive(false);
        wakeUpBtnGO.SetActive(true);
    }

    public void WakeUpBtnClicked()
    {
        sendToBedBtnGO.SetActive(true);
        wakeUpBtnGO.SetActive(false);
    }

    public void WakeUpNextDayBtnClicked()
    {
        sendToBedBtnGO.SetActive(false);
        wakeUpBtnGO.SetActive(false);
    }

    #endregion

    #region KitchenUI

    private void SetKitchenUI(bool value)
    {
        //Debug.Log("kitchen UI set to " + value);
        eatFoodBtnGO.SetActive(value);
        eatJunkFoodBtnGO.SetActive(value);

        SetKitchenUIListeners(value);
    }

    private void SetKitchenUIListeners(bool value)
    {
        if (value == true)
        {
            eatFoodBtn.onClick.AddListener(CurrentBehaviour.EatFood);
            eatJunkFoodBtn.onClick.AddListener(CurrentBehaviour.EatJunkFood);
        }
        else
        {
            eatFoodBtn.onClick.RemoveAllListeners();
            eatJunkFoodBtn.onClick.RemoveAllListeners();
        }
    }

    #endregion

    #region GymUI

    private void SetGymUI(bool value)
    {
        //Debug.Log("gym UI set to " + value);
        benchPressBtnGO.SetActive(value);

        SetGymUIListeners(value);
    }

    private void SetGymUIListeners(bool value)
    {
        if (value == true)
        {
            benchPressBtn.onClick.AddListener(CurrentBehaviour.BenchPress);
        }
        else
        {
            benchPressBtn.onClick.RemoveAllListeners();
        }
    }

    #endregion

    //CLARIFY ON BEST SPORT
    #region Panel UI
    //private void SetResponsePnlUI(bool value)
    //{
    //    responsePnl.SetActive(value);
    //}

    //private void SetColourPnlUI(bool value)
    //{
    //    colourPnl.SetActive(value);
    //}

    //private void SetEntryPnlUI(bool value)
    //{
    //    entryPnl.SetActive(value);
    //}

    //private void SetPetPnlUI(bool value)
    //{
    //    petSpeechPnl.SetActive(value);
    //}

    #endregion

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

    public void QuitGame()
    {
        SetSplashScreenUI(false);
        SetMenuScreenUI(false);
        SetTimeUI(false);
        SetPlayerStatsUI(false);
        SetPlayerPrefsUI(false);
        GameManager.Instance.QuitGame();
    }

    #endregion

    // Methods called from converse class
    //public void DisplayPetSpeechBubble(string textToDisplay)
    //{
    //    SetPetPnlUI(true);
    //}

    // Coroutine used to disable multiple UI elements after a set period of time
    IEnumerator DisableUIElementsAfterSeconds(int timeToDisplay, GameObject[] UIElements)
    {
        yield return new WaitForSeconds(timeToDisplay);
        foreach (GameObject UIElement in UIElements)
        {
            UIElement.SetActive(false);
        }
    }
}