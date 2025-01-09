using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Rendering;

public class ItemSlot : MonoBehaviour
{
    public string itemName;
    public int weight;
    public Sprite itemSprite;
    public bool isFull;

    [SerializeField]
    private TMP_Text weightText;
    private Image itemImage;


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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
