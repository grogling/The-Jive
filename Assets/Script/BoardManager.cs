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
    public int cellSize = 1;
    public GameObject[] walls;
    public GameObject[] floortiles;
    public GameObject[] FoWMain;
    public GameObject[] FoWSecondary;
    public GameObject[] FoWCanvas;
    public GameObject[] enemies;
    

    private Transform boardHolder;
    private List<Vector3> gridpositions = new List<Vector3>();

    void InitializeList()
    {
        gridpositions.Clear ();

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                gridpositions.Add(new Vector3(x, y, 0f));

            }
                
        }
        Debug.Log(gridpositions.Count);
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = 0; x < columns; x++)
        {
            for (int y = -1 ; y < rows; y++)
            {
                GameObject toInstantiate = floortiles[0];
                         
                if (y == -1)
                    toInstantiate = walls[0];

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f),Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);

            }
        }
        GameObject FoW1 = Instantiate(FoWMain[0]) as GameObject;
        GameObject FoW2 = Instantiate(FoWSecondary[0]) as GameObject;
        GameObject FoW3 = Instantiate(FoWCanvas[0]) as GameObject;
    }


    void RoomSetup()
    {
        
        var maze = MazeGenerator.Generate(columns/2, rows/2);
        GameObject toInstantiate = walls[0];

        for (int x = 0; x < columns/2; x++)
        {
            for (int y = 0; y < rows/2; y++)
            {
                var cell = maze[x, y];
                var position = new Vector3(2*x, 2*y, 0);
                if (cell.HasFlag(WallState.UP))
                {
                    GameObject instance = Instantiate(toInstantiate, position + new Vector3(0f, 1, 0f), Quaternion.identity) as GameObject;
                    instance = Instantiate(toInstantiate, position + new Vector3(-1, 1, 0f), Quaternion.identity) as GameObject;
                    instance = Instantiate(toInstantiate, position + new Vector3(1, 1, 0f), Quaternion.identity) as GameObject;
                    gridpositions.RemoveAt(gridpositions.LastIndexOf(position + new Vector3(0f, 1, 0f)));
                }
                if (cell.HasFlag(WallState.LEFT))
                {
                    GameObject instance = Instantiate(toInstantiate, position + new Vector3(-1, 0f, 0f), Quaternion.identity) as GameObject;
                    instance = Instantiate(toInstantiate, position + new Vector3(-1, 1, 0f), Quaternion.identity) as GameObject;
                    instance = Instantiate(toInstantiate, position + new Vector3(-1, -1, 0f), Quaternion.identity) as GameObject;
                }
                if (x == columns/2-1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        GameObject instance = Instantiate(toInstantiate, position + new Vector3(1, 0f, 0f), Quaternion.identity) as GameObject;
                        instance = Instantiate(toInstantiate, position + new Vector3(1, 1, 0f), Quaternion.identity) as GameObject;
                        instance = Instantiate(toInstantiate, position + new Vector3(1, -1, 0f), Quaternion.identity) as GameObject;
                    }
                }
                
            }
        }
        Debug.Log(gridpositions.Count);
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
        
        //ObjectPlacement(walls, 1, 10);
        //foreach (Vector3 x in gridpositions)
        //{
        //    Debug.Log(x);
        //}


    }
}

