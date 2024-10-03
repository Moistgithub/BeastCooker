using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }

    }
    public void Death()
    {

        Destroy(gameObject);
    }
}
