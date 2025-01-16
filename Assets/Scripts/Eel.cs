using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eel : Fish
{
    
    public Eel(string name, int weight, int quantity, int clicks, float value) : base(name, weight, quantity, clicks, value)
    {
        this.Value = value * weight;
    }

}