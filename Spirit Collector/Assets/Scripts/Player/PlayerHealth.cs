using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private int playerHealth;
    private bool isReduce = true;

    [SerializeField] private float reduceSpeed = 3f;
    [SerializeField] private TMP_Text healthTxt;

    void Awake()
    {
        playerHealth = CurrentScore.playerHealth;
    }

    void Update()
    {
        ReduceScore();
    }

    private void ReduceScore()
    {
        if (!isReduce) return;

        if (playerHealth > 0)
            playerHealth--;

        UpdateUI();
        StartCoroutine(EnableReduce());
    }

    IEnumerator EnableReduce()
    {
        isReduce = false;

        yield return new WaitForSeconds(reduceSpeed);

        isReduce = true;
    }

    public void DamageEnemy()
    {
        for (int i = 0; i < 10; i++)
        {
            if (playerHealth > 0)
                playerHealth--;
        }

        UpdateUI();
    }

    public void HealPlayer(int value)
    {
        for (int i = 0; i < value; i++)
        {
            if (playerHealth < 100)
                playerHealth++;
        }
    }

    private void UpdateUI()
    {
        healthTxt.text = "Health: " + playerHealth.ToString();
    }
}
