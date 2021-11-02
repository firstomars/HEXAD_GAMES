using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sandbox.Omar.Greybox
{
    public class GameManager : MonoBehaviour
    {
        #region Singelton

        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    go.AddComponent<GameManager>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }
        #endregion

        //scene management variables
        private int sceneIndex;
        private int nextSceneIndex;
        private int maxScenes;

        [Header("Player")]
        [SerializeField] private GameObject playerPrefab;
        private GameObject player;

        [Header("House")]
        [SerializeField] private GameObject housePrefab;
        public GameObject house;

        [Header("UI")]
        [SerializeField] private GameObject UIManagerObj;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);

            //set scenes
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            nextSceneIndex = sceneIndex + 1;
            maxScenes = SceneManager.sceneCountInBuildSettings;
            Debug.Log("Scene Loaded: " + SceneManager.GetActiveScene().name);
        }

        // Update is called once per frame
        void Update()
        {
            //if (nextSceneIndex < maxScenes && Input.GetKeyDown(KeyCode.Space))
            //    GoToNextScene();
        }

        public GameObject AssignPlayer()
        {
            //load player stats

            //create and return player
            player = Instantiate(playerPrefab);
            return player;
        }

        public GameObject AssignHouse()
        {
            //create and return house
            house = Instantiate(housePrefab);
            return house;
        }

        #region SceneManagement Functions

        private void GoToNextScene()
        {
            SceneManager.LoadScene(nextSceneIndex);
            sceneIndex = nextSceneIndex;
            nextSceneIndex = sceneIndex + 1;
        }

        public void MenuScene()
        {
            SceneManager.LoadScene(1);
        }

        public void PlayScene()
        {
            SceneManager.LoadScene(2);
        }

        public void StagingScene()
        {
            SceneManager.LoadScene(3);
        }

        public void TimeScene()
        {
            SceneManager.LoadScene(4);
        }

        public void PlayerStatsScene()
        {
            SceneManager.LoadScene(5);
        }

        public void PlayerPrefsScene()
        {
            SceneManager.LoadScene(6);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        #endregion
    }
}