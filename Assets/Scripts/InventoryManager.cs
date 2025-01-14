using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Fish> fishInventory = new List<Fish>();
    public GameObject Inventory;
    public GameObject InventoryMenu;
    public GameObject InventoryCanvas;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    // Add this variable to track the selected slot
    private ItemSlot currentlySelectedSlot;
    private CharacterController2D Controller;
    public GameManager gameManager;

    public Sprite codSprite;
    public Sprite salmonSprite;
    public Sprite toonaSprite;
    void Start()
    {
        Controller = FindObjectOfType<CharacterController2D>();
        InitializeInventory();
    }
    public void InitializeInventory()
    {
        // Initialize all item slots
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
            if (!itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, weight, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName, weight, leftOverItems, itemSprite, itemDescription);

                return leftOverItems;
            }
        }
        return quantity;
    }
    public void AddFishToInventory(Fish fish, Sprite fishSprite)
    {
        fishInventory.Add(fish);
        AddItem(fish.Name, fish.Weight, fish.Quantity, fishSprite, "Caught Sprite");
    }



    public void SelectSlot(ItemSlot slot)
    {
        // Deselect the previously selected slot
        if (currentlySelectedSlot != null)
        {
            currentlySelectedSlot.selectedShader.SetActive(false);
            currentlySelectedSlot.thisItemSelected = false;
        }

        // Select the new slot
        currentlySelectedSlot = slot;
        currentlySelectedSlot.selectedShader.SetActive(true);
        currentlySelectedSlot.thisItemSelected = true;
    }

    public void SortByName()
    {
        InitializeInventory();
        List<Fish>[] pigeonholes = new List<Fish>[26]; // 26 buckets one for each letter of the alphabet
        for (int i = 0; i < 26; i++)
        {
            pigeonholes[i] = new List<Fish>();
        }

        foreach (Fish fish in fishInventory)
        {
            char firstLetter = char.ToLower(fish.Name[0]); // Normalize to lowercase
            int index = firstLetter - 'a'; // Calculate the index (0 for 'a', 1 for 'b', etc.)
            pigeonholes[index].Add(fish);

        }

        fishInventory.Clear();
        foreach (List<Fish> bucket in pigeonholes)
        {
            foreach (Fish fish in bucket)
            {
                string fishType = CutOffNumber(fish.Name);

                if (fishType == "Cod")
                    AddFishToInventory(fish, codSprite);
                else if (fishType == "Salmon")
                    AddFishToInventory(fish, salmonSprite);
                else if (fishType == "Toona")
                    AddFishToInventory(fish, toonaSprite);
            }
        }
    }

public string CutOffNumber(string fishName)
    {
        int index = fishName.Length - 1;
        while (index >= 0 && char.IsDigit(fishName[index]))
        {
            index -= 1;
        }

        return fishName.Substring(0, index + 1);
    }

    public void PrintList()
    {
        foreach(Fish fish in fishInventory)
        {
            print($"{fish.Name}, {fish.Weight}");
        }
    }
}
