using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevCommandsManager : MonoBehaviour
{
    public GameManager gameManager;

    void Update()
    {
        HandleDevKeyInputs();
    }

    private void HandleDevKeyInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            SpawnSpecificFish("cod");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            SpawnSpecificFish("salmon");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            SpawnSpecificFish("toona");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) 
        {
            SpawnSpecificFish("koi");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) 
        {
            SpawnSpecificFish("angler");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6)) 
        {
            SpawnSpecificFish("eel");
        }
        else if (Input.GetKeyDown(KeyCode.R)) 
        {
            SpawnRandomFish();
        }
    }

    private void SpawnSpecificFish(string fishName)
    {
        Debug.Log($"Spawning specific fish: {fishName}");
        gameManager.CatchFish(fishName);
    }

    private void SpawnRandomFish()
    {
        Debug.Log("Spawning random fish...");
        int Luck = Random.Range(1, 1001);
        string randomFish;

        if (Luck <= 500)
            randomFish = "cod";
        else if (Luck <= 750)
            randomFish = "salmon";
        else if (Luck <= 900)
            randomFish = "toona";
        else if (Luck <= 950)
            randomFish = "koi";
        else if (Luck <= 999)
            randomFish = "angler";
        else
            randomFish = "eel";

        gameManager.CatchFish(randomFish);
    }
}
