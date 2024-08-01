using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    //to see how much the players position has changed
    public Vector2 movementDir;
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
        Flip();
    }
    void Flip()
    {
        if (movementDir.x > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Is Right");
        }
        else if (movementDir.x < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Debug.Log("Is left");
        }
        else if (movementDir.y > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Is up");
        }
        else if (movementDir.y < - 0.1f)
        {
            transform.localScale = new Vector3(1, -1, 1);
            Debug.Log("Is down");
        }
    }
    void FixedUpdate()
    {
        rb.velocity = movementDir * speed;
    }
}
