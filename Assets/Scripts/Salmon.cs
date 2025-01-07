using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salmon : Fish
{
    void Start()
    {
        int weight = Random.Range(10, 20);
        Initialize("salmon", Random.Range(10, 20), 20, 10);
    }
}
