using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public bool isInvincible = false;
    public bool triggerSpecialAttack = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible == true)
            return;
        // Reduce health by the damage amount
        currentHealth -= damage;

        // Check if health reaches zero or below
        /*if (currentHealth <= 5 && !triggerSpecialAttack)
        {
            isInvincible = true;
            triggerSpecialAttack = true;
        }*/

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Destroy the enemy
        Destroy(gameObject);
    }
}
