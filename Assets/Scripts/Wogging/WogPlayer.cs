using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WogPlayer : MonoBehaviour
{
    public Vector2 movementDir;
    public Rigidbody2D rb;
    public float movespeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDir.x = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        rb.velocity = movementDir * movespeed;
    }
}
