using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.SkyboxTesting
{
    public class SkyboxController : MonoBehaviour
    {
        [SerializeField] Material[] skyboxes;

        private int currentSkybox;

        // Start is called before the first frame update
        void Start()
        {
            currentSkybox = 0;
            ChangeSkybox();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ChangeSkybox();

            if (Input.GetKeyDown(KeyCode.Return))
                Debug.Log(skyboxes.Length);
        }

        private void ChangeSkybox()
        {
            if (currentSkybox == skyboxes.Length - 1)    currentSkybox = 0;
            else                                    currentSkybox += 1;

            Debug.Log(currentSkybox);

            RenderSettings.skybox = skyboxes[currentSkybox];
        }
    }
}

