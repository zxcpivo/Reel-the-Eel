using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Fish> fishInventory = new List<Fish>();
    public Button minigameButton;
    public Canvas canvas;

    public int Index = 0;

    private int rodLuck = 5; // set private so that the inspector doesn't change it

    private string currentFish = "";

    public GameObject fishingGame;

    public bool isFishing = false;

    void Start()
    {
        fishingGame.SetActive(false);
    }
    void Update()
    {
        
    }

    public void StopCasting()
    {

    }

    public void StartCasting()
    {
        StartCoroutine(CastCoroutine());
    }

    public IEnumerator CastCoroutine()
    {
        print("casting");
        int randomNum = 0;
        while (randomNum != 1)
        {
            randomNum = Random.Range(1, rodLuck);
            yield return new WaitForSeconds(0.1f);
        }

        Reel();
    }

    public void Reel()
    {
        int Luck = Random.Range(1, 100);
        if(Luck <= 50)
        {
            currentFish = "cod";
            fishingGame.SetActive(true);

            print("reeling a cod");
        }
        else if(51 <= Luck && Luck <= 75)
        {
            currentFish = "salmon";
            fishingGame.SetActive(true);

            print("reeling a salmon");
        }
        else if(76 <= Luck && Luck <= 90)
        {
            currentFish = "toona";
            fishingGame.SetActive(true);

            print("reeling a toona");
        }
    }

    public void FishingMinigameWon()
    {
        print("Won minigame");
        fishingGame.SetActive(false);
        isFishing = false;
        CatchFish(currentFish);
    }

    public void FishingMinigameLost()
    {
        print("Lost minigame");
        fishingGame.SetActive(false);
        isFishing = false;
    }

    public void CatchFish(string name)
    {
        Fish newFish = null;
        if (name == "cod")
        {
            int weight = Random.Range(1, 10);
            newFish = new Fish($"Cod{Index}", weight, 10, weight * 1f);
        }
        else if (name == "salmon")
        {
            int weight = Random.Range(10, 20);
            newFish = new Fish($"Salmon{Index}", weight, 20, weight * 1.5f);
        }
        else if (name == "toona")
        {
            int weight = Random.Range(10, 20);
            newFish = new Fish($"Toona{Index}", weight, 20, weight * 2f);
        }
        print($"Added a {newFish.Name} that weighs {newFish.Weight}");
        fishInventory.Add(newFish);
        Index += 1;
    }


}
