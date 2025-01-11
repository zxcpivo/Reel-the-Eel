using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void SettingButton()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void NewSaveButton()
    {
        print("Not working yet");
    }

    public void BackButton()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void ControlButton()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void ControlBackButton()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
