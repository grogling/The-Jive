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

    public int columns = 14;
    public int rows = 14;
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
            {
                gridpositions.Add(new Vector3(x, y, 0f));

            }
                
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


    void RoomSetup()
    {
        GameObject toInstantiate= walls[0];
        for (int x = -1; x < columns; x++)
        {
            for (int y = -1; y < rows; y++)
            {
                if (x == 4 || x == 9 || y == 4 || y == 9)
                {
                    toInstantiate = walls[0];
                    gridpositions.Remove(new Vector3(x, y, 0f));
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                }
                
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridpositions.Count);
        Vector3 randomPosition = gridpositions[randomIndex];
        gridpositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void ObjectPlacement (GameObject[] objectArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        for (int i = 0; i <objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject objectChoice = objectArray[Random.Range(0, objectArray.Length)];
            Instantiate(objectChoice, randomPosition, Quaternion.identity);
        }
        
               
        
    }

    public void SetupScene()
    {
        BoardSetup();
        InitializeList();
        RoomSetup();
        ObjectPlacement(walls, 1, 10);
        foreach (Vector3 x in gridpositions)
        {
            Debug.Log(x);
        }
       

    }
}

