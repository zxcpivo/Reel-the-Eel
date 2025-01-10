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
    private int currentClicks = 0;
    private int clicksNeeded = 0;

    private string currentFish = "";
    private Button currentButton;

    public bool isFishing = false;

    void Update()
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
            clicksNeeded = 10;

            SpawnButtonGame();

            currentFish = "cod";
            print("reeling a cod");
        }
        else if(51 <= Luck && Luck <= 75)
        {
            clicksNeeded = 20;

            SpawnButtonGame();

            currentFish = "salmon";
            print("reeling a salmon");
        }
        else if(76 <= Luck && Luck <= 90)
        {
            clicksNeeded = 30;

            SpawnButtonGame();

            currentFish = "toona";
            print("reeling a toona");
        }
    }

    public void SpawnButtonGame()
    {
        Vector3 ButtonPos = new Vector3(0, -450, 0);
        Button catchFishButton = Instantiate(minigameButton, ButtonPos, Quaternion.identity);
        currentButton = catchFishButton;

        currentButton.transform.SetParent(canvas.transform, false);

        currentButton.onClick.AddListener(ClickCounter);
    }

    public void ClickCounter()
    {
        currentClicks += 1;

        print(currentClicks);
        if(currentClicks == clicksNeeded)
        {
            currentClicks = 0;
            Destroy(currentButton.gameObject);
            isFishing = false;
            CatchFish(currentFish);
        }
    }

    public void CatchFish(string name)
    {
        Fish newFish = null;
        if (name == "cod")
        {
            newFish = new Fish($"Cod{Index}", Random.Range(1, 10), 10, 5);

        }
        else if (name == "salmon")
        {
            newFish = new Fish($"Salmon{Index}", Random.Range(10, 20), 20, 10);
        }
        else if (name == "toona")
        {
            newFish = new Fish($"Toona{Index}", Random.Range(10, 20), 20, 10);
        }

        fishInventory.Add(newFish);
        Index += 1;
        PrintFish();
    }

    public void PrintFish()
    {
        foreach(Fish fish in fishInventory)
        {
            print($"{fish.Name}, {fish.Weight}, {fish.Value}, {fish.Clicks}");
        }
    }


}
