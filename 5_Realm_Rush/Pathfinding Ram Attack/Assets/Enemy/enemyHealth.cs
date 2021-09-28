using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Enemy))]
public class enemyHealth : MonoBehaviour
{
    [SerializeField] int maxHP = 5;
    [Tooltip("Adds amount to maxHP when enemy dies")]
    [SerializeField] int difficultyRamp = 1;

    [SerializeField] TextMeshProUGUI displayHealth;

    int currentHP = 0;

    Enemy enemy;
    KillCounter killCounter;

    void OnEnable()
    {
        currentHP = maxHP;
        UpdateDisplay();
    }

    void Awake()
    {
        killCounter = FindObjectOfType<KillCounter>();
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        UpdateDisplay();
    }

    void ProcessHit()
    {
        currentHP--;
        if (currentHP < 1)
        {
            enemy.RewardGold();
            maxHP += difficultyRamp;
            gameObject.SetActive(false);
            killCounter.AddToKillCount();
        }
    }

    void UpdateDisplay()
    {
        displayHealth.text = "HP: " + currentHP;
    }
}