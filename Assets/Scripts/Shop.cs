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

    private bool _shopActivated;
    private float _coins;
    private string _EquippedRodKey = "EquippedRod"; // name of the rod equiped

    public bool isShopping = false; // public since unity inspector needs access to it

    private bool _beginnerRod;
    private bool _amateurRod;
    private bool _glebRod;

    void Start()
    {
        LoadEquippedRod(); // load everything
        LoadCoins();
        LoadRodPurchaseStatuses();

        shopCanvas.SetActive(false); // close the shop on start
        sellCanvas.SetActive(false);
        shopObject.SetActive(false);
        AryaCollider.enabled = true; // enable the collider to get ready to detect clicks
    }

    void OnMouseDown()
    {
        isShopping = true;
        Time.timeScale = 0; // pauses game background
        _shopActivated = true;
        shopObject.SetActive(true); // turns the shop on
        shopCanvas.SetActive(true);
        sellCanvas.SetActive(false);
        AryaCollider.enabled = false; // disables so you cant open the shop twice
    }

    public void ExitShop()
    {
        isShopping = false;
        Time.timeScale = 1; // resumes game
        _shopActivated = false;
        shopObject.SetActive(false); // turns shop off
        shopCanvas.SetActive(false);
        sellCanvas.SetActive(false);
        AryaCollider.enabled = true; // enables the collider again
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
        if (_beginnerRod) // if they already purchased it
            EquipRod("BeginnerRod", 50, 1f);
        else if (_coins > 100)
        {
            _coins -= 100;
            SaveCoins();
            EquipRod("BeginnerRod", 50, 1f);
            _beginnerRod = true;
            SaveRodPurchaseStatus("BeginnerRod", true);
        }
    }
    public void PurchaseAmateurRod()
    {
        if (_amateurRod) // if they already purchased it
            EquipRod("AmateurRod", 25, 1.5f);
        else if (_coins > 1000)
        {
            _coins -= 1000;
            SaveCoins();
            EquipRod("AmateurRod", 25, 1.5f);
            _amateurRod = true;
            SaveRodPurchaseStatus("AmateurRod", true);
        }
    }
    public void PurchaseRodOfGleb()
    {
        if(_glebRod) // if they already purchased it
            EquipRod("RodOfGleb", 10, 2f);
        else if (_coins > 9999)
        {
            _coins -= 9999;
            SaveCoins();
            EquipRod("RodOfGleb", 10, 2f);
            _glebRod = true;
            SaveRodPurchaseStatus("RodOfGleb", true);
        }
    }

    public void Sell(string name)
    {
        for (int i = inventoryScript.fishInventory.Count - 1; i >= 0; i--)
        {
            if (inventoryScript.fishInventory[i].Name == name) // goes through the fish inventory and every time it spots a fish with the same name as the one you want to sell it sells it
            {
                _coins += inventoryScript.fishInventory[i].Value;
                Coins.text = $"{_coins}"; // update coins for both sell and buy screen
                CoinsShop.text = $"{_coins}";
                inventoryScript.fishInventory.RemoveAt(i);
            }
        }
        inventoryScript.SortByName(); // sort the inventory
        SaveCoins();
    }

    private void EquipRod(string rodName, int luck, float strength)
    {
        gameScript.ChangeRodLuck(luck); // changes the rods luck to reel fish faster
        fishGameScript.ChangeRodStrength(strength); // changed the rod to hold onto the fish better so they dont move around during the minigame
        SaveEquippedRod(rodName);
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetFloat("Coins", _coins); 
        PlayerPrefs.Save();
    }

    private void LoadCoins()
    {
        _coins = PlayerPrefs.GetFloat("Coins", 0f); // default to zero if not found
        Coins.text = $"{_coins}";
        CoinsShop.text = $"{_coins}";
    }

    private void SaveEquippedRod(string rodName)
    {
        PlayerPrefs.SetString(_EquippedRodKey, rodName);
        PlayerPrefs.Save();
    }

    private void LoadEquippedRod()
    {
        string rodName = PlayerPrefs.GetString(_EquippedRodKey, "BeginnerRod"); // Default to BeginnerRod
        switch (rodName) // tests every case
        {
            case "BeginnerRod":
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
        _beginnerRod = PlayerPrefs.GetInt("BeginnerRod", 0) == 1;
        _amateurRod = PlayerPrefs.GetInt("AmateurRod", 0) == 1;
        _glebRod = PlayerPrefs.GetInt("RodOfGleb", 0) == 1;
    }

}
