using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Subject
{
    [SerializeField] private GridScriptableObject gridScriptable;
    [SerializeField] private Transform wallParent;
    [SerializeField] private Transform floorParent;

    private List<Vector2> wallPosList = new List<Vector2>();
    private List<Vector2> floorPosList = new List<Vector2>();

    private Vector2 entrancePos;
    private Vector2 exitPos;

    private GameObject player;

    void Start()
    {
        GenerateGrid();
        CreateWall();
        CreateFloor();
        SpawnPlayer();
        SpawnSpirits();
    }

    private void GenerateGrid()
    {
        for (float x = gridScriptable.startPos.x; x <= gridScriptable.endPos.x; x++)
        {
            for (float y = gridScriptable.startPos.y; y <= gridScriptable.endPos.y; y++)
            {
                if (x == gridScriptable.startPos.x || x == gridScriptable.endPos.x || y == gridScriptable.startPos.y || y == gridScriptable.endPos.y)
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

    private void CreateWall()
    {
        WallEdgeFilter(ref entrancePos);
        WallEdgeFilter(ref exitPos);

        Instantiate(gridScriptable.portalPrefab, exitPos, Quaternion.identity);

        Instantiate(gridScriptable.floorPrefab, entrancePos, Quaternion.identity, floorParent);
        Instantiate(gridScriptable.floorPrefab, exitPos, Quaternion.identity, floorParent);

        foreach (Vector2 pos in wallPosList)
        {
            Instantiate(gridScriptable.wallPrefab, pos, Quaternion.identity, wallParent);
        }
    }

    private void WallEdgeFilter(ref Vector2 pos)
    {
        bool found = false;

        while (!found)
        {
            int index = Random.Range(0, this.wallPosList.Count);
            Vector2 currentPos = wallPosList[index];

            if (currentPos != gridScriptable.startPos && currentPos != gridScriptable.endPos 
                && currentPos != new Vector2(gridScriptable.startPos.x, gridScriptable.endPos.y)
                && currentPos != new Vector2(gridScriptable.endPos.x, gridScriptable.startPos.y))
            {
                pos = currentPos;
                wallPosList.Remove(currentPos);
                found = true;
            }
        }
    }

    private void CreateFloor()
    {
        for (int i = 0; i < Random.Range(5, 16); i++)
        {
            int randIndex = Random.Range(0, floorPosList.Count);
            Vector2 randPos = floorPosList[randIndex];

            // Check if the random position is adjacent to the border
            bool isAdjacentToBorder = Mathf.Approximately(randPos.x, gridScriptable.startPos.x + 1) || Mathf.Approximately(randPos.x, gridScriptable.endPos.x - 1)
                || Mathf.Approximately(randPos.y, gridScriptable.startPos.y + 1) || Mathf.Approximately(randPos.y, gridScriptable.endPos.y - 1);

            if (!isAdjacentToBorder)
            {
                Instantiate(gridScriptable.wallPrefab, randPos, Quaternion.identity, wallParent);
                floorPosList.RemoveAt(randIndex);
            }
        }

        foreach (Vector2 pos in floorPosList)
        {
            Instantiate(gridScriptable.floorPrefab, pos, Quaternion.identity, floorParent);
        }
    }

    private void SpawnPlayer()
    {
        player = Instantiate(gridScriptable.playerPrefab, entrancePos, Quaternion.identity);
        player.GetComponent<PlayerController>().playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void SpawnSpirits()
    {
        for (int i = 0; i < Random.Range(5, 16); i++)
        {
            int randIndex = Random.Range(0, floorPosList.Count);
            Vector2 currentPos = floorPosList[randIndex];

            if (randIndex % 2 == 1)
            {
                GameObject lightSpiritClone = Instantiate(gridScriptable.lightSpiritPrefab, currentPos, Quaternion.identity);

                LightEnemy lightEnemy = lightSpiritClone.GetComponent<LightEnemy>();
                lightEnemy.enemyScriptable.enemyType = EntityType.Light;
                lightEnemy.gridManager = this;
            }
            else
            {
                GameObject darkSpiritClone = Instantiate(gridScriptable.darkSpiritPrefab, currentPos, Quaternion.identity);
                DarkEnemy darkEnemy = darkSpiritClone.GetComponent<DarkEnemy>();
                darkEnemy.enemyScriptable.enemyType = EntityType.Dark;
                darkEnemy.gridManager = this;
            }

            floorPosList.Remove(currentPos);
        }
    }

    public void NotifyScore()
    {
        NotifyObservers(Random.Range(2, 6), ScoreType.Score);
    }

    public void NotifyType(EntityType eType)
    {
        int Value = Random.Range(1, 5);

        if (eType == EntityType.Dark)
        {
            NotifyObservers(Value, ScoreType.DarkSpirit);
        }
        else
        {
            NotifyObservers(Value, ScoreType.LightSpirit);
        }
    }
}
