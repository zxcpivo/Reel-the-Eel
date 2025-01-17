using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance; // Singleton instance

    private bool _soundOn;
    private bool _musicOn;


    void Awake()
    {
        if (Instance == null) // Makes sure that when screens change they all use the same instance
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
      
        }
        else
        {
            Destroy(gameObject);
        }
        LoadPreferences(); // load preferences when game starts
    }

    public void TurnSoundOn()
    {
        _soundOn = true;
        SavePreferences(); // save prefs
    }

    public void TurnSoundOff()
    {
        _soundOn = false;
        SavePreferences(); // save prefs
    }

    public bool GetSound()
    {
        return _soundOn;
    }

    public void TurnMusicOn()
    {
        _musicOn = true;
        SavePreferences(); // save prefs
    }

    public void TurnMusicOff()
    {
        _musicOn = false;
        SavePreferences(); // save prefs
    }

    public bool GetMusic()
    {
        return _musicOn;
    }

    private void SavePreferences()
    {
        PlayerPrefs.SetInt("SoundOn", _soundOn ? 1 : 0); // checks is _soundOn is 1 or 0
        PlayerPrefs.SetInt("MusicOn", _musicOn ? 1 : 0); // 1 is true 0 is false
        PlayerPrefs.Save();
    }

    private void LoadPreferences()
    {
        _soundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1; // if their true it sets to true
        _musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1; // if they dont equal 1(true) then go to default value which is false
    }
}

