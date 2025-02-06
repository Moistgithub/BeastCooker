using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpecialBubble : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    public Vector3 maxSize = new Vector3(2f, 2f, 2f);
    public float growthSpeed;
    public string originalTag;
    public float attackDamage;
    public float speed;
    public bool willKillPlayer = false;
    public bool willKillBoss = false;
    public ParticleSystem bubbleDeath;
    public SpriteRenderer bubbleSprite;

    private PlayerHealth playerHealth;
    private BossHealth bossHealth;
    private LobsterAttackManager lobAttackManager;
    private Vector3 originalSize;
    private Transform playerTransform;

    public GameObject goon1;
    public GameObject goon2;
    public GameObject goon3;

    void Start()
    {
        //store original size of the object
        originalSize = transform.localScale;
        originalTag = gameObject.tag;
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        bossHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>();
        lobAttackManager = GameObject.FindGameObjectWithTag("Boss").GetComponent<LobsterAttackManager>();
    }

    void Update()
    {
        //calculate the new size
        Vector3 targetSize = Vector3.Lerp(originalSize, maxSize, Time.time * growthSpeed);

        //apply the new size
        transform.localScale = Vector3.Min(targetSize, maxSize);

        //tag change
        if(transform.localScale == maxSize)
        {
            if (gameObject.tag != "SpecialBullet")
            {
                playerTransform = GameObject.FindWithTag("Player").transform;
                gameObject.tag = "SpecialBullet";
                willKillPlayer = true;
            }
        }
        if(goon1 == null && goon2 == null && goon3 == null)
        {
            if (gameObject.tag != "BossHurter")
            {
                gameObject.tag = "BossHurter";
                willKillBoss = true;
                willKillPlayer = false;
                lobAttackManager.isAttacking = false;
            }
        }
        if(willKillPlayer == true)
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
        //calculate direction towards the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        //move the bubble towards the player
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
        bossHealth.currentHealth = 15f;
        yield return new WaitForSeconds(2f);
        CameraManager.SwitchCamera(cam1);
        Destroy(gameObject);
    }
}
