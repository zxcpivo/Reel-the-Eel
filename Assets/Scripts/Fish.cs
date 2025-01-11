using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{
    public string Name;
    public int Weight;
    public int Quantity;
    public int Clicks;
    public float Value;

    public Fish(string name, int weight, int quantity, int clicks, float value)
    {
        this.Name = name;
        this.Weight = weight;
        this.Quantity = quantity;
        this.Clicks = clicks;
        this.Value = value;
    }
}

