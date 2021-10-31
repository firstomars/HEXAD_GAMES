using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sandbox.Omar.GameManager
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
        private float splashScreenLoad;

        //TO DO
        //player prefab
        //player stats
        //house
        //time 

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);

            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            nextSceneIndex = sceneIndex + 1;
            maxScenes = SceneManager.sceneCountInBuildSettings;
            Debug.Log(maxScenes);
        }

        // Update is called once per frame
        void Update()
        {
            if (nextSceneIndex < maxScenes && Input.GetKeyDown(KeyCode.Space))
                GoToNextScene();
        }

        private void GoToNextScene()
        {
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("Scene Loaded: " + SceneManager.GetActiveScene().name);
            sceneIndex = nextSceneIndex;
            nextSceneIndex = sceneIndex + 1;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
