using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            if (s.name == "Theme")
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume = PlayerPrefs.GetFloat("AudioVolume");

                s.source.loop = s.loop;
            }
            else
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume = PlayerPrefs.GetFloat("SoundVolume");

                s.source.loop = s.loop;
            }
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void OffOnMusic(Button audioButton)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == "Theme")
            {
                if (s.volume <= 0)
                {
                    PlayerPrefs.SetFloat("AudioVolume", .3f);
                    audioButton.GetComponent<Image>().color = Color.green;
                    s.source.volume = s.volume = PlayerPrefs.GetFloat("AudioVolume");
                }
                else
                {
                    PlayerPrefs.SetFloat("AudioVolume", 0);
                    audioButton.GetComponent<Image>().color = Color.red;
                    s.source.volume = s.volume = PlayerPrefs.GetFloat("AudioVolume");
                }
            }
        }
    }

    public void OffOnSound(Button soundButton)
    {
        foreach (Sound s in sounds)
        {
            if (s.name != "Theme")
            {
                if (s.volume <= 0)
                {
                    PlayerPrefs.SetFloat("SoundVolume", .3f);
                    soundButton.GetComponent<Image>().color = Color.green;
                    s.source.volume = s.volume = PlayerPrefs.GetFloat("SoundVolume");
                }
                else
                {
                    PlayerPrefs.SetFloat("SoundVolume", 0);
                    soundButton.GetComponent<Image>().color = Color.red;
                    s.source.volume = s.volume = PlayerPrefs.GetFloat("SoundVolume");
                }
            }
        }
    }

    public void PaintingButtons(Button audioButton, Button soundButton)
    {
        if (PlayerPrefs.GetFloat("AudioVolume") == 0)
        {
            audioButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            audioButton.GetComponent<Image>().color = Color.green;
        }

        if (PlayerPrefs.GetFloat("SoundVolume") == 0)
        {
            soundButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            soundButton.GetComponent<Image>().color = Color.green;
        }
    }
}
