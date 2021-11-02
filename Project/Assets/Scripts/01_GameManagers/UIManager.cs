using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Splash Scene UI")]
    [SerializeField] private GameObject menuBtn;

    [Header("Menu Scene UI")]
    [SerializeField] private GameObject playBtn;

    [Header("Staging Scene UI")]
    [SerializeField] private GameObject stagingBtn;

    [Header("Play Scene UI")]
    [SerializeField] private GameObject playUI;

    private void Start()
    {
        SetSplashScreenUI(true); // rework
        SetMenuScreenUI(false);
        SetPlayScreenUI(false);
    }

    public void SetSplashScreenUI(bool value)
    {
        menuBtn.SetActive(value);
    }

    public void SetMenuScreenUI(bool value)
    {
        playBtn.SetActive(value);
        stagingBtn.SetActive(value);
    }

    public void SetPlayScreenUI(bool value)
    {
        playUI.SetActive(value);
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
        SetPlayScreenUI(true);
    }

    public void GoToStagingScene()
    {
        GameManager.Instance.StagingScene();
        SetSplashScreenUI(false);
        SetMenuScreenUI(false);
    }
}
