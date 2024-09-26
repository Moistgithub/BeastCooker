using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Children : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    public float speed;
    public float force;
    private float timer;
    public float attackDamage;
    public float rotateSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)player.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }
}
