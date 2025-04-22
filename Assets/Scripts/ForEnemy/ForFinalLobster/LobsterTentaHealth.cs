using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterTentaHealth : MonoBehaviour
{
    [Header("Public Variables")]
    public float maxHealth;
    public float currentHealth;
    public float offsetVal;

    public AudioSource aus;
    public AudioClip hit;
    public Transform player;
    public SpriteRenderer sr;
    public float fadeDuration = 1f;
    public float fadeinDuration = 2f;

    private bool isDying = false;
    private bool cantdamage = false;

    private DamageFlash damageFlash;

    private ParticleSystem damageParticlesInstance;
    [SerializeField] private ParticleSystem damageParticles;
    // Start is called before the first frame update
    void Start()
    {
        damageFlash = GetComponent<DamageFlash>();
        sr = GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;

        Color startColor = sr.color;
        sr.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (cantdamage) return;
        if (isDying) return;
        if (damageParticles != null)
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
        if (currentHealth <= 0)
        {
            cantdamage = true;
            StartCoroutine(FadeAndDestroy());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            HitStop.Instance.StopTime(0.2f);
            TakeDamage(1f);
        }
    }
    private void SpawnParticles()
    {
        //gains direction of where it was hit by calculating player dir
        Vector2 hitDirection = (transform.position - player.position).normalized;
        if (player != null)
        {
            //Vector3 spawnPosition = transform.position + (Vector3)(hitDirection * offsetVal);
            Vector3 spawnPosition = player.position + (Vector3)(hitDirection * offsetVal);

            //makes angle to rotate to
            float angle = Mathf.Atan2(hitDirection.y, hitDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            damageParticlesInstance = Instantiate(damageParticles, spawnPosition, rotation);
        }
        //Vector2 spawnPosition2D = (Vector2)transform.position + (hitDirection * offsetVal);

    }
    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color originalColor = sr.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeinDuration);
            sr.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        sr.color = targetColor;
    }
    private IEnumerator FadeAndDestroy()
    {
        float elapsed = 0f;
        Color originalColor = sr.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Destroy(gameObject);
    }
}
