using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEnemy : MonoBehaviour
{
    public EnemyScriptableObject enemyScriptable;
    public GridManager gridManager;

    private int health;

    private float moveDuration;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float currentMoveDuration;
    private bool isMoving;
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
        if (isMoving)
        {
            currentMoveDuration -= Time.deltaTime;
            if (currentMoveDuration <= 0f)
            {
                StopMoving();
            }
        }

        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.AddForce(moveDirection * enemyScriptable.moveSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMoving)
        {
            ChangeDirection();
        }

        if (collision.collider.tag == "Bullet")
        {
            if (collision.collider.gameObject.GetComponent<Bullet>().bulletType == EntityType.Light)
            {
                health--;
                gridManager.NotifyScore();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isMoving && collision.CompareTag("Player"))
        {
            ImpulseTowardsPlayer();
        }
    }

    private void StartMoving()
    {
        moveDirection = GetRandomDirection();
        moveDuration = Random.Range(1.5f, 2.5f);
        currentMoveDuration = moveDuration;
        isMoving = true;
    }

    private void StopMoving()
    {
        rb.velocity = Vector2.zero;
        isMoving = false;
        Invoke("StartMoving", Random.Range(1f, 3f));
    }

    private Vector2 GetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        return new Vector2(randomX, randomY).normalized;
    }

    private void ChangeDirection()
    {
        moveDirection = -moveDirection;
    }

    private void ImpulseTowardsPlayer()
    {
        Vector2 playerDirection = player.position - transform.position;
        rb.AddForce(playerDirection.normalized * enemyScriptable.impulseForce, ForceMode2D.Impulse);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
