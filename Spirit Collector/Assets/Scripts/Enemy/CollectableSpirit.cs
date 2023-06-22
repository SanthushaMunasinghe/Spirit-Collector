using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpirit : MonoBehaviour
{
    [SerializeField] private CollectableSpiritScriptableObject collectableSpiritSciptable;
    public GridManager gridManager;

    private void Update()
    {
        Destroy(gameObject, 20.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerController"))
        {
            gridManager.NotifyType(collectableSpiritSciptable.entityType);
            gridManager.HealPlayer();
            Destroy(gameObject);
        }
    }
}
