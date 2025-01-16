using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButtons : MonoBehaviour
{

    public Button musicOnButton;
    public Button musicOffButton;
    public Button soundOnButton;
    public Button soundOffButton;

    private void Start() // scene gets loaded
    {
        // rehook buttons
        musicOnButton.onClick.AddListener(SettingsManager.Instance.TurnMusicOn);
        musicOffButton.onClick.AddListener(SettingsManager.Instance.TurnMusicOff);

        soundOnButton.onClick.AddListener(SettingsManager.Instance.TurnSoundOn);
        soundOffButton.onClick.AddListener(SettingsManager.Instance.TurnSoundOff);
    }
}
