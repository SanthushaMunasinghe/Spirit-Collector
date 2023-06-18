using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5.0f;
    [SerializeField] private float bulletLifetime = 10.0f;

    void Update()
    {
        if (bulletLifetime <= 0)
        {
            BasePool.Instance.playerBulletPool.Release(gameObject);
        }
        else
        {
            bulletLifetime -= Time.deltaTime;
        }

        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BasePool.Instance.playerBulletPool.Release(gameObject);
    }
}
