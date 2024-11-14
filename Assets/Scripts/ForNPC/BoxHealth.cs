using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHealth : MonoBehaviour
{
    public float maxHealth = 3f;
    public float currentHealth;
    public GameObject itemGain;

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
        if (itemGain != null)
        {
            Instantiate(itemGain, transform.position, transform.rotation);
            //meat spawn
        }
        Destroy(gameObject);
    }
}
