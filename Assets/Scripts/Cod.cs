using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cod : Fish
{
    
    public Cod(string name, int weight, int clicks, float value) : base(name, weight, clicks, value)
    {
        this.Value = value * weight;
    }

}
