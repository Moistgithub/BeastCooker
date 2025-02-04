using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBubble : MonoBehaviour
{

    public Vector3 maxSize = new Vector3(2f, 2f, 2f);
    public float growthSpeed;
    public string originalTag;
    public float attackDamage;
    public float speed;
    public bool willKillPlayer = false;

    private PlayerHealth playerHealth;
    private Vector3 originalSize;
    private Transform playerTransform;

    void Start()
    {
        //store original size of the object
        originalSize = transform.localScale;
        originalTag = gameObject.tag;
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
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
        if(willKillPlayer == true)
        {
            MoveTowardsPlayer();
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
    }
}
