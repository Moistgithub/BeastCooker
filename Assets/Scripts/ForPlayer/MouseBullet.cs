using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBullet : MonoBehaviour
{
    private GameObject enemy;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public float attackDamage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag("BreakableEnemy");
        Vector3 direction = enemy.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime;
        if(timer > 10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth something = collision.gameObject.GetComponent<EnemyHealth>();
        if (something == null)
            return;
        if (collision.gameObject.CompareTag("BreakableEnemy"))
        {
            something.TakeDamage(attackDamage);
            Destroy(gameObject);
        }
    }
}
