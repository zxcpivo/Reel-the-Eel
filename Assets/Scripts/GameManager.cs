using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> fishInventory = new List<GameObject>();
    public Button minigameButton;
    public Canvas canvas;

    public int Index = 0;

    private int rodLuck = 10; // set private so that the inspector doesn't change it
    private int currentClicks = 0;
    private int clicksNeeded = 0;

    private string currentFish = "";
    private Button currentButton;

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
        int Luck = Random.Range(1, 75);
        if(Luck <= 50)
        {
            clicksNeeded = 10;

            SpawnButtonGame();

            currentFish = "cod";
            print("cod");
        }
        else if(51 <= Luck && Luck <= 75)
        {
            clicksNeeded = 20;

            SpawnButtonGame();

            currentFish = "salmon";
            print("salmon");
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
            CatchFish(currentFish);
        }
    }

    public void CatchFish(string name)
    {
        if (name == "cod")
        {
            print("hello cod");
            currentFish = "";
            GameObject FishReeling = new GameObject("Cod" + (Index + 1));
            Cod codComponent = FishReeling.AddComponent<Cod>();
            codComponent.Initialize("Cod", Random.Range(1, 10), 10, 5);

            fishInventory.Add(FishReeling);
        }

        else if(name == "salmon")
        {
            print("hello salmon");
            currentFish = "";
            GameObject FishReeling = new GameObject("Salmon" + (Index + 1));
            Salmon salmonComponent = FishReeling.AddComponent<Salmon>();
            salmonComponent.Initialize("Salmon", Random.Range(10, 20), 20, 10);

            fishInventory.Add(FishReeling);
        }
        Index += 1;


    }

    
}
