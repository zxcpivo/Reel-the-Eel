using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FishInventoryData
{
    public List<Fish> fishInventory = new List<Fish>();
}

public class InventoryManager : MonoBehaviour
{
    public List<Fish> fishInventory = new List<Fish>();
    public GameObject Inventory;
    public GameObject InventoryMenu;
    public GameObject InventoryCanvas;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    private ItemSlot currentlySelectedSlot;
    private CharacterController2D Controller;
    public GameManager gameManager;

    public Sprite codSprite;
    public Sprite salmonSprite;
    public Sprite toonaSprite;
    public Sprite koiSprite;
    public Sprite anglerSprite;
    public Sprite eelSprite;

    private string filePath;

    public InputField searchInputField; // Reference to the search bar input field
    private List<Fish> filteredFishInventory = new List<Fish>(); // Filtered fish list

    void Start()
    {

        Controller = FindObjectOfType<CharacterController2D>();
        filePath = Path.Combine(Application.persistentDataPath, "fishInventory.json");
        LoadInventory();

        Inventory.SetActive(true);

        if (fishInventory.Count == 0)
        {
            SaveInventory();
        }

        StartCoroutine(DelayedSort());
    }

    private IEnumerator DelayedSort()
    {
        yield return new WaitForSeconds(0.1f);
        SortByName();
        Inventory.SetActive(false);
    }


