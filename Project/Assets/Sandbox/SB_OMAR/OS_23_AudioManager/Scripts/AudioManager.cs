using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Sandbox.Omar.AudioManager
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton

        private static AudioManager _instance;

        public static AudioManager AudioManagerInstance { get { return _instance; } }

        #endregion

        public Sound[] sounds;

        private void Awake()
        {
            //singleton
            if (_instance != null) Destroy(gameObject);
            else _instance = this;

            //connect audio source controls with serialized sound variables
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        private void Start()
        {
            //singleton
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySound(string name)
        {
            //find sound in sounds array
            Sound s = Array.Find(sounds, sound => sound.name == name);

            //return error message if sound not found
            if (s == null)
            {
                Debug.Log("Sound " + name + " not found");
                return;
            }

            //stop sound
            s.source.Play();
        }

        public void StopSound(string name)
        {
            //find sound in sounds array
            Sound s = Array.Find(sounds, sound => sound.name == name);

            //return error message if sound not found
            if (s == null)
            {
                Debug.Log("Sound " + name + " not found");
                return;
            }

            //pause sound
            s.source.Stop();
        }

        public void PauseSound(string name)
        {
            //find sound in sounds array
            Sound s = Array.Find(sounds, sound => sound.name == name);

            //return error message if sound not found
            if (s == null)
            {
                Debug.Log("Sound " + name + " not found");
                return;
            }

            //play sound
            s.source.Pause();
        }
    }
}
