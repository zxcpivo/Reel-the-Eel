using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] fishInventory;
    public Button click;
    public Canvas canvas;
    public int Index;

    public int rodLuck = 50;

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
            print(randomNum);
            randomNum = Random.Range(1, rodLuck);
            yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
        }
        print(randomNum);

        Reel();
    }

    public void Reel()
    {
        int Luck = Random.Range(1, 75);
        if(Luck <= 50)
        {
            int clicksNeeded = 10;
            Vector3 ButtonPos = new Vector3(0, -450, 0);
            Button Clicker = Instantiate(click, ButtonPos, Quaternion.identity);

            Clicker.transform.SetParent(canvas.transform, false);
            // if caught then call catch fish and send it the name
            print("cod");
        }
        else if(51 <= Luck && Luck <= 75)
        {
            int clicksNeeded = 20;
            Vector3 ButtonPos = new Vector3(0, -450, 0);
            Button Clicker = Instantiate(click, ButtonPos, Quaternion.identity);

            Clicker.transform.SetParent(canvas.transform, false);
            // if caught then call catch fish and send it the name
            print("Salmon");
        }
    }

    public void CatchFish(string name)
    {
        if (name == "cod")
        {
            GameObject FishReeling = new GameObject("Cod" + (Index + 1));
            Cod codComponent = FishReeling.AddComponent<Cod>();
            codComponent.Initialize("Cod", Random.Range(1, 10), 10, 5);
        }

        else if(name == "salmon")
        {
            GameObject FishReeling = new GameObject("Salmon" + (Index + 1));
            Salmon salmonComponent = FishReeling.AddComponent<Salmon>();
            salmonComponent.Initialize("Salmon", Random.Range(10, 20), 20, 10);
        }

    }
}
