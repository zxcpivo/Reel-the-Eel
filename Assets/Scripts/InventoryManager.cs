using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject InventoryMenu;
    public GameObject InventoryCanvas;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    // Add this variable to track the selected slot
    private ItemSlot currentlySelectedSlot;
    private CharacterController2D Controller;
    public GameManager gameManager;
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
}
