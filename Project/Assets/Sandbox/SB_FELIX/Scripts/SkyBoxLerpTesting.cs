using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxLerpTesting : MonoBehaviour
{
    public Material mat;
    public Material day;
    public Material skyBox;

    float t = 0.02f;
    float a;
    // Use this for initialization
    void Start()
    {
        RenderSettings.skybox.Lerp(skyBox, mat, 0.5f);  //Here assigning mat material as default .
    }

    // Update is called once per frame
    void Update()
    {
        a = (Time.time * 4) % 360;
        transform.eulerAngles = new Vector3(transform.rotation.x + a, transform.rotation.y, transform.rotation.z);

        if (a > 25 && a < 150)
        {
            //Changing default to day matiral skybox.
            RenderSettings.skybox.Lerp(skyBox, day, 0.5f * Time.deltaTime);
        }

        else if (a > 150 && a < 185)
        {
            //Changing day matiral to mat matiral
            RenderSettings.skybox.Lerp(skyBox, mat, 0.5f * Time.deltaTime);
        }
    }
}
