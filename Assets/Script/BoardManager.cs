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

    public int columns = 15;
    public int rows = 15;
    public GameObject[] walls;
    public GameObject[] floortiles;

    private Transform boardHolder;
    private List<Vector3> gridpositions = new List<Vector3>();

    void InitializeList()
    {
        gridpositions.Clear ();

        for (int x = 1; x < columns-1; x++)
        {
            for (int y = 1; y < rows-1; y++)
                gridpositions.Add(new Vector3(x, y, 0f));
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
            for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floortiles[0];

                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = walls[0];

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f),Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);

            }
        }
    }

    public void SetupScene()
    {
        BoardSetup();
        InitializeList();
    }
}

