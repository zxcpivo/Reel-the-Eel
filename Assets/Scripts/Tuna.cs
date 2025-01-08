using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuna : Fish
{
    void Start()
    {
        int weight = Random.Range(1, 10);
        Initialize("tuna", weight, 10, 25);
    }
}
