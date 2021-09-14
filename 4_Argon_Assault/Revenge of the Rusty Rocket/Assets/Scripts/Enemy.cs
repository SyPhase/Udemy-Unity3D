using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX; // Explosion particle effect
    [SerializeField] GameObject damageVFX; // damage particle effect

    [SerializeField] int killScore = 10; // score per kill
    [SerializeField] int health = 2; // number of shots before enemy dies (each laser is 1)

    GameObject parentGameObject; // SpawnAtRuntime empty
    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnRuntime");
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false; // prevents enemies from falling
        rb.isKinematic = true; // prevents enemies from colliding (when not on track?)
    }

    void OnParticleCollision(GameObject other)
    {
        if (health <= 0)
        {
            KillEnemy();
        }
        else
        {
            DamageEnemy();
        }
    }

    void ProcessHit()
    {
        scoreBoard.ChangeScore(killScore);
    }

    void KillEnemy()
    {
        ProcessHit();
        GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(this.gameObject);
    }

    void DamageEnemy()
    {
        GameObject vfx = Instantiate(damageVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        health -= 1; // both lasers do 1, so if both hit its 2
    }
}