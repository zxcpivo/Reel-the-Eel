using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance; // Singleton instance

    private bool soundOn;
    private bool musicOn;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
      
        }
        else
        {
            Destroy(gameObject);
        }
        LoadPreferences();
    }

    public void TurnSoundOn()
    {
        soundOn = true;
        SavePreferences();
    }

    public void TurnSoundOff()
    {
        soundOn = false;
        SavePreferences();
    }

    public bool GetSound()
    {
        return soundOn;
    }

    public void TurnMusicOn()
    {
        musicOn = true;
        SavePreferences();
    }

    public void TurnMusicOff()
    {
        musicOn = false;
        SavePreferences();
    }

    public bool GetMusic()
    {
        return musicOn;
    }

    private void SavePreferences()
    {
        PlayerPrefs.SetInt("SoundOn", soundOn ? 1 : 0);
        PlayerPrefs.SetInt("MusicOn", musicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadPreferences()
    {
        soundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
    }
}

