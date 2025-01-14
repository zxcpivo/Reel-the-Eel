using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameManager gameScript;
    public FishingBarScript fishGameScript;
    public GameObject shopObject;
    private bool shopActivated;
    private string EquippedRodKey = "EquippedRod";

    void Start()
    {
        LoadEquippedRod();
    }

    void OnMouseDown()
    {
        Time.timeScale = 0;
        shopActivated = true;
        shopObject.SetActive(shopActivated);
    }

    public void ExitShop()
    {
        Time.timeScale = 1;
        shopActivated = false;
        shopObject.SetActive(shopActivated);
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
