using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //reguular variables
    public float speed;
    private Rigidbody2D rb;
    public Vector2 movementDir;
    private Vector2 lastMovementDR;

    //dodge variables
    private Vector3 dodgerollDir;
    private float dodgerollSpeed = 10f;
    private PlayerState state;

    //create a simple state handler
    public enum PlayerState
    {
        Normal,
        DodgeRolling,
    }

    private void Awake()
    {
        //deciding our first state
        state = PlayerState.Normal;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //make a switch case for normal and rolling for movement and the dodge roll movement
        switch (state)
        {
            case PlayerState.Normal:
                movementDir.x = Input.GetAxisRaw("Horizontal");
                movementDir.y = Input.GetAxisRaw("Vertical");
                movementDir = new Vector2(movementDir.x, movementDir.y).normalized;
                
                if (movementDir.x != 0 || movementDir.y != 0)
                {
                    lastMovementDR = movementDir;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    dodgerollDir = lastMovementDR;
                    dodgerollSpeed = 250f;
                    state = PlayerState.DodgeRolling;
                    Debug.Log("is rolling");
                }
                break;
            case PlayerState.DodgeRolling:

                float drsMultiplier = 5f;
                dodgerollSpeed -= dodgerollSpeed * drsMultiplier * Time.deltaTime;
                float drsminimum = 50f;

                if(dodgerollSpeed < drsminimum)
                {
                    state = PlayerState.Normal;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case PlayerState.DodgeRolling:
                rb.velocity = dodgerollDir * dodgerollSpeed;
            break;
            case PlayerState.Normal:
                rb.velocity = movementDir * speed;
                break;
        }

    }
}
