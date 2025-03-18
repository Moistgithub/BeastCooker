using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBossHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public bool isInvincible = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible == true)
            return;
        // Reduce health by the damage amount
        currentHealth -= damage;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
