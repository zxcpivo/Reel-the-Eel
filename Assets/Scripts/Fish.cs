using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public string Name;
    public int Weight;
    public int Clicks;
    public float Value;

    public void Initialize(string name, int weight, int clicks, float value)
    {
        this.Name = name;
        this.Weight = weight;
        this.Clicks = clicks;
        this.Value = value;
    }
}
