using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koi : Fish
{
    
    public Koi(string name, int weight, int quantity, int clicks, float value) : base(name, weight, quantity, clicks, value)
    {
        this.Value = value * weight;
    }

}