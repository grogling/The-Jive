using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            maximum = max;
            minimum = min;
        }
    }

    public int columns = 30;
    public int rows = 30;
    public GameObject[] Walls;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
