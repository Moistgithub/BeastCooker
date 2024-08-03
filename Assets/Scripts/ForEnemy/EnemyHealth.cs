using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 3f;
    public float currentHealth;
    public GameObject meatPrefab;

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
            DeathSpawn();
        }

    }
    public void DeathSpawn()
    {
        if (meatPrefab != null)
        {
            Instantiate(meatPrefab, transform.position, transform.rotation);
            //meat spawn
        }
        Destroy(gameObject);
    }
}
