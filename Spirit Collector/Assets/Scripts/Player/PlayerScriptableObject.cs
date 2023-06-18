using UnityEngine;

[CreateAssetMenu(fileName = "GameScriptableObjects", menuName = "ScriptableObjects/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    [Header("Movement Settings")]
    public float rotSpeed = 10.0f;
    public float moveSpeed = 10.0f;

    [Header("Shoot Settings")]
    public float shootingRate = 0.25f;
}
