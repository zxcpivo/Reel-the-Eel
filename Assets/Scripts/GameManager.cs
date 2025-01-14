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
    public InventoryManager inventoryManager;
    public Sprite codSprite;
    public Sprite salmonSprite;
    public Sprite toonaSprite;

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
        int randomNum = 0;
        while (randomNum != 1)
        {
            print(randomNum);
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
        }
        else if(51 <= Luck && Luck <= 75)
        {
            currentFish = "salmon";
            fishingGame.SetActive(true);
        }
        else if(76 <= Luck && Luck <= 90)
        {
            currentFish = "toona";
            fishingGame.SetActive(true);
        }
    }

    public void FishingMinigameWon()
    {
        fishingGame.SetActive(false);
        isFishing = false;
        CatchFish(currentFish);
    }

    public void FishingMinigameLost()
    {
        fishingGame.SetActive(false);
        isFishing = false;
    }

    public void CatchFish(string name)
    {
        Fish newFish = null;
        Sprite fishSprite = null;
        if (name == "cod")
        {
            int weight = Random.Range(1, 10);
            newFish = new Fish($"Cod{Index}", weight, 1, 10, weight * 1f);
            fishSprite = codSprite;
        }
        else if (name == "salmon")
        {
            int weight = Random.Range(10, 20);
            newFish = new Fish($"Salmon{Index}", weight, 1, 20, weight * 1.5f);
            fishSprite = salmonSprite;
        }
        else if (name == "toona")
        {
            int weight = Random.Range(10, 20);
            newFish = new Fish($"Toona{Index}", weight, 1, 20, weight * 2f);
            fishSprite = toonaSprite;
        }
        print($"Added a {newFish.Name} that weighs {newFish.Weight}");
        fishInventory.Add(newFish);
        Index += 1;
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager is not assigned in the GameManager!");
            return;
        }
        inventoryManager.AddFishToInventory(newFish, fishSprite);
    }


}
