using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy : MonoBehaviour
{
    public EnemyScriptableObject enemyScriptable;
    public GridManager gridManager;

    public float waitDuration = 2f;

    private int health;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float currentMoveDuration;
    private bool isWaiting;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = Random.Range(3, 6);
    }

    private void Start()
    {
        StartMoving();
    }

    private void Update()
    {
        if (isWaiting)
        {
            currentMoveDuration -= Time.deltaTime;
            if (currentMoveDuration <= 0f)
            {
                StartMoving();
            }
        }

        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void FixedUpdate()
    {
        if (!isWaiting)
        {
            rb.AddForce(moveDirection * enemyScriptable.moveSpeed, ForceMode2D.Impulse);
            isWaiting = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            if (collision.collider.gameObject.GetComponent<Bullet>().bulletType == EntityType.Dark)
            {
                health--;
                gridManager.NotifyScore();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isWaiting && collision.CompareTag("Player"))
        {
            ImpulseAwayFromPlayer();
        }
    }

    private void StartMoving()
    {
        moveDirection = GetRandomDirection();
        waitDuration = Random.Range(1.5f, 2.5f);
        currentMoveDuration = waitDuration;
        isWaiting = false;
    }

    private Vector3 GetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        return new Vector2(randomX, randomY).normalized;
    }

    private void ImpulseAwayFromPlayer()
    {
        Vector2 playerDirection = transform.position - player.position;
        rb.AddForce(playerDirection.normalized * enemyScriptable.impulseForce, ForceMode2D.Impulse);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
