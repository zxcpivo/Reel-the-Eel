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

    void Update()
    {
        
    }

    public void Cast()
    {
        print("casting");
        print("hi arya");
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
        }
        else if(51 <= Luck && Luck <= 75)
        {
            int clicksNeeded = 20;
            Vector3 ButtonPos = new Vector3(0, -450, 0);
            Button Clicker = Instantiate(click, ButtonPos, Quaternion.identity);

            Clicker.transform.SetParent(canvas.transform, false);
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
