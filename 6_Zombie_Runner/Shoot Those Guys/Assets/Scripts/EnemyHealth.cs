using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] TextMeshProUGUI displayHealth;
    //[SerializeField] Canvas healthBar;

    bool isDead = false;

    void Start()
    {
        UpdateHealthBar();
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        UpdateHealthBar();
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) { return; }
        isDead = true;
        RemoveHealthBar();
        GetComponent<Animator>().SetTrigger("die");
    }

    void UpdateHealthBar()
    {
        displayHealth.text = "HP: " + hitPoints;
    }

    void RemoveHealthBar()
    {
        displayHealth.enabled = false;
    }
}