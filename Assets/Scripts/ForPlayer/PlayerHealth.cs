using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 3f;
    public float currentHealth;
    private PlayerMovement playerMovement;
    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
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

    }
}
