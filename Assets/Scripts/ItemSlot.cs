using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int weight;          // The weight of the item
    public int quantity;        // The quantity of the item
    public Sprite itemSprite;   // The sprite for the item
    public bool isFull;         // Whether the slot is full
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField]
    private int maxNumberOfItems; // Maximum items this slot can hold

    // References to UI Elements
    [SerializeField]
    private Image itemImage;       // Reference to the ItemImage UI
    [SerializeField]
    private TMP_Text weightText;   // Reference to the Weight UI (TextMeshPro)
    [SerializeField]
    private TMP_Text quantityText; // Reference to the Quantity UI (TextMeshPro)

    public GameObject selectedShader; // Visual highlight for selected slot
    public bool thisItemSelected;     // Tracks if this slot is selected

    private InventoryManager inventoryManager;
    //===Item Description ==//
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;
    

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();

        // Ensure the shader starts as disabled
        if (selectedShader != null)
        {
            selectedShader.SetActive(false);
        }

        // Ensure UI starts as cleared
        ClearSlot();
    }

    public int AddItem(string itemName, int weight, int quantity, Sprite itemSprite, string itemDescription)
    {
        if (isFull)
            return quantity;

        this.itemName = itemName;
        this.weight = weight;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        this.quantity += quantity;

        // Handle overflow of items beyond max capacity
        if (this.quantity > maxNumberOfItems)
        {
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            isFull = true;
            UpdateUI();
            return extraItems;
        }

        // Mark as full if capacity is reached
        if (this.quantity == maxNumberOfItems)
        {
            isFull = true;
        }

        UpdateUI();
        return 0;
    }

    public void ClearSlot()
    {
        itemName = string.Empty;
        weight = 0;
        quantity = 0;
        itemSprite = null;
        itemDescription = string.Empty;
        isFull = false;

        // Reset UI elements
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

    public void UpdateUI()
    {
        // Update the item image
        if (itemImage != null)
        {
            itemImage.sprite = itemSprite;
            itemImage.enabled = itemSprite != null;
        }

        // Update the weight text
        if (weightText != null)
        {
            weightText.text = weight > 0 ? weight.ToString() : string.Empty;
            weightText.enabled = weight > 0;
        }

        // Update the quantity text
        if (quantityText != null)
        {
            quantityText.text = quantity > 0 ? quantity.ToString() : string.Empty;
            quantityText.enabled = quantity > 0;
        }
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
