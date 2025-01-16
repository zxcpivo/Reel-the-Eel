using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button minigameButton;
    public Canvas canvas;

    private int rodLuck = 5; // set private so that the inspector doesn't change it

    private string currentFish = "";

    public GameObject fishingGame;
    public GameObject Bobber;
    public GameObject Exclamation;

    public bool isFishing = false;
    public InventoryManager inventoryManager;
    public Sprite codSprite;
    public Sprite salmonSprite;
    public Sprite toonaSprite;

    void Start()
    {
        fishingGame.SetActive(false);
        Bobber.SetActive(false);
        Exclamation.SetActive(false);
    }
    void Update()
    {
        
    }

    public void ChangeRodLuck(int luck)
    {
        rodLuck = luck;
    }

    public void StartCasting(Vector3 mousePosition)
    {
        StartCoroutine(CastCoroutine(mousePosition));
    }

    public IEnumerator CastCoroutine(Vector3 mousePosition)
    {
        Bobber.SetActive(true);
        Bobber.transform.position = mousePosition;
        int randomNum = 0;
        while (randomNum != 1)
        {
            randomNum = Random.Range(1, rodLuck);
            yield return new WaitForSeconds(0.1f);
        }
        Bobber.SetActive(false);
        Reel();
    }

    public void Reel()
    {
        Exclamation.SetActive(true);
        int Luck = Random.Range(1, 90);
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
        Exclamation.SetActive(false);
        fishingGame.SetActive(false);
        isFishing = false;
    }

    public void CatchFish(string name)
    {
        Exclamation.SetActive(false);
        Fish newFish = null;
        Sprite fishSprite = null;
        if (name == "cod")
        {
            int weight = Random.Range(1, 10);
            newFish = new Fish($"Cod", weight, 1, 10, weight * 1f);
            fishSprite = codSprite;
        }
        else if (name == "salmon")
        {
            int weight = Random.Range(10, 20);
            newFish = new Fish($"Salmon", weight, 1, 20, weight * 1.5f);
            fishSprite = salmonSprite;
        }
        else if (name == "toona")
        {
            int weight = Random.Range(10, 20);
            newFish = new Fish($"Toona", weight, 1, 20, weight * 2f);
            fishSprite = toonaSprite;
        }
        inventoryManager.AddFishToInventory(newFish, fishSprite);
    }


}
