using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toona : Fish
{
    public Toona(string name, int weight, int clicks, float value) : base(name, weight, clicks, value)
    {
        this.Value = value * weight;
    }

}
