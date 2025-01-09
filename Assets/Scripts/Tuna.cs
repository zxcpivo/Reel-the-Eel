using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toona : Fish
{
    void Start()
    {
        int weight = Random.Range(1, 10);
        Initialize("toona", weight, 30, 25);
    }
}
