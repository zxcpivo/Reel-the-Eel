using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cod : Fish
{
    void Start()
    {
        int weight = Random.Range(1, 10);
        Initialize("cod", weight, 10, 5);
    }
}
