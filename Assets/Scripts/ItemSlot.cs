using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int weight;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField]
    private TMP_Text weightText; // For displaying the item's weight
    private TMP_Text quantityText; // For displaying the item's quantity
    private Image itemImage; // For showing the item's sprite in the slot

    public GameObject selectedShader; // Visual highlight for selected slot
    public bool thisItemSelected; // Tracks if this slot is selected

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        itemImage = GetComponent<Image>(); // Automatically find the image component

        // Ensure the selected shader is disabled initially
        if (selectedShader != null)
        {
            selectedShader.SetActive(false);
        }
    }

    public void AddItem(string itemName, int weight, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.weight = weight;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        // Update UI elements for this slot
        if (weightText != null)
        {
            weightText.text = weight.ToString();
            weightText.enabled = true;
        }

        if (quantityText != null)
        {
            quantityText.text = quantity.ToString();
            quantityText.enabled = true;
        }

        if (itemImage != null)
        {
            itemImage.sprite = itemSprite;
            itemImage.enabled = true;
        }
    }

    public void ClearSlot()
    {
        itemName = string.Empty;
        weight = 0;
        quantity = 0;
        itemSprite = null;
        itemDescription = string.Empty;
        isFull = false;

        // Reset UI elements for this slot
        if (weightText != null)
        {
            weightText.text = string.Empty;
            weightText.enabled = false;
        }

        if (quantityText != null)
        {
            quantityText.text = string.Empty;
            quantityText.enabled = false;
        }

        if (itemImage != null)
        {
            itemImage.sprite = emptySprite;
            itemImage.enabled = false;
        }

        // Disable the selected shader
        if (selectedShader != null)
        {
            selectedShader.SetActive(false);
        }

        thisItemSelected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log($"Right-clicked on slot with item: {itemName}");
        }
    }

    public void OnLeftClick()
    {
        // Notify the InventoryManager of this slot selection
        inventoryManager.SelectSlot(this);

        // Enable the selected shader
        if (selectedShader != null)
        {
            selectedShader.SetActive(true);
        }

        thisItemSelected = true;
    }
}
