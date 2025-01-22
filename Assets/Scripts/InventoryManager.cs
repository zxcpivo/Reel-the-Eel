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
    public List<Fish> FishInventory = new List<Fish>();
    public GameObject Inventory;
    public GameObject InventoryMenu;
    public GameObject InventoryCanvas;
    private bool _menuActivated;
    public ItemSlot[] ItemSlot;

    private ItemSlot _currentlySelectedSlot;
    private CharacterController2D _controller;
    public GameManager GameManager;

    public Sprite CodSprite;
    public Sprite SalmonSprite;
    public Sprite ToonaSprite;
    public Sprite KoiSprite;
    public Sprite AnglerSprite;
    public Sprite EelSprite;

    private string _filePath;

    public InputField SearchInputField; // Reference to the search bar input field
    private List<Fish> _filteredFishInventory = new List<Fish>(); // Filtered fish list

    void Start()
    {

        _controller = FindObjectOfType<CharacterController2D>();
        _filePath = Path.Combine(Application.persistentDataPath, "fishInventory.json");
        LoadInventory();

        Inventory.SetActive(true);

        if (FishInventory.Count == 0)
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
        foreach (var slot in ItemSlot)
        {
            if (slot != null)
            {
                slot.ClearSlot();
            }
        }
    }


    void Update()
    {
        if (Input.GetButtonDown("Inventory") && _menuActivated)
        {
            Time.timeScale = 1;
            Inventory.SetActive(false);
            InventoryCanvas.SetActive(false);
            _controller.CloseInventory();
            _menuActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !_menuActivated)
        {
            Time.timeScale = 0;
            Inventory.SetActive(true);
            InventoryCanvas.SetActive(true);
            _controller.OpenInventory();
            _menuActivated = true;
        }
    }

    public int AddItem(string itemName, int weight, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < ItemSlot.Length; i++)
        {
            if (!ItemSlot[i].isFull)  // Find an empty slot
            {
                // Add the item to this slot
                ItemSlot[i].AddItem(itemName, weight, 1, itemSprite, itemDescription); // Only add one item at a time
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
        FishInventory.Add(fish);
        AddItem(fish.Name, fish.Weight, fish.Quantity, fishSprite, "Caught Sprite");
    }

    public void SelectSlot(ItemSlot slot)
    {
        if (_currentlySelectedSlot != null)
        {
            _currentlySelectedSlot.selectedShader.SetActive(false);
            _currentlySelectedSlot.thisItemSelected = false;
        }

        _currentlySelectedSlot = slot;
        _currentlySelectedSlot.selectedShader.SetActive(true);
        _currentlySelectedSlot.thisItemSelected = true;
    }

    public void SortByName()
    {
        InitializeInventory(); // clears inventory
        List<Fish>[] pigeonholes = new List<Fish>[26];
        for (int i = 0; i < 26; i++) // initializes all the buckets
        {
            pigeonholes[i] = new List<Fish>();
        }
        foreach (Fish fish in FishInventory)
        {
            char firstLetter = char.ToLower(fish.Name[0]); // takes first letter of every fish name and sets it to lower checking for which bucket it needs to go to
            int index = firstLetter - 'a'; // figures out which index the bucket is using ASCII value of 'a' from the ASCII value of the first letter
            pigeonholes[index].Add(fish); // adds fish to the bucket
        }

        FishInventory.Clear(); // clears the inventory to reorganize
        foreach (List<Fish> bucket in pigeonholes)
        {
            foreach (Fish fish in bucket) // redistributes all the fish inside each bucket back to int inventory
            {
                if (fish.Name == "Cod")
                    AddFishToInventory(fish, CodSprite);
                else if (fish.Name == "Salmon")
                    AddFishToInventory(fish, SalmonSprite);
                else if (fish.Name == "Toona")
                    AddFishToInventory(fish, ToonaSprite);
                else if (fish.Name == "Koi")
                    AddFishToInventory(fish, KoiSprite);
                else if (fish.Name == "Angler")
                    AddFishToInventory(fish, AnglerSprite);
                else if (fish.Name == "Eel")
                    AddFishToInventory(fish, EelSprite);
            }
        }
    }
    public void SortByValue()
    {
        // Find the min and max values of all the fish to determine pigeonhole range
        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        foreach (Fish fish in FishInventory)
        {
            if (fish.Value < minValue) minValue = fish.Value;
            if (fish.Value > maxValue) maxValue = fish.Value;
        }

        // Define pigeonholes based on the value range (we assume values are rounded to integers)
        int range = (int)(maxValue - minValue) + 1;  // Range is the difference between max and min value
        List<Fish>[] pigeonholes = new List<Fish>[range];

        // Initialize pigeonholes
        for (int i = 0; i < range; i++)
        {
            pigeonholes[i] = new List<Fish>();
        }

        // Distribute the fish into pigeonholes based on their value
        foreach (Fish fish in FishInventory)
        {
            int index = Mathf.FloorToInt(fish.Value - minValue); // Normalize value to pigeonhole index
            pigeonholes[index].Add(fish);
        }

        FishInventory.Clear(); // clears the inventory to reorganize

        // Add fish back to the inventory, in descending order of value (from maxValue to minValue)
        for (int i = range - 1; i >= 0; i--) // Loop from max value index to min value index
        {
            foreach (Fish fish in pigeonholes[i])
            {
                FishInventory.Add(fish); // Re-add the fish to the main inventory
            }
        }

        // Update the UI with the sorted fish
        UpdateInventoryDisplay(FishInventory);
    }

    public void SortByNameDescending()
    {
        InitializeInventory(); // clears inventory
        List<Fish>[] pigeonholes = new List<Fish>[26];
        for (int i = 0; i < 26; i++) // initializes all the buckets
        {
            pigeonholes[i] = new List<Fish>();
        }
        foreach (Fish fish in FishInventory)
        {
            char firstLetter = char.ToLower(fish.Name[0]); // takes first letter of every fish name and sets it to lower checking for which bucket it needs to go to
            int index = -(firstLetter - 'z'); // figures out which index the bucket is using ASCII value of 'a' from the ASCII value of the first letter
            pigeonholes[index].Add(fish); // adds fish to the bucket
        }

        FishInventory.Clear(); // clears the inventory to reorganize
        foreach (List<Fish> bucket in pigeonholes)
        {
            foreach (Fish fish in bucket) // redistributes all the fish inside each bucket back to int inventory
            {
                if (fish.Name == "Cod")
                    AddFishToInventory(fish, CodSprite);
                else if (fish.Name == "Salmon")
                    AddFishToInventory(fish, SalmonSprite);
                else if (fish.Name == "Toona")
                    AddFishToInventory(fish, ToonaSprite);
                else if (fish.Name == "Koi")
                    AddFishToInventory(fish, KoiSprite);
                else if (fish.Name == "Angler")
                    AddFishToInventory(fish, AnglerSprite);
                else if (fish.Name == "Eel")
                    AddFishToInventory(fish, EelSprite);
            }
        }
    }
    public void SortByWeightAndValue() //gleb
    {
        InitializeInventory(); 

        for (int i = 0; i < FishInventory.Count - 1; i++)
        {
            for (int j = 0; j < FishInventory.Count - i - 1; j++)
            {
                Fish fish1 = FishInventory[j];
                Fish fish2 = FishInventory[j + 1];

                if (fish1.Weight < fish2.Weight ||
                    (fish1.Weight == fish2.Weight && fish1.Value < fish2.Value)) 
                {
                    FishInventory[j] = fish2;
                    FishInventory[j + 1] = fish1;
                }
            }
        }

        foreach (var fish in FishInventory)
        {
            Sprite sprite = GetFishSprite(fish.Name);
            if (sprite != null)
            {
                AddItem(fish.Name, fish.Weight, fish.Quantity, sprite, "Caught Fish");
            }
        }
    }
    public void SearchByColor(string searchColor) //gleb
    {
        _filteredFishInventory.Clear();

        foreach (Fish fish in FishInventory)
        {
            if (fish.Color.ToLower() == searchColor.ToLower()) 
            {
                _filteredFishInventory.Add(fish); 
            }
        }

        if (string.IsNullOrWhiteSpace(searchColor)) // found IsNullOrWhiteSpace method being mentioned on stack overflow
        {
            SortByName(); 
        }   
        else
        {
            UpdateInventoryDisplay(_filteredFishInventory); 
        }
    }

    public void OnSearchChanged(string searchText)
    {
        _filteredFishInventory.Clear(); // clears the other list
        for (int i = 0; i < FishInventory.Count; i++)
        {
            if (FishInventory[i].Name.ToLower().Contains(searchText.ToLower())) // Converts the fish's name and the text inputed in serach bar to lowercase for case-insensitive comparison. Contain() checks if any letters are similar
            {
                _filteredFishInventory.Add(FishInventory[i]); // add to the new filtered list
            }
        }

        if (string.IsNullOrWhiteSpace(searchText)) // if they enter nothing
        {
            SortByName(); // display the full inventory
        }
        else
        {
            UpdateInventoryDisplay(_filteredFishInventory); // update the ui to display the inventory
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
            case "Cod": return CodSprite;
            case "Salmon": return SalmonSprite;
            case "Toona": return ToonaSprite;
            case "Koi": return KoiSprite;
            case "Angler": return AnglerSprite;
            case "Eel": return EelSprite;
            default: return null;
        }
    }

    public void PrintList()
    {
        foreach (Fish fish in FishInventory)
        {
            print($"{fish.Name}, {fish.Weight}");
        }
    }

    public void SaveInventory()
    {
        FishInventoryData data = new FishInventoryData { fishInventory = this.FishInventory };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_filePath, json);
    }

    public void LoadInventory()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            FishInventoryData data = JsonUtility.FromJson<FishInventoryData>(json);
            this.FishInventory = data.fishInventory;
        }
    }
}
