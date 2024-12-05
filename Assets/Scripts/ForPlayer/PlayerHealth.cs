using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 3f;
    public float currentHealth;
    public float iFrames;
    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Image Health4;
    public Image Health3;
    public Image Health2;
    public Image Health1;
    public Image rarhappy;
    public Image rarDead;
    public Image skewer;
    public Image rarhurt;
    public Rigidbody2D rb;
    public GameObject enemy;
    public float pushBackForce;
    public GameObject corpse;
    public CinemachineImpulseSource impulseSource;
    public Animator animator;
    public Animator corpseanimator;
    public GameObject deathScreen;
    private AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip squeak;
    public BoxCollider2D bc;
    public PlayerAttack playerAttack;
    public GameObject gameoverUI;
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
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        UpdateHealthBar();
        if (impulseSource == null)
        {
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }
        
    }
    public void TakeDamage(float damage)
    {
        if (playerMovement.isInvincible )
        {
            Debug.Log("Invincible");
            return;
        }
        StartCoroutine(HurtAnimPlay());
        playerMovement.pushed = true;

        Vector2 pushDirection = (transform.position - enemy.transform.position).normalized;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        }

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
        playerMovement.pushed = false;
    }
    private IEnumerator HurtAnimPlay()
    {   
        animator.SetBool("isHurt", true);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isHurt", false);
    }
    private IEnumerator ImInvincibleAdoOnePiece()
    {
        int invisibleWallLayer = LayerMask.NameToLayer("InvisibleWalls");
        //animator.SetBool("isHurt", false);
        playerMovement.isInvincible = true;
        //bc.enabled = false;
        //enables collision with invisible walls
        Physics2D.IgnoreLayerCollision(gameObject.layer, invisibleWallLayer, false);
        for (int layerIndex = 0; layerIndex < 32; layerIndex++)
        {
            if (layerIndex != invisibleWallLayer)
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

        playerMovement.isInvincible = false;
    }
    private void UpdateHealthBar()
    {
        if(currentHealth <= 3)
        {
            Health4.gameObject.SetActive(false);
        }
        if (currentHealth <= 2)
        {
            Health3.gameObject.SetActive(false);
            rarhappy.gameObject.SetActive(false);
        }
        if (currentHealth <= 1)
        {
            Health2.gameObject.SetActive(false);
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
}