    public void InitializeInventory()
    {
        foreach (var slot in itemSlot)
        {
            if (slot != null)
            {
                slot.ClearSlot();
            }
        }
    }


    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            Inventory.SetActive(false);
            InventoryCanvas.SetActive(false);
            Controller.CloseInventory();
            menuActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            Inventory.SetActive(true);
            InventoryCanvas.SetActive(true);
            Controller.OpenInventory();
            menuActivated = true;
        }
    }

    public int AddItem(string itemName, int weight, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)  // Find an empty slot
            {
                // Add the item to this slot
                itemSlot[i].AddItem(itemName, weight, 1, itemSprite, itemDescription); // Only add one item at a time
                return 0; // No leftover items
            }
        }

        // If there is no empty slot, return the quantity as is (unable to add more items)
        return quantity;
    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }

    public void AddFishToInventory(Fish fish, Sprite fishSprite)
    {
        fishInventory.Add(fish);
        AddItem(fish.Name, fish.Weight, fish.Quantity, fishSprite, "Caught Sprite");
    }

    public void SelectSlot(ItemSlot slot)
    {
        if (currentlySelectedSlot != null)
        {
            currentlySelectedSlot.selectedShader.SetActive(false);
            currentlySelectedSlot.thisItemSelected = false;
        }

        currentlySelectedSlot = slot;
        currentlySelectedSlot.selectedShader.SetActive(true);
        currentlySelectedSlot.thisItemSelected = true;
    }

    public void SortByName()
    {
        InitializeInventory(); // clears inventory
        List<Fish>[] pigeonholes = new List<Fish>[26];
        for (int i = 0; i < 26; i++) // initializes all the buckets
        {
            pigeonholes[i] = new List<Fish>();
        }
        foreach (Fish fish in fishInventory)
        {
            char firstLetter = char.ToLower(fish.Name[0]); // takes first letter of every fish name and sets it to lower checking for which bucket it needs to go to
            int index = firstLetter - 'a'; // figures out which index the bucket is using ASCII value of 'a' from the ASCII value of the first letter
            pigeonholes[index].Add(fish); // adds fish to the bucket
        }

        fishInventory.Clear(); // clears the inventory to reorganize
        foreach (List<Fish> bucket in pigeonholes)
        {
            foreach (Fish fish in bucket) // redistributes all the fish inside each bucket back to int inventory
            {
                if (fish.Name == "Cod")
                    AddFishToInventory(fish, codSprite);
                else if (fish.Name == "Salmon")
                    AddFishToInventory(fish, salmonSprite);
                else if (fish.Name == "Toona")
                    AddFishToInventory(fish, toonaSprite);
                else if (fish.Name == "Koi")
                    AddFishToInventory(fish, koiSprite);
                else if (fish.Name == "Angler")
                    AddFishToInventory(fish, anglerSprite);
                else if (fish.Name == "Eel")
                    AddFishToInventory(fish, eelSprite);
            }
        }
    }
    public void SortByValue()
    {
        InitializeInventory();

        
        fishInventory.Sort((fish1, fish2) => fish2.Value.CompareTo(fish1.Value));

        foreach (var fish in fishInventory)
        {
            Sprite sprite = GetFishSprite(fish.Name);
            if (sprite != null)
            {
                AddItem(fish.Name, fish.Weight, fish.Quantity, sprite, "Caught Fish");
            }
        }
    }
    public void SortByNameDescending()
    {
        InitializeInventory();

        // Sort by name in descending order
        fishInventory.Sort((fish1, fish2) => fish2.Name.CompareTo(fish1.Name));

        foreach (var fish in fishInventory)
        {
            Sprite sprite = GetFishSprite(fish.Name);
            if (sprite != null)
            {
                AddItem(fish.Name, fish.Weight, fish.Quantity, sprite, "Caught Fish");
            }
        }
    }

    public void SortByWeightAndValue()
    {
        InitializeInventory(); 

        for (int i = 0; i < fishInventory.Count - 1; i++)
        {
            for (int j = 0; j < fishInventory.Count - i - 1; j++)
            {
                Fish fish1 = fishInventory[j];
                Fish fish2 = fishInventory[j + 1];

                if (fish1.Weight < fish2.Weight ||
                    (fish1.Weight == fish2.Weight && fish1.Value < fish2.Value)) 
                {
                    fishInventory[j] = fish2;
                    fishInventory[j + 1] = fish1;
                }
            }
        }

        foreach (var fish in fishInventory)
        {
            Sprite sprite = GetFishSprite(fish.Name);
            if (sprite != null)
            {
                AddItem(fish.Name, fish.Weight, fish.Quantity, sprite, "Caught Fish");
            }
        }
    }

    public void OnSearchChanged(string searchText)
    {
        filteredFishInventory.Clear(); // clears the other list
        for (int i = 0; i < fishInventory.Count; i++)
        {
            if (fishInventory[i].Name.ToLower().Contains(searchText.ToLower())) // Converts the fish's name and the text inputed in serach bar to lowercase for case-insensitive comparison. Contain() checks if any letters are similar
            {
                filteredFishInventory.Add(fishInventory[i]); // add to the new filtered list
            }
        }

        if (string.IsNullOrWhiteSpace(searchText)) // if they enter nothing
        {
            SortByName(); // display the full inventory
        }
        else
        {
            UpdateInventoryDisplay(filteredFishInventory); // update the ui to display the inventory
        }
    }

    private void UpdateInventoryDisplay(List<Fish> fishList) // simply updates the display of the fish
    {
        InitializeInventory(); // initialize list

        foreach (var fish in fishList)
        {
            Sprite sprite = GetFishSprite(fish.Name);
            if (sprite != null)
            {
                AddItem(fish.Name, fish.Weight, fish.Quantity, sprite, "Caught Fish");
            }
        }
    }

    private Sprite GetFishSprite(string fishName)
    {
        switch (fishName)
        {
            case "Cod": return codSprite;
            case "Salmon": return salmonSprite;
            case "Toona": return toonaSprite;
            case "Koi": return koiSprite;
            case "Angler": return anglerSprite;
            case "Eel": return eelSprite;
            default: return null;
        }
    }

    public void PrintList()
    {
        foreach (Fish fish in fishInventory)
        {
            print($"{fish.Name}, {fish.Weight}");
        }
    }

    public void SaveInventory()
    {
        FishInventoryData data = new FishInventoryData { fishInventory = this.fishInventory };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    public void LoadInventory()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            FishInventoryData data = JsonUtility.FromJson<FishInventoryData>(json);
            this.fishInventory = data.fishInventory;
        }
    }
}
