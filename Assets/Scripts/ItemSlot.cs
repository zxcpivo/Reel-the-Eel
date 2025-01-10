using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Rendering;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int weight;
    public Sprite itemSprite;
    public bool isFull;

    [SerializeField]
    private TMP_Text weightText;
    private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

public void AddItem(string itemName, int weight, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.weight = weight;
        this.itemSprite = itemSprite;
        isFull = true;

        weightText.text = weight.ToString();
        weightText.enabled = true;
        itemImage.sprite = itemSprite;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button== PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button== PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.SelectSlot(this);
    }

    public void OnRightClick()
    {
        
    }
}