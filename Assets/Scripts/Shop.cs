using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameManager gameScript;
    public InventoryManager inventoryScript;
    public FishingBarScript fishGameScript;

    public GameObject shopObject;
    public GameObject shopCanvas;
    public GameObject sellCanvas;
    public Text Coins;
    public Text CoinsShop;

    public BoxCollider AryaCollider;

    private bool shopActivated;
    private float coins;
    private string EquippedRodKey = "EquippedRod";
    public bool isShopping = false;
    private bool beginnerRod;
    private bool amateurRod;
    private bool glebRod;

    void Start()
    {
        LoadEquippedRod();
        LoadCoins();
        LoadRodPurchaseStatuses();
        shopCanvas.SetActive(false);
        sellCanvas.SetActive(false);
        shopObject.SetActive(false);
        AryaCollider.enabled = true;
    }
    void Update()
    {
        //print($"beginner: {beginnerRod}");
        //print($"Amateur: {amateurRod}");
        //print($"gleb: {glebRod}");
    }

    void OnMouseDown()
    {
        isShopping = true;
        Time.timeScale = 0;
        shopActivated = true;
        shopObject.SetActive(shopActivated);
        shopCanvas.SetActive(true);
        sellCanvas.SetActive(false);
        AryaCollider.enabled = false;
    }

    public void ExitShop()
    {
        isShopping = false;
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
        if (beginnerRod)
            EquipRod("BeginnerRod", 50, 1f);
        else if (coins > 100)
        {
            coins -= 100;
            SaveCoins();
            EquipRod("BeginnerRod", 50, 1f);
            beginnerRod = true;
            SaveRodPurchaseStatus("BeginnerRod", true);
        }
    }
    public void PurchaseAmateurRod()
    {
        if (amateurRod)
            EquipRod("AmateurRod", 25, 1.5f);
        else if (coins > 1000)
        {
            coins -= 1000;
            SaveCoins();
            EquipRod("AmateurRod", 25, 1.5f);
            amateurRod = true;
            SaveRodPurchaseStatus("AmateurRod", true);
        }
    }
    public void PurchaseRodOfGleb()
    {
        if(glebRod)
            EquipRod("RodOfGleb", 10, 2f);
        else if (coins > 9999)
        {
            coins -= 9999;
            SaveCoins();
            EquipRod("RodOfGleb", 10, 2f);
            glebRod = true;
            SaveRodPurchaseStatus("RodOfGleb", true);
        }
    }

    public void Sell(string name)
    {
        for (int i = inventoryScript.fishInventory.Count - 1; i >= 0; i--)
        {
            if (inventoryScript.fishInventory[i].Name == name)
            {
                coins += inventoryScript.fishInventory[i].Value;
                Coins.text = $"{coins}";
                CoinsShop.text = $"{coins}";
                inventoryScript.fishInventory.RemoveAt(i);
            }
        }
        inventoryScript.SortByName();
        SaveCoins();
    }

    private void EquipRod(string rodName, int luck, float strength)
    {
        gameScript.ChangeRodLuck(luck);
        fishGameScript.ChangeRodStrength(strength);
        SaveEquippedRod(rodName);
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetFloat("Coins", coins); 
        PlayerPrefs.Save();
    }

    private void LoadCoins()
    {
        coins = PlayerPrefs.GetFloat("Coins", 0f); // default to zero if not found
        Coins.text = $"{coins}";
        CoinsShop.text = $"{coins}";
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
    private void SaveRodPurchaseStatus(string rodName, bool isPurchased)
    {
        int purchaseStatus = isPurchased ? 1 : 0;
        PlayerPrefs.SetInt(rodName, purchaseStatus);
        PlayerPrefs.Save();
    }
    private void LoadRodPurchaseStatuses()
    {
        beginnerRod = PlayerPrefs.GetInt("BeginnerRod", 0) == 1;
        amateurRod = PlayerPrefs.GetInt("AmateurRod", 0) == 1;
        glebRod = PlayerPrefs.GetInt("RodOfGleb", 0) == 1;
    }

}
