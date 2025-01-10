using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    // Add this variable to track the selected slot
    private ItemSlot currentlySelectedSlot;
    private CharacterController2D Controller;
    void Start()
    {
        Controller = FindObjectOfType<CharacterController2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            Controller.CloseInventory();
            menuActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            Controller.OpenInventory();
            menuActivated = true;
        }
    }

    public void AddItem(string itemName, int weight, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, weight, itemSprite, itemDescription);
                return;
            }
        }
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
