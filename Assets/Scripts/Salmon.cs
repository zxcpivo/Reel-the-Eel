using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salmon : Fish
{
    public Salmon(string name, int weight, int quantity, int clicks, float value, string color) : base(name, weight, quantity, clicks, value, color)
    {
        this.Value = value * weight;
        this.Color = color;
    }

}
