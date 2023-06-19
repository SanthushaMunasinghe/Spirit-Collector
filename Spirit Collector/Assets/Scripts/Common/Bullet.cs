using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletScriptableObject bulletScriptable;
    private float currentLifetime;
    public EntityType bulletType;

    private void OnEnable()
    {
        currentLifetime = bulletScriptable.bulletLifetime;
    }

    public void SetSprite()
    {
        if (bulletType == EntityType.Light)
            GetComponent<SpriteRenderer>().sprite = bulletScriptable.bulletSprites[0];
        else
            GetComponent<SpriteRenderer>().sprite = bulletScriptable.bulletSprites[1];
    }

    void Update()
    {
        if (currentLifetime <= 0)
        {
            BasePool.Instance.playerBulletPool.Release(gameObject);
        }
        else
        {
            currentLifetime -= Time.deltaTime;
        }

        transform.position += transform.up * bulletScriptable.bulletSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BasePool.Instance.playerBulletPool.Release(gameObject);
    }
}
