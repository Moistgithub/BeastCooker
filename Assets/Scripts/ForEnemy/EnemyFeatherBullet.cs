using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFeatherBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public float attackDamage;
    private float lifetimer;
    public GameObject bullet;
    public GameObject mother;
    // Start is called before the first frame update
    void Start()
    {
        mother = GameObject.FindGameObjectWithTag("BreakableEnemy");
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        //float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerhealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerhealth != null)
            {
                playerhealth.TakeDamage(attackDamage);
                Destroy(gameObject);
            }

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
        if(mother == null)
        {
            Destroy(gameObject);
        }
    }
}
