using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // used so we can save the fish to JSON file
public class Fish
{
    public string Name;
    public int Weight;
    public int Quantity;
    public int Clicks;
    public float Value;
    public string Color;

    public Fish(string name, int weight, int quantity, int clicks, float value, string color)
    {
        this.Name = name;
        this.Weight = weight;
        this.Quantity = quantity;
        this.Clicks = clicks;
        this.Value = value;
        this.Color = color;
    }
}

