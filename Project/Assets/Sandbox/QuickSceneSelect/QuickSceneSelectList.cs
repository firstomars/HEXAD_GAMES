using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Sandbox.QuickSceneSelect
{   

    public class QuickSceneSelectList : MonoBehaviour
    {
        List<GameObject> listItems = new List<GameObject>();

        public GameObject sceneEventSystem;
        public GameObject sceneSelectMenuContainer;
        public GameObject sceneSelectTitle;

        public GameObject sceneSelectList;
        public GameObject sceneSelectListItemPrefab;

        public GameObject sceneBackBtn;

        public Text txtVersion;
        public Toggle toggleEnableBackNav;

        string selectedScene = "";

        // Start is called before the first frame update
        void Start()
        {
            sceneBackBtn.SetActive(false);
            selectedScene = "";

            txtVersion.text = "<b>Version:</b>" + Application.version;
            
            for(int sceneIndex = 0; sceneIndex < SceneManager.sceneCountInBuildSettings; sceneIndex ++)
            {
                var scene = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
                var pathParts = scene.Split('/');
                string sceneName = pathParts.LastOrDefault().Replace(".unity", "");
                string scenePath = string.Join("/", pathParts.Skip(1).Take(pathParts.Count() - 2));

                // Hack:
                // dont show this scene
                if (sceneName == "QuickSceneSelect")
                    continue;

                GameObject item = Instantiate(sceneSelectListItemPrefab, sceneSelectList.transform);
                listItems.Add(item);

                Text txt = item.GetComponentInChildren<Text>();
                txt.text = $"<size=14><b>{sceneName}</b></size>\n<size=12>{scenePath}</size>";

                Button btn = item.GetComponentInChildren<Button>();
                btn.onClick.AddListener(() =>
                {
                    LoadScene(scene);
                });
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowSelectScreen()
        {
            if (string.IsNullOrWhiteSpace(selectedScene))
                return;

            var unloadSceneTask = SceneManager.UnloadSceneAsync(selectedScene);
            unloadSceneTask.completed += (AsyncOperation obj) =>
            {
                selectedScene = "";

                sceneSelectMenuContainer.SetActive(true);
                sceneEventSystem.SetActive(true);
                toggleEnableBackNav.gameObject.SetActive(true);

                sceneBackBtn.SetActive(false);
            };
        }

        public void LoadScene(string scenePath)
        {
            if (string.IsNullOrWhiteSpace(selectedScene) == false)
                return;

            sceneSelectMenuContainer.SetActive(false);
            sceneEventSystem.SetActive(false);
            toggleEnableBackNav.gameObject.SetActive(false);
            sceneBackBtn.SetActive(true);

            LoadSceneMode mode = toggleEnableBackNav.isOn ? LoadSceneMode.Additive : LoadSceneMode.Single;
            SceneManager.LoadScene(scenePath, mode);
            selectedScene = scenePath;
        }
    }
}
