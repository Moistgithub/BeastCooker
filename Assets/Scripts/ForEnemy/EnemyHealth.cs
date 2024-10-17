using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject meatPrefab;
    public GameObject breakableBodyPartA;
    public GameObject breakableBodyPartB;
    public GameObject eggSpawner;
    public GameObject player;
    public float pushBackForce = 5f;
    public float flashDuration = 0.5f;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRenderer2;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer2 = transform.Find("WingSprite").GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth == 30)
        {
            Destroy(breakableBodyPartA);
            Flash();
        }

        if (currentHealth == 10)
        {
            Destroy(breakableBodyPartB);
            Flash();
            eggSpawner.SetActive(true);
        }


        if (currentHealth <= 0)
        {
            eggSpawner.SetActive(false);
            DeathSpawn();
        }

    }
    public void DeathSpawn()
    {
        if (meatPrefab != null)
        {
            Instantiate(meatPrefab, transform.position, transform.rotation);
            //meat spawn
        }
        Destroy(gameObject);
    }
    private void Flash()
    {
        //Vector3 pushDirection = (transform.position - player.transform.position).normalized;
        //GetComponent<Rigidbody2D>().AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        StartCoroutine(FlashYellow());
    }

    private IEnumerator FlashYellow()
    {
        spriteRenderer.color = Color.black;
        if (spriteRenderer2 != null)
        {
            spriteRenderer2.color = Color.black;
        }
        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = Color.white;
        if (spriteRenderer2 != null)
        {
            spriteRenderer2.color = Color.white;
        }
    }
}
