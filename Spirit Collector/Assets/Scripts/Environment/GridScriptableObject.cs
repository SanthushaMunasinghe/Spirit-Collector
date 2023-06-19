using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameScriptableObjects", menuName = "ScriptableObjects/Grid")]
public class GridScriptableObject : ScriptableObject
{
    [Header("Grid Settings")]
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public Vector2 startPos;
    public Vector2 endPos;

    [Header("Characters")]
    public GameObject playerPrefab;
    public GameObject lightSpiritPrefab;
    public GameObject darkSpiritPrefab;
}
