using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.AudioManagerInstance.PlaySound("BackgroundMusic");
    }

    // Update is called once per frame
    void Update()
    {

        //play test sound example
        if (Input.GetKeyDown(KeyCode.Space)) 
            AudioManager.AudioManagerInstance.PlaySound("Test");

        //stop test sound example
        if (Input.GetKeyDown(KeyCode.Return))
            AudioManager.AudioManagerInstance.StopSound("Test");
    }
}
