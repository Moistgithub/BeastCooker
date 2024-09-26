using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject meatPrefab;
    public GameObject breakableBodyPartA;
    public GameObject breakableBodyPartB;
    public GameObject eggSpawner;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth == 30)
        {
            Destroy(breakableBodyPartA);
        }

        if (currentHealth == 10)
        {
            Destroy(breakableBodyPartB);
            eggSpawner.SetActive(true);
        }


        if (currentHealth <= 0)
        {
            eggSpawner.SetActive(false);
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
