using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

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
    [SerializeField] private GameObject menuBG;
    [SerializeField] private GameObject gameTitle;
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
    private bool hasUpgradeBeenCalled = false;
    private int nextHoursPage = 0;
    private readonly List<string> inputHours = new List<string> { "00:00", "01:00", "02:00", "03:00", "04:00", "05:00",
                                                            "06:00", "07:00", "08:00", "09:00", "10:00", "11:00",
                                                            "12:00", "13:00", "14:00", "15:00", "16:00", "17:00",
                                                            "18:00", "19:00", "20:00", "21:00", "22:00", "23:00" };

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

    [Header("Report Panel Background")]
    [SerializeField] private GameObject playerReportBackgroundPanel;

    [Header("Player Statistics - UI")]
    [SerializeField] private GameObject playerStatisticsPanel;
    [SerializeField] private Slider spiritSlider;
    [SerializeField] private Slider energySlider;
    [SerializeField] private Slider fulfilmentSlider;
    [SerializeField] private TextMeshProUGUI hoursSleptText;
    [SerializeField] private TextMeshProUGUI minigamesPlayed;
    [SerializeField] private TextMeshProUGUI mealsEatenValue;
    [SerializeField] private TextMeshProUGUI sleepGoalsValue;
    [SerializeField] private TextMeshProUGUI sleepDollarsValue;
    [SerializeField] private TextMeshProUGUI completionValue;
    private bool spiritLevelPressed;
    private bool isRoomSet = false;
    private string currentRoom = "default";

    [Header("Daily Report - UI")]
    [SerializeField] private GameObject playerDailyReportPanel;
    [SerializeField] private TextMeshProUGUI setBedTime;
    [SerializeField] private TextMeshProUGUI setWakeTime;
    [SerializeField] private TextMeshProUGUI hoursSleptLastNight;
    [SerializeField] private TextMeshProUGUI hoursSleptPreviousNight;
    [SerializeField] private TextMeshProUGUI trophyDescriptionText;
    [SerializeField] private GameObject trophyAwardPanel;

    [Header("Pet Dialogue Text Panel")]
    [SerializeField] private GameObject petDialoguePanel;
    [SerializeField] private TextMeshProUGUI petDialogueText;
    [SerializeField] private GameObject playerResponsePanel;
    [SerializeField] private GameObject petColourPanel;
    [SerializeField] private GameObject timeEntryPanel;
    [HideInInspector] public DialogueManager DialogueManager;


    [Header("Button Prefabs - Player Responses")]
    [SerializeField] private GameObject playerResponseButtonPrefab;
    [SerializeField] private GameObject colourSwatchButtonPrefab;
    [SerializeField] private GameObject timeEntryButtonPrefab;

    [Header("Bedroom Interactables - UI")]
    [SerializeField] private GameObject sendToBedBtnGO;
    private Button sendToBedBtn;
    [SerializeField] private GameObject wakeUpBtnGO;
    private Button wakeUpBtn;
    [SerializeField] private GameObject wakeUpFromNapBtnGO;
    private Button wakeUpFromNapBtn;
    [SerializeField] private GameObject miniGameBtnGO;
    private Button miniGameBtn;
    [SerializeField] private GameObject bedroomInteractBtnGO;
    private Button bedroomInteractBtn;
    private bool isMinigameBeingPlayed = false;
    private bool isPetNapping = false;
    private bool isPetSleeping = false;

    [Header("Kitchen Interactables - UI")]
    [SerializeField] private GameObject eatFoodBtnGO;
    private Button eatFoodBtn;
    [SerializeField] private GameObject eatJunkFoodBtnGO;
    private Button eatJunkFoodBtn;
    [SerializeField] private GameObject kitchenInteractBtnGO;
    private Button kitchenInteractBtn;

    [Header("Gym Interactables - UI")]
    [SerializeField] private GameObject benchPressBtnGO;
    private Button benchPressBtn;
    [SerializeField] private GameObject gymInteractBtnGO;
    private Button gymInteractBtn;

    [Header("Lounge Interactables - UI")]
    [SerializeField] private GameObject loungeInteractBtnGO;
    private Button loungeInteractBtn;

    [Header("Report Interactables UI")]
    [SerializeField] public GameObject UIMorningReportObj;

    [Header("Upgrade Interactables UI")]
    [SerializeField] private GameObject upgradeBedBtnGO;
    private Button upgradeBedBtn;

    [Header("UI Required Inputs")]
    [SerializeField] private string[] petColours;
    [SerializeField] public Material playerMaterial;
    [HideInInspector] public bool isPetColourSet = false;
    [HideInInspector] public List<string> timeEntries;

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
        wakeUpFromNapBtn = wakeUpFromNapBtnGO.GetComponent<Button>();
        miniGameBtn = miniGameBtnGO.GetComponent<Button>();
        bedroomInteractBtn = bedroomInteractBtnGO.GetComponent<Button>();

        //kitchen UI
        eatFoodBtn = eatFoodBtnGO.GetComponent<Button>();
        eatJunkFoodBtn = eatJunkFoodBtnGO.GetComponent<Button>();
        kitchenInteractBtn = kitchenInteractBtnGO.GetComponent<Button>();

        //gym UI
        benchPressBtn = benchPressBtnGO.GetComponent<Button>();
        gymInteractBtn = gymInteractBtnGO.GetComponent<Button>();

        //lounge UI
        loungeInteractBtn = loungeInteractBtnGO.GetComponent<Button>();

        //upgrades UI
        upgradeBedBtn = upgradeBedBtnGO.GetComponent<Button>();
        //===

        #endregion
    }

    public void SwitchSceneUI(string sceneName)
    {
        switch(sceneName)
        {
            case "01_Menu":
                SetSplashScreenUI(false);   //sceneIndex 0
                SetMenuScreenUI(true);      //sceneIndex 1
                SetPlayUI(false);           //sceneIndex 2
                SetAssetStagingUI(false);   //sceneIndex 3
                SetTimeUI(false);           //sceneIndex 4
                SetPlayerStatsUI(false);    //sceneIndex 5
                SetPlayerPrefsUI(false);    //sceneIndex 6
                break;

            case "02_Play_V02":
                SetSplashScreenUI(false);   //sceneIndex 0
                SetMenuScreenUI(false);     //sceneIndex 1
                SetPlayUI(true);            //sceneIndex 2
                SetAssetStagingUI(false);   //sceneIndex 3
                SetTimeUI(false);           //sceneIndex 4
                SetPlayerStatsUI(false);    //sceneIndex 5
                SetPlayerPrefsUI(false);    //sceneIndex 6
                break;

                #region ObsoleteScenes
                //case "01_Splash":
                //    SetSplashScreenUI(true);    //sceneIndex 0
                //    SetMenuScreenUI(false);     //sceneIndex 1
                //    SetPlayUI(false);           //sceneIndex 2
                //    SetAssetStagingUI(false);   //sceneIndex 3
                //    SetTimeUI(false);           //sceneIndex 4
                //    SetPlayerStatsUI(false);    //sceneIndex 5
                //    SetPlayerPrefsUI(false);    //sceneIndex 6
                //    break;


                //case "04_AssetStaging":
                //    SetSplashScreenUI(false);   //sceneIndex 0
                //    SetMenuScreenUI(false);     //sceneIndex 1
                //    SetPlayUI(false);           //sceneIndex 2
                //    SetAssetStagingUI(true);    //sceneIndex 3
                //    SetTimeUI(false);           //sceneIndex 4
                //    SetPlayerStatsUI(false);    //sceneIndex 5
                //    SetPlayerPrefsUI(false);    //sceneIndex 6
                //    break;

                //case "05_Time":
                //    SetSplashScreenUI(false);   //sceneIndex 0
                //    SetMenuScreenUI(false);     //sceneIndex 1
                //    SetPlayUI(false);           //sceneIndex 2
                //    SetAssetStagingUI(false);   //sceneIndex 3
                //    SetTimeUI(true);            //sceneIndex 4
                //    SetPlayerStatsUI(false);    //sceneIndex 5
                //    SetPlayerPrefsUI(false);    //sceneIndex 6
                //    break;

                //case "06_PlayerStats":
                //    SetSplashScreenUI(false);   //sceneIndex 0
                //    SetMenuScreenUI(false);     //sceneIndex 1
                //    SetPlayUI(false);           //sceneIndex 2
                //    SetAssetStagingUI(false);   //sceneIndex 3
                //    SetTimeUI(false);           //sceneIndex 4
                //    SetPlayerStatsUI(true);     //sceneIndex 5
                //    SetPlayerPrefsUI(false);    //sceneIndex 6
                //    break;

                //case "07_PlayerPrefs":
                //    SetSplashScreenUI(false);   //sceneIndex 0
                //    SetMenuScreenUI(false);     //sceneIndex 1
                //    SetPlayUI(false);           //sceneIndex 2
                //    SetAssetStagingUI(false);   //sceneIndex 3
                //    SetTimeUI(false);           //sceneIndex 4
                //    SetPlayerStatsUI(false);    //sceneIndex 5
                //    SetPlayerPrefsUI(true);     //sceneIndex 6
                //    break;
                #endregion
        }
    }

    private void SetSplashScreenUI(bool value)
    {
        menuBtn.SetActive(value);
    }

    private void SetMenuScreenUI(bool value)
    {
        playBtn.SetActive(value);
        menuBG.SetActive(value);
        gameTitle.SetActive(value);
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
                SetLoungeUI(false);
                break;

            case "report":
                SetBedroomUI(false);
                SetReportUI(true);
                SetKitchenUI(false);
                SetGymUI(false);
                SetLoungeUI(false);
                break;

            case "kitchen":
                SetBedroomUI(false);
                SetReportUI(false);
                SetKitchenUI(true);
                SetGymUI(false);
                SetLoungeUI(false);
                break;

            case "gym":
                SetBedroomUI(false);
                SetReportUI(false);
                SetKitchenUI(false);
                SetGymUI(true);
                SetLoungeUI(false);
                break;

            case "lounge":
                SetBedroomUI(false);
                SetReportUI(false);
                SetKitchenUI(false);
                SetGymUI(false);
                SetLoungeUI(true);
                break;

            default:
                SetBedroomUI(false);
                SetReportUI(false);
                SetKitchenUI(false);
                SetGymUI(false);
                SetLoungeUI(false);
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

            if (spiritLevelPressed) SpiritLevelPressed();
            if (isSettingsFlyoutActivated) ActivateSettingsFlyoutMenu();
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

        if (isUpgradeFlyoutActivated)
        {
            if (!hasUpgradeBeenCalled)
            {
                DialogueManager.PetConversation("UpgradeHouseFirstTime");
                hasUpgradeBeenCalled = true;
            }
            else
                DialogueManager.PetConversation("UpgradeHouse");

            CloseAllFlyouts();
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

        if (isSettingsFlyoutActivated) ActivateSettingsFlyoutMenu();
        if (isUpgradeFlyoutActivated) ActivateUpgradeFlyoutMenu();

        mainFlyoutPanel.SetActive(false);
        isMainFlyoutActivated = false;
        flyoutButtonPanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = activateFlyoutImage;
    }

    //public void DeactivateMainMenu()
    //{

    //}

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
        CloseAllFlyouts();
        flyoutButtonPanel.SetActive(!value);

        //if (value == true)
        //{
        //    CloseAllFlyouts();
        //    flyoutButtonPanel.SetActive(false);
        //}

        UIMorningReportObj.SetActive(value);
    }

    // Non overload to display report without achievments
    public void ViewDailyReport(string trophyText)
    {
        EnableUIElements(new[] { playerReportBackgroundPanel, playerDailyReportPanel });
        trophyDescriptionText.text = trophyText;
    }

    // Overload to allow display with achievements
    public void ViewDailyReport(string trophyText, string achievement, string achievementText)
    {
        EnableUIElements(new[] { playerReportBackgroundPanel, playerDailyReportPanel });
        trophyDescriptionText.text = trophyText;
        trophyAwardPanel.SetActive(true);
    }

    #endregion

    #region BedroomUI

    private void SetBedroomUI(bool value)
    {
        if (value == true)
        {
            if (isMinigameBeingPlayed)  miniGameBtnGO.SetActive(value);         //minigame played
            else if (isPetNapping)      wakeUpFromNapBtnGO.SetActive(value);    //napping
            else if (isPetSleeping)     wakeUpBtnGO.SetActive(value);            //sleeping
            else                        sendToBedBtnGO.SetActive(value);        //in bedroom
        }
        else
        {
            miniGameBtnGO.SetActive(value);
            sendToBedBtnGO.SetActive(value);
            wakeUpBtnGO.SetActive(value);
            wakeUpFromNapBtnGO.SetActive(value);
        }
        SetBedroomUIListeners(value);
    }

    private void SetBedroomUIListeners(bool value)
    {
        if (value == true)
        {
            sendToBedBtn.onClick.AddListener(CurrentBehaviour.StartConversation);
            wakeUpBtn.onClick.AddListener(CurrentBehaviour.StartConversationWakeUp);
            wakeUpFromNapBtn.onClick.AddListener(CurrentBehaviour.StartConversationWakeUpFromNap);
            miniGameBtn.onClick.AddListener(CurrentBehaviour.StartConversationMinigame);
        }
        else
        {
            sendToBedBtn.onClick.RemoveAllListeners();
            wakeUpBtn.onClick.RemoveAllListeners();
            wakeUpFromNapBtn.onClick.RemoveAllListeners();
            miniGameBtn.onClick.RemoveAllListeners();
        }
    }

    public void ActivateBedroomControl(bool value)
    {
        sendToBedBtnGO.SetActive(value);
    }

    public void MinigameClicked(bool value)
    {
        isMinigameBeingPlayed = value;
        
        miniGameBtnGO.SetActive(value);
    }

    public void SendToBedBtnClicked()
    {
        sendToBedBtnGO.SetActive(false);
        wakeUpFromNapBtnGO.SetActive(false);
        wakeUpBtnGO.SetActive(true);

        isPetSleeping = true;

        CloseAllFlyouts();
    }

    public void SendToBedForNapBtnClicked()
    {
        sendToBedBtnGO.SetActive(false);
        wakeUpFromNapBtnGO.SetActive(true);
        wakeUpBtnGO.SetActive(false);

        isPetNapping = true;

        CloseAllFlyouts();
    }

    public void WakeUpBtnClicked()
    {
        sendToBedBtnGO.SetActive(true);
        wakeUpBtnGO.SetActive(false);
        wakeUpFromNapBtnGO.SetActive(false);

        isPetSleeping = false;
        isPetNapping = false;
    }

    public void WakeUpNextDayBtnClicked()
    {
        sendToBedBtnGO.SetActive(false);
        wakeUpBtnGO.SetActive(false);

        isPetSleeping = false;
    }

    #endregion

    #region KitchenUI

    private void SetKitchenUI(bool value)
    {
        //Debug.Log("kitchen UI set to " + value);
        eatFoodBtnGO.SetActive(false);
        eatJunkFoodBtnGO.SetActive(value);
        //kitchenInteractBtnGO.SetActive(value);

        SetKitchenUIListeners(value);
    }

    private void SetKitchenUIListeners(bool value)
    {
        if (value == true)
        {
            eatJunkFoodBtn.onClick.AddListener(CurrentBehaviour.StartConversation);
            //eatFoodBtn.onClick.AddListener(CurrentBehaviour.EatFood);         DELETE
            //eatJunkFoodBtn.onClick.AddListener(CurrentBehaviour.EatJunkFood); DELETE
            //kitchenInteractBtn.onClick.AddListener(CurrentBehaviour.EatFood); DELETE
        }
        else
        {
            eatJunkFoodBtn.onClick.RemoveAllListeners();
            //eatFoodBtn.onClick.RemoveAllListeners();          DELETE
            //kitchenInteractBtn.onClick.RemoveAllListeners();  DELETE
        }
    }

    #endregion

    #region GymUI

    private void SetGymUI(bool value)
    {
        //Debug.Log("gym UI set to " + value);
        benchPressBtnGO.SetActive(value);
        gymInteractBtnGO.SetActive(false);

        SetGymUIListeners(value);
    }
    

    private void SetGymUIListeners(bool value)
    {
        if (value == true)
        {
            benchPressBtn.onClick.AddListener(CurrentBehaviour.StartConversation);
            gymInteractBtn.onClick.AddListener(CurrentBehaviour.StartConversationStopWorkour);
            //benchPressBtn.onClick.AddListener(CurrentBehaviour.BenchPress);

        }
        else
        {
            benchPressBtn.onClick.RemoveAllListeners();
            //gymInteractBtn.onClick.RemoveAllListeners();
        }
    }

    public void ActivateWorkingOutUI(bool value)
    {
        CloseAllFlyouts();
        flyoutButtonPanel.SetActive(!value);
        benchPressBtnGO.SetActive(!value);
        gymInteractBtnGO.SetActive(value);
    }

    #endregion

    #region Lounge UI

    private void SetLoungeUI(bool value)
    {
        loungeInteractBtnGO.SetActive(value);

        SetLoungeUIListeners(value);
    }

    private void SetLoungeUIListeners(bool value)
    {
        if (value == true)
        {
            loungeInteractBtn.onClick.AddListener(CurrentBehaviour.PlayMiniGame);
        }
        else
        {
            loungeInteractBtn.onClick.RemoveAllListeners();
        }
    }

    #endregion

    #region Dialogue UI

    // Non overload method to be used for displaying pet dialogue
    public void EnablePetDialogueText(string petDialogue)
    {
        petDialoguePanel.SetActive(true);
        petDialogueText.text = petDialogue;
    }

    // Overload method to be used for displaying pet tips
    public void EnablePetDialogueText(string petDialogue, int timeToDisplay)
    {
        petDialoguePanel.SetActive(true);
        petDialogueText.text = petDialogue;
        StartCoroutine(DisableUIElementsAfterSeconds(timeToDisplay, new[] { petDialoguePanel }));
    }

    // Player colour selection
    // Only used when changing the pets colour
    public void DisplayColourSelections()
    {
        //Debug.Log("Lets pick a colour");
        // Display pet colour swatch selection
        petColourPanel.SetActive(true);
        // Create a button for each pet colour listed in the UIManager
        foreach (string colour in petColours)
        {
            GameObject newButton = Instantiate(colourSwatchButtonPrefab, petColourPanel.transform);
            ColorUtility.TryParseHtmlString(colour, out Color col);
            newButton.transform.GetChild(0).GetComponentInChildren<Image>().color = col;
            newButton.GetComponent<Button>().onClick.AddListener(PetColourSelection);
        }
    }

    private void PetColourSelection()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;

        // This is not UI related
        playerMaterial.color = clickedButton.transform.GetChild(0).gameObject.GetComponent<Image>().color;

        isPetColourSet = true;

        // Reset the UI and move to the next conversation line
        DestroyUIButtons(petColourPanel);
        petColourPanel.SetActive(false);
        petDialoguePanel.SetActive(false);
        DialogueManager.AdvanceLine("_");
    }

    // Player text response selection
    // Accepts an array of strings to display text responses the player can choose
    public void DisplayPlayerResponses(string[] responses)
    {
        // If the passed in array is too long then exit
        if (responses.Length > 4)
            return;
        // Enable the player response panel
        playerResponsePanel.SetActive(true);
        // Display a button for each response the player can make

        foreach (string response in responses)
        {
            GameObject newButton = Instantiate(playerResponseButtonPrefab, playerResponsePanel.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = response;
            newButton.GetComponent<Button>().onClick.AddListener(StorePlayerResponse);
        }
    }

    // When the player clicks a response button the text response is stored in a public variable
    private void StorePlayerResponse()
    {
        string UIStoredPlayerResponse = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        DestroyUIButtons(playerResponsePanel);
        playerResponsePanel.SetActive(false);
        petDialoguePanel.SetActive(false);
        DialogueManager.AdvanceLine(UIStoredPlayerResponse);
    }

    // Used to reset a UI panels button options
    private void DestroyUIButtons(GameObject UIParent)
    {
        foreach (Transform child in UIParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Time input controller
    // Used to input times for bed time and wake up time
    public void DisplayTimeEntry()
    {
        //Debug.Log("Lets enter some time variables");
        // Enable the time entry panel
        timeEntryPanel.SetActive(true);
        PaginateHoursList(nextHoursPage);
    }

    // Pagination function to support scrolling through pages
    private void PaginateHoursList(int pageToFetch)
    {
        //Debug.Log("Fetching page" + pageToFetch);
        int pageSize = 6;
        // Set how many pages to skip based on current page
        int pageCount = pageToFetch * pageSize;
        IEnumerable<string> currentPage = inputHours.Skip(pageCount).Take(pageSize);
        // Get the time button panel and create the new page of buttons
        GameObject timeButtonPanel = timeEntryPanel.transform.GetChild(0).gameObject;
        DestroyUIButtons(timeButtonPanel);
        foreach (string hour in currentPage)
        {
            GameObject newButton = Instantiate(timeEntryButtonPrefab, timeButtonPanel.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = hour;
            newButton.GetComponent<Button>().onClick.AddListener(StoreTimeEntry);
        }
    }

    // Helper function to set current page for pagination
    private void SetNextHoursPage(bool direction)
    {
        if (direction)
            nextHoursPage++;
        else
            nextHoursPage--;
        //Debug.Log("Current page is " + nextHoursPage);
        if (nextHoursPage > 3) nextHoursPage = 0;
        if (nextHoursPage < 0) nextHoursPage = 3;
    }

    // Public button function to move forward a page
    public void NextPageHoursList()
    {
        PaginateHoursList(nextHoursPage);
        SetNextHoursPage(true);
    }

    // Public button function to move back a page
    public void PreviousPageHoursList()
    {
        PaginateHoursList(nextHoursPage);
        SetNextHoursPage(false);
    }

    //When the player clicks a time entry button the time is stored in a public variable
    public void StoreTimeEntry()
    {
        string UIStoredInputTime = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        timeEntryPanel.SetActive(false);
        petDialoguePanel.SetActive(false);
        DialogueManager.AdvanceLine(UIStoredInputTime);

        timeEntries.Add(UIStoredInputTime);
        //Debug.Log(UIStoredInputTime);
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

    // Helper function to enable multiple UI elements at the same time
    private void EnableUIElements(GameObject[] UIElements)
    {
        foreach (GameObject UIElement in UIElements)
        {
            UIElement.SetActive(true);
        }
    }
}