using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpecialBubble : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    public float growthSpeed;
    public string originalTag;
    public float attackDamage;
    public float speed;
    public bool willKillPlayer = false;
    public bool willKillBoss = false;
    public bool hitByBubble = false;
    public ParticleSystem bubbleDeath;
    public SpriteRenderer bubbleSprite;

    private PlayerHealth playerHealth;
    private BossHealth bossHealth;
    private LobsterAttackManager lobAttackManager;
    private Vector3 originalSize;
    private Transform playerTransform;

    public GameObject bubbleObj;
    public GameObject goon1;
    public GameObject goon2;
    public GameObject goon3;

    // New timer variable to control when the bubble grows
    private float growthTimer;
    public float maxGrowthTime = 5f; // Time in seconds to wait before bubble grows to custom size

    // New flag to prevent the scale from changing after it fully grows
    private bool hasFullyGrown = false;

    void Start()
    {
        // Store original size and set up other references
        originalSize = transform.localScale;
        originalTag = gameObject.tag;
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        bossHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>();
        lobAttackManager = GameObject.FindGameObjectWithTag("Boss").GetComponent<LobsterAttackManager>();

        // Initialize growth timer
        growthTimer = maxGrowthTime;
    }

    void Update()
    {
        // Check if the game object is active before doing anything
        if (!gameObject.activeSelf)
        {
            return;
        }

        // Decrease the growth timer over time
        if (growthTimer > 0)
        {
            growthTimer -= Time.deltaTime;
        }
        else
        {
            // Once the timer reaches zero, set the size to the custom values (only if it hasn't grown already)
            if (!hasFullyGrown)
            {
                transform.localScale = new Vector3(0.7778037f, 0.7778037f, 0.7778037f);
                hasFullyGrown = true; // Set the flag to prevent further scaling
            }
        }

        // Tag change logic when the bubble reaches max size
        if (transform.localScale.x == 0.7778037f && gameObject.tag != "SpecialBullet")
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            gameObject.tag = "SpecialBullet";
            willKillPlayer = true;
        }

        // Change tag to BossHurter when all goons are destroyed
        if (goon1 == null && goon2 == null && goon3 == null)
        {
            if (gameObject.tag != "BossHurter")
            {
                gameObject.tag = "BossHurter";
                willKillBoss = true;
                willKillPlayer = false;
                lobAttackManager.isAttacking = false;
            }
        }

        // Move towards the player if the bubble will kill the player
        if (willKillPlayer == true)
        {
            MoveTowardsPlayer();
        }

        // Handle bubble death logic when it is targeting the boss
        if (willKillBoss == true)
        {
            StartCoroutine(BubbleDeath());
        }
    }

    private void MoveTowardsPlayer()
    {
        // Calculate direction towards the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Move the bubble towards the player
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameObject.tag == "SpecialBullet")
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Boss") && gameObject.tag == "BossHurter")
        {
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator BubbleDeath()
    {
        CameraManager.SwitchCamera(cam2);
        yield return new WaitForSeconds(1.5f);
        bubbleSprite.enabled = false;
        bubbleDeath.Play();
        yield return new WaitForSeconds(1f);
        bossHealth.currentHealth = 10f;
        bossHealth.isInvincible = false;
        hitByBubble = true;
        yield return new WaitForSeconds(2f);
        CameraManager.SwitchCamera(cam1);
        Destroy(gameObject);
    }
}