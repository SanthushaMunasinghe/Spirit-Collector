using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameScriptableObjects", menuName = "ScriptableObjects/Bullet")]
public class BulletScriptableObject : ScriptableObject
{
    public float bulletSpeed = 5.0f;
    public float bulletLifetime = 10.0f;
    public Sprite[] bulletSprites;
}
