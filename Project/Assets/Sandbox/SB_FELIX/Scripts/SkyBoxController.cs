using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxController : MonoBehaviour
{
    public Material MorningSky;
    public Material DaySky;
    public Material SunsetSky;
    public Material NightSky;

    public float Sec;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = MorningSky;
    }

    // Update is called once per frame
    void Update()
    {
        float lerp = Time.time / Sec;
        RenderSettings.skybox.Lerp(MorningSky, DaySky, lerp);
    }
}

