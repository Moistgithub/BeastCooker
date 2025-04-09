using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicklett : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public float attackDamage;
    private float lifetimer;
    public GameObject bullet;
    public GameObject mother;
    public float pushBackForce;
    public AudioClip sound;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        mother = GameObject.FindGameObjectWithTag("Boss");
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
        }

        //float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            ReverseBulletDirection();
            return;
        }
        /*if (collision.CompareTag("Player"))
        {
            PlayerHealth playerhealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerhealth != null)
            {
                playerhealth.TakeDamage(attackDamage);
                Destroy(gameObject);
            }

        }*/
        if (collision.gameObject.CompareTag("Box"))
        {
            BoxHealth something = collision.gameObject.GetComponent<BoxHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            Debug.Log("Box is hurt");
        }
        else if (ShouldIgnoreCollision(collision))
        {
            return;
        }
    }

    private bool ShouldIgnoreCollision(Collider2D collision)
    {
        return collision.GetComponent<EnemyHealth>() != null;
    }

    private void ReverseBulletDirection()
    {
        Vector3 direction = -player.transform.position + transform.position;
        Debug.Log("Bullet hit by player attack! Reversing direction.");
        rb.velocity = -rb.velocity;
        rb.AddForce(rb.velocity.normalized * pushBackForce, ForceMode2D.Impulse);
        StartCoroutine(SpinSpin());
    }

    private IEnumerator SpinSpin()
    {
        while(true)
        {
            transform.Rotate(0f, 0f, 360f * Time.deltaTime);
            yield return null; 
        }
    }
    // Update is called once per frame
    void Update()
    {
        //timer = Time.deltaTime;
        lifetimer += Time.deltaTime;
        //Debug.Log(Time.deltaTime);
        if (lifetimer > 5)
        {
            Destroy(gameObject);
        }
        if (mother == null)
        {
            Destroy(gameObject);
        }
    }
}
