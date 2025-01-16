using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameManager gameScript;
    public InventoryManager inventoryScript;
    public FishingBarScript fishGameScript;

    public GameObject shopObject;
    public GameObject shopCanvas;
    public GameObject sellCanvas;

    public BoxCollider AryaCollider;

    private bool shopActivated;
    private float Money;
    private string EquippedRodKey = "EquippedRod";

    void Start()
    {
        LoadEquippedRod();
        shopObject.SetActive(false);
        AryaCollider.enabled = true;
    }

    void OnMouseDown()
    {
        Time.timeScale = 0;
        shopActivated = true;
        shopObject.SetActive(shopActivated);
        shopCanvas.SetActive(true);
        sellCanvas.SetActive(false);
        AryaCollider.enabled = false;
    }

    public void ExitShop()
    {
        Time.timeScale = 1;
        shopActivated = false;
        shopObject.SetActive(shopActivated);
        shopCanvas.SetActive(false);
        sellCanvas.SetActive(false);
        AryaCollider.enabled = true;
    }

    public void EnterSell()
    {
        shopCanvas.SetActive(false);
        sellCanvas.SetActive(true);
    }

    public void ExitSell()
    {
        sellCanvas.SetActive(false);
        shopCanvas.SetActive(true);

    }

    public void PurchaseBeginnerRod()
    {
        EquipRod("BeginnerRod", 50, 1f);
    }
    public void PurchaseAmateurRod()
    {
        EquipRod("AmateurRod", 25, 1.5f);
    }
    public void PurchaseRodOfGleb()
    {
        EquipRod("RodOfGleb", 10, 2f);
    }

    public void SellCod()
    {
        for (int i = inventoryScript.fishInventory.Count - 1; i >= 0; i--)
        {
            if (inventoryScript.fishInventory[i].Name == "Cod")
            {
                Money += inventoryScript.fishInventory[i].Value;
                inventoryScript.fishInventory.RemoveAt(i);
            }
        }
        inventoryScript.SortByName();
    }

    public void SellSalmon()
    {
        for (int i = inventoryScript.fishInventory.Count - 1; i >= 0; i--)
        {
            if (inventoryScript.fishInventory[i].Name == "Salmon")
            {
                Money += inventoryScript.fishInventory[i].Value;
                inventoryScript.fishInventory.RemoveAt(i);
            }
        }
        inventoryScript.SortByName();
    }

    public void SellToona()
    {
        for (int i = inventoryScript.fishInventory.Count - 1; i >= 0; i--)
        {
            if (inventoryScript.fishInventory[i].Name == "Toona")
            {
                Money += inventoryScript.fishInventory[i].Value;
                inventoryScript.fishInventory.RemoveAt(i);
            }
        }
        inventoryScript.SortByName();
    }

    public void SellKoi()
    {
        for (int i = inventoryScript.fishInventory.Count - 1; i >= 0; i--)
        {
            if (inventoryScript.fishInventory[i].Name == "Koi")
            {
                Money += inventoryScript.fishInventory[i].Value;
                inventoryScript.fishInventory.RemoveAt(i);
            }
        }
        inventoryScript.SortByName();
    }
    public void SellAngler()
    {
        for (int i = inventoryScript.fishInventory.Count - 1; i >= 0; i--)
        {
            if (inventoryScript.fishInventory[i].Name == "Angler")
            {
                Money += inventoryScript.fishInventory[i].Value;
                inventoryScript.fishInventory.RemoveAt(i);
            }
        }
        inventoryScript.SortByName();
    }

    public void SellEel()
    {
        for (int i = inventoryScript.fishInventory.Count - 1; i >= 0; i--)
        {
            if (inventoryScript.fishInventory[i].Name == "Eel")
            {
                Money += inventoryScript.fishInventory[i].Value;
                inventoryScript.fishInventory.RemoveAt(i);
            }
        }
        inventoryScript.SortByName();
    }

    private void EquipRod(string rodName, int luck, float strength)
    {
        gameScript.ChangeRodLuck(luck);
        fishGameScript.ChangeRodStrength(strength);
        SaveEquippedRod(rodName);
    }

    private void SaveEquippedRod(string rodName)
    {
        PlayerPrefs.SetString(EquippedRodKey, rodName);
        PlayerPrefs.Save();
    }

    private void LoadEquippedRod()
    {
        string rodName = PlayerPrefs.GetString(EquippedRodKey, "BeginnerRod"); // Default to BeginnerRod
        switch (rodName) // tests every case
        {
            case "BeginnerRod": // not necessary as if other 2 cases fail its already default to beginner rod but looks nicer in code
                EquipRod("BeginnerRod", 50, 1f);
                break;
            case "AmateurRod":
                EquipRod("AmateurRod", 25, 1.5f);
                break;
            case "RodOfGleb":
                EquipRod("RodOfGleb", 10, 2f);
                break;
        }
    }

}
