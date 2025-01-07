using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private bool isInventoryScene = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleScene();
        }
    }

    void ToggleScene()
    {
        if (!isInventoryScene)
        {
            SceneManager.LoadScene("InventoryScene");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }

        isInventoryScene = !isInventoryScene;
    }
}
