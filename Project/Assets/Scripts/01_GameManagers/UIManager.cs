using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject backgrdPnl;
    [SerializeField] private GameObject responsePnl;
    [SerializeField] private GameObject colourPnl;
    [SerializeField] private GameObject entryPnl;
    [SerializeField] private GameObject petSpeechPnl;
    [SerializeField] private GameObject spiritLevelSldr;

    [Header("Buttons")]
    [SerializeField] private GameObject menuBtn;
    [SerializeField] private GameObject playBtn;
    [SerializeField] private GameObject stagingBtn;
    [SerializeField] private GameObject timeBtn;
    [SerializeField] private GameObject playerStatsBtn;
    [SerializeField] private GameObject playerPrefsBtn;
    [SerializeField] private GameObject quitBtn;


    private void Start()
    {
        SetSplashScreenUI(true); // rework
        SetMenuScreenUI(false);
        SetAssetStagingUI(false);
        SetTimeUI(false);
        SetPlayerStatsUI(false);
        SetPlayerPrefsUI(false);
        SetPlayUI(false);
        SetResponsePnlUI(false);
        SetColourPnlUI(false);
        SetEntryPnlUI(false);
        SetPetPnlUI(false);
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
        backgrdPnl.SetActive(value);
        stagingBtn.SetActive(value);
        spiritLevelSldr.SetActive(value);
    }

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

    private void UpdateSpiritLevelUI(bool value)
    {
        
    }

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

    // Methods called from converse class

    public void DisplayPetSpeechBubble(string textToDisplay)
    {
        SetPetPnlUI(true);
    }
}
