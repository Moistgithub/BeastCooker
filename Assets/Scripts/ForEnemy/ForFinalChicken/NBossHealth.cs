using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBossHealth : MonoBehaviour
{
    [Header("Public Variables")]
    public float maxHealth;
    public float currentHealth;
    public float knockbackForce;
    
    public bool isInvincible = false;

    public Transform player;

    [Header("Private Variables")]
    private DamageFlash damageFlash;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        damageFlash = GetComponent<DamageFlash>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible == true)
            return;
        // Reduce health by the damage amount
        damageFlash.CallDFlash();
        currentHealth -= damage;
        Vector2 direction = (transform.position - (Vector3)player.position).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
