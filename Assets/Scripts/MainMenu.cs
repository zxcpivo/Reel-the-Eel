using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
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
}
