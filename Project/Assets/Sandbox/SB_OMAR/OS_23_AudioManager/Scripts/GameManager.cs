using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.AudioManager
{
    public class GameManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            AudioManager.AudioManagerInstance.PlaySound("BackgroundMusic");
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}