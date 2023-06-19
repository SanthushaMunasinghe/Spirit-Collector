using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameScriptableObjects", menuName = "ScriptableObjects/CollectableSpirit")]
public class CollectableSpiritScriptableObject : ScriptableObject
{
    public EntityType entityType;
}
