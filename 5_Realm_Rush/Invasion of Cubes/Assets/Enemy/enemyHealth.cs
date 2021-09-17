using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] int maxHP = 5;
    [SerializeField] int currentHP = 0;

    Enemy enemy;

    void OnEnable()
    {
        currentHP = maxHP;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHP--;
        if (currentHP < 1)
        {
            enemy.RewardGold();
            gameObject.SetActive(false);
        }
    }
}