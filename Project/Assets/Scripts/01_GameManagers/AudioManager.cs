using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    private static AudioManager _instance;

    public static AudioManager AudioManagerInstance { get { return _instance; } }

    #endregion

    #region Sound class
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;

        [Range(0f, 1f)]
        public float pitch;

        public bool loop = false;

        [HideInInspector] public AudioSource source;
    }
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
        PlaySound("BackgroundMusic");
    }

    public bool IsAudioPlaying(string name)
    {
        Sound s = FindSound(name);

        if (s.source.isPlaying) return true;
        return false;
    }

    public void PlaySound(string name)
    {
        Sound s = FindSound(name);

        //play sound
        s.source.Play();
    }

    public void StopSound(string name)
    {
        Sound s = FindSound(name);

        //Stop sound
        s.source.Stop();
    }

    public void PauseSound(string name)
    {
        Sound s = FindSound(name);

        //pause sound
        s.source.Pause();
    }
    private Sound FindSound(string name)
    {
        //find sound in sounds array
        Sound s = Array.Find(sounds, sound => sound.name == name);

        //return error message if sound not found
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found");
            return null;
        }

        return s;
    }
}