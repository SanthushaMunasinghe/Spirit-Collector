using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform wallParent;
    [SerializeField] private Transform floorParent;

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;

    [SerializeField] private GameObject spawnPoint;

    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;

    private List<Vector2> wallPosList = new List<Vector2>();
    private List<Vector2> floorPosList = new List<Vector2>();

    private Vector2 entrancePos;
    private Vector2 exitPos;

    void Start()
    {
        GenerateGrid();
        CreateWall();
        CreateFloor();
    }

    private void CreateWall()
    {
        WallEdgeFilter(ref entrancePos);
        WallEdgeFilter(ref exitPos);

        Instantiate(floorPrefab, entrancePos, Quaternion.identity, floorParent);
        Instantiate(floorPrefab, exitPos, Quaternion.identity, floorParent);

        foreach (Vector2 pos in wallPosList)
        {
            Instantiate(wallPrefab, pos, Quaternion.identity, wallParent);
        }
    }

    private void WallEdgeFilter(ref Vector2 pos)
    {
        bool found = false;

        while (!found)
        {
            int index = Random.Range(0, this.wallPosList.Count);
            Vector2 currentPos = wallPosList[index];

            if (currentPos != startPos && currentPos != endPos && currentPos != new Vector2(startPos.x, endPos.y)
                && currentPos != new Vector2(endPos.x, startPos.y))
            {
                pos = currentPos;
                wallPosList.Remove(currentPos);
                found = true;
            }
        }
    }

    private void CreateFloor()
    {
        for (int i = 0; i < Random.Range(5, 11); i++)
        {
            int randIndex = Random.Range(0, floorPosList.Count);
            Vector2 randPos = floorPosList[randIndex];

            // Check if the random position is adjacent to the border
            bool isAdjacentToBorder = Mathf.Approximately(randPos.x, startPos.x) || Mathf.Approximately(randPos.x, endPos.x)
                || Mathf.Approximately(randPos.y, startPos.y) || Mathf.Approximately(randPos.y, endPos.y);

            if (!isAdjacentToBorder)
            {
                Instantiate(wallPrefab, randPos, Quaternion.identity, wallParent);
                floorPosList.RemoveAt(randIndex);
            }
        }

        foreach (Vector2 pos in floorPosList)
        {
            Instantiate(floorPrefab, pos, Quaternion.identity, floorParent);
        }
    }

    private void GenerateGrid()
    {
        for (float x = startPos.x; x <= endPos.x; x++)
        {
            for (float y = startPos.y; y <= endPos.y; y++)
            {
                if (x == startPos.x || x == endPos.x || y == startPos.y || y == endPos.y)
                {
                    wallPosList.Add(new Vector2(x, y));
                }
                else
                {
                    floorPosList.Add(new Vector2(x, y));
                }
            }
        }
    }
}
