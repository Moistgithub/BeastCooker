using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        UpdateHealthBar();
    }
    public void TakeDamage(float damage)
    {
        if (playerMovement.isInvincible)
        {
            Debug.Log("Invincible");
            return;
        }

        currentHealth -= damage;
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
    }
    private IEnumerator ImInvincibleAdoOnePiece()
    {
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
