using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    //to see how much the players position has changed
    private Vector2 movementDir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDir.x = Input.GetAxisRaw("Horizontal");
        movementDir.y = Input.GetAxisRaw("Vertical");
        movementDir = new Vector2(movementDir.x, movementDir.y).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = movementDir * speed;
    }
}