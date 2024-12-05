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
    public GameObject gameOverUI;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Image Health4;
    public Image Health3;
    public Image Health2;
    public Image Health1;
    public Image rarhappy;
    public Image rarhurt;
    public Rigidbody2D rb;
    public GameObject enemy;
    public float pushBackForce;
    public CinemachineImpulseSource impulseSource;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
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
        if (playerMovement.isInvincible)
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

        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }

        if (currentHealth <= 0)
        {
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }
            UpdateHealthBar();
            Destroy(gameObject);
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
        //animator.SetBool("isHurt", false);
        playerMovement.isInvincible = true;
        //does the does and changes sprite color to make it transparent
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        //makes player invicible
        yield return new WaitForSeconds(iFrames);
        //undoes the does
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
}
