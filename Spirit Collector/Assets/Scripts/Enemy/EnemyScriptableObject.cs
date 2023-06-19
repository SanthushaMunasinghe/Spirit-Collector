using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameScriptableObjects", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float detectionRadius = 5f;
    public float impulseForce = 10f;

    public EntityType enemyType;
}
