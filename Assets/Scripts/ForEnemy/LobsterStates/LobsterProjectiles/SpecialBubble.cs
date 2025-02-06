using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpecialBubble : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    public Animator lobsterAnimator;
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
    public float maxGrowthTime = 15f;

    // New flag to prevent the scale from changing after it fully grows
    private bool hasFullyGrown = false;

    void Start()
    {
        lobsterAnimator = GetComponentInChildren<Animator>();
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
        if (!gameObject.activeSelf)
        {
            return;
        }


        if (growthTimer > 0)
        {
            growthTimer -= Time.deltaTime;
        }
        else
        {
            if (!hasFullyGrown)
            {
                transform.localScale = new Vector3(0.7778037f, 0.7778037f, 0.7778037f);
                hasFullyGrown = true;
            }
        }


        if (transform.localScale.x == 0.7778037f && gameObject.tag != "SpecialBullet")
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            gameObject.tag = "SpecialBullet";
            willKillPlayer = true;
        }


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

        if (willKillPlayer == true)
        {
            MoveTowardsPlayer();
        }

        if (willKillBoss == true)
        {
            StartCoroutine(BubbleDeath());
        }
    }

    private void MoveTowardsPlayer()
    {

        Vector3 direction = (playerTransform.position - transform.position).normalized;


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
                //bossHealth.TakeDamage(10f);
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
        bossHealth.currentHealth = 65;
        bossHealth.isInvincible = false;
        hitByBubble = true;
        yield return new WaitForSeconds(2f);
        CameraManager.SwitchCamera(cam1);
        Destroy(gameObject);
        lobsterAnimator.SetBool("Special", false);
        lobsterAnimator.SetBool("Idle", false);
    }
}