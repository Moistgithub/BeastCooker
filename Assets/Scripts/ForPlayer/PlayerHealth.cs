using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 3f;
    public float currentHealth;
    public float iFrames;
    private PlayerMovement playerMovement;
    public GameObject gameOverUI;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    public void TakeDamage(float damage)
    {
        if (playerMovement.isInvincible)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(ImInvincibleAdoOnePiece());
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
}
