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
            AudioManager.AudioManagerInstance.PlaySound("FootStep");

        //stop test sound example
        if (Input.GetKeyDown(KeyCode.Return))
            AudioManager.AudioManagerInstance.StopSound("FootStep");



        //play Snoring sound example
        if (Input.GetKeyDown(KeyCode.Mouse0))
            AudioManager.AudioManagerInstance.PlaySound("Sleeping");

        //stop Snoring sound example
        if (Input.GetKeyDown(KeyCode.Return))
            AudioManager.AudioManagerInstance.StopSound("Sleeping");


        //play Gym sound example
        if (Input.GetKeyDown(KeyCode.Mouse1))
            AudioManager.AudioManagerInstance.PlaySound("Gym");

        //stop Gym sound example
        if (Input.GetKeyDown(KeyCode.Return))
            AudioManager.AudioManagerInstance.StopSound("Gym");

    }

}
