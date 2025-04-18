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
    public AudioSource aus;
    public AudioClip hit;
    public Transform player;

    public float offsetVal;  

    [Header("Private Variables")]
    private DamageFlash damageFlash;
    private Rigidbody2D rb;
    private ParticleSystem damageParticlesInstance;

    [SerializeField] private ParticleSystem damageParticles;

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
        if(damageParticles != null)
        {
            SpawnParticles();
        }
        if (hit != null)
        {
            aus.PlayOneShot(hit);
        }
        //reduce health by the damage amount
        damageFlash.CallDFlash();
        currentHealth -= damage;
        Vector2 direction = (transform.position - (Vector3)player.position).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnParticles()
    {
        //gains direction of where it was hit by calculating player dir
        Vector2 hitDirection = (transform.position - player.position).normalized;

        Vector3 spawnPosition = transform.position + (Vector3)(hitDirection * offsetVal);
        //Vector2 spawnPosition2D = (Vector2)transform.position + (hitDirection * offsetVal);

        //makes angle to rotate to
        float angle = Mathf.Atan2(hitDirection.y, hitDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        damageParticlesInstance = Instantiate(damageParticles, spawnPosition, rotation);
    }
}
