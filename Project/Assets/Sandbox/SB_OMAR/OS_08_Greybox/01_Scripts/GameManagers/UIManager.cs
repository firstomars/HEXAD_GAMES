using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sandbox.Omar.Greybox
{
    public class UIManager : MonoBehaviour
    {
        [Header("Buttons")]
        //[SerializeField] private GameObject menuBtn;
        [SerializeField] private GameObject playBtn;
        //[SerializeField] private GameObject stagingBtn;
        //[SerializeField] private GameObject timeBtn;
        //[SerializeField] private GameObject playerStatsBtn;
        //[SerializeField] private GameObject playerPrefsBtn;
        //[SerializeField] private GameObject quitBtn;

        //[Header("Play Scene UI")]
        //[SerializeField] private GameObject playUI;

        private void Start()
        {
            playBtn.SetActive(true);
            /*
            SetSplashScreenUI(true); // rework
            SetMenuScreenUI(false);
            SetPlayScreenUI(false);
            SetAssetStagingUI(false);
            SetTimeUI(false);
            SetPlayerStatsUI(false);
            SetPlayerPrefsUI(false);
            */
        }

        public void GoToPlayScene()
        {
            GameManager.Instance.PlayScene();
        }

        /*
        
        private void SetSplashScreenUI(bool value)
        {
            menuBtn.SetActive(value);
        }

        private void SetMenuScreenUI(bool value)
        {
            playBtn.SetActive(value);
        }

        private void SetPlayScreenUI(bool value)
        {
            playUI.SetActive(value);
            stagingBtn.SetActive(value);
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
            SetAssetStagingUI(true);
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
        */
    }
}