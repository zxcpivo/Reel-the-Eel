using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] fishInventory;
    public Button click;
    public int Index;

    public void Reel()
    {
        int Luck = Random.Range(1, 100);
        if(Luck <= 50)
        {
            int clicksNeeded = 10;
            // Spawn button
            // if caught then call catch fish and send it the name
        }
        else if(51 <= Luck && Luck <= 75)
        {
            int clicksNeeded = 20;
            // Spawn Button
            // if caught then call catch fish and send it the name
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
