using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salmon : Fish
{
    public Salmon(string name, int weight, int clicks, float value) : base(name, weight, clicks, value)
    {
        //this.Value = value * weight;
    }
}
