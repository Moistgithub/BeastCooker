using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBubble : MonoBehaviour
{
    public GameObject spawner;
    public float speed = 10f;    
    public float rotationSpeed = 10f;
    private Transform player;
    private Rigidbody2D rb;
    public AudioSource aus;
    public AudioClip sound;

    void Start()
    {
        if (aus != null)
        {
            aus.PlayOneShot(sound);
        }
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            if (spawner != null)
            {
                spawner.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.velocity = transform.forward * speed;
    }
}
