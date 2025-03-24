using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Public variables")]
    public float maxHealth = 3f;
    public float currentHealth;
    public float iFrames;
    public bool isHurt = false;
    public float pushBackForce;

    [Header("References")]
    private NewPlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("Health UI")]
    //Health Icons
    public Image Health4;
    public Image Health3;
    public Image Health2;
    public Image Health1;
    public Image rarhappy;
    public Image rarDead;
    public Image skewer;
    public Image rarhurt;
    public GameObject gameoverUI;
    public GameObject deathScreen;

    [Header("Unity Components and Game Objects")]
    public BoxCollider2D bc;
    public Rigidbody2D rb;
    public GameObject enemy;
    public GameObject corpse;
    public CinemachineImpulseSource impulseSource;
    public Animator animator;
    public Animator corpseanimator;

    [Header("Audio Stuff")]
    private AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip squeak;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        bc = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        corpseanimator = GetComponent<Animator>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        playerMovement = GetComponent<NewPlayerMovement>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        UpdateHealthBar();
        if (impulseSource == null)
        {
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }
        
    }
    public void TakeDamage(float damage)
    {
        if (playerMovement.isInvincible)
        {
            Debug.Log("Invincible");
            return;
        }
        StartCoroutine(PushBackTimer());

        //opposite direction of last movement
        Vector2 pushDirection = -playerMovement.lastMoveDir.normalized;

        if (rb != null)
        {
            //apply pushback
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            rb.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        }

        //playerMovement.pushed = true;

        currentHealth -= damage;
        audioSource.PlayOneShot(squeak);
        /*if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
        */

        if (currentHealth <= 0)
        {
            UpdateHealthBar();
            //Destroy(gameObject);
            StartCoroutine(DeadMode());
        }
        else
        {
            StartCoroutine(ImInvincibleAdoOnePiece());
            UpdateHealthBar();
        }
        StartCoroutine(ResetPushed());
    }
    private IEnumerator ResetPushed()
    {
        yield return new WaitForSeconds(0.2f);
        //playerMovement.pushed = false;
    }
    private IEnumerator PushBackTimer()
    {   
        animator.SetBool("isHurt", true);
        isHurt = true;
        yield return new WaitForSeconds(0.3f);
        isHurt = false;
    }
    private IEnumerator ImInvincibleAdoOnePiece()
    {
        playerMovement.isInvincible = true;
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
        yield return new WaitForSeconds(iFrames);
        spriteRenderer.color = originalColor;
        playerMovement.isInvincible = false;
        /*int invisibleWallLayer = LayerMask.NameToLayer("InvisibleWalls");
        //animator.SetBool("isHurt", false);
        playerMovement.isInvincible = true;
        //bc.enabled = false;
        //enables collision with invisible walls
        Physics2D.IgnoreLayerCollision(gameObject.layer, invisibleWallLayer, false);
        for (int layerIndex = 0; layerIndex < 32; layerIndex++)
        {
            if (layerIndex != invisibleWallLayer)// || layerIndex != attackLayer)
            {
                //stops it from colliding with any other layer
                Physics2D.IgnoreLayerCollision(gameObject.layer, layerIndex, true);
            }
        }
        //does the does and changes sprite color to make it transparent
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        //makes player invicible
        yield return new WaitForSeconds(iFrames);
        //restores collision
        for (int layerIndex = 0; layerIndex < 32; layerIndex++)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, layerIndex, false);
        }
        //undoes the does
        //bc.enabled = true;
        spriteRenderer.color = originalColor;
        */
    }
    private void UpdateHealthBar()
    {
        if(currentHealth <= 3)
        {
            Health4.gameObject.SetActive(false);
        }
        else
        {
            Health4.gameObject.SetActive(true);
        }
        if (currentHealth <= 2)
        {
            Health3.gameObject.SetActive(false);
            rarhappy.gameObject.SetActive(false);
        }
        else
        {
            Health3.gameObject.SetActive(true);
            rarhappy.gameObject.SetActive(true);
        }
        if (currentHealth <= 1)
        {
            Health2.gameObject.SetActive(false);
        }
        else
        {
            Health2.gameObject.SetActive(true);
        }
        if (currentHealth <= 0)
        {
            rarhurt.gameObject.SetActive(false);
            Health1.gameObject.SetActive(false);
        }
    }
    private IEnumerator DeadMode()
    {
        if (impulseSource != null)
        {
            audioSource.PlayOneShot(deathSound);
            Destroy(rarDead);
            Destroy(skewer);
            deathScreen.SetActive(true);
            impulseSource.GenerateImpulse();
            HitStop.Instance.StopTime(0.1f);
        }
        //bc.enabled = false;
        playerAttack.enabled = false;
        rb.simulated = false;
        spriteRenderer.enabled = false;
        corpse.SetActive(true);
        corpseanimator.SetBool("IsDead", true);
        enemy.SetActive(false);
        playerMovement.enabled = false;
        yield return new WaitForSeconds(1f);
        corpseanimator.SetBool("IsDead", false);
        gameoverUI.SetActive(true);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            if (currentHealth < maxHealth)
            {
                Heal(+1);
                Destroy(other.gameObject);
            }
        }
    }
    private void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdateHealthBar();
    }
}
