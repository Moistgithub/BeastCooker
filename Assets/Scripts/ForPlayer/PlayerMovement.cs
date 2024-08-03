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
    private float dodgerollDuration = 0.3f;
    private float dodgeRollEndTime;

    private PlayerState state;

    //creates a simple state handler
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
                //uses horizontal and vertical axis' to get raw movement. its also normalized to avoid 1.4 speed in diagonals
                movementDir.x = Input.GetAxisRaw("Horizontal");
                movementDir.y = Input.GetAxisRaw("Vertical");
                movementDir = new Vector2(movementDir.x, movementDir.y).normalized;
                
                //if the player isn't moving at all
                if (movementDir.x != 0 || movementDir.y != 0)
                {
                    //ensures that player can still roll
                    lastMovementDR = movementDir;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    dodgerollDir = lastMovementDR;
                    dodgerollSpeed = 10f;
                    dodgeRollEndTime = Time.time + dodgerollDuration;
                    state = PlayerState.DodgeRolling;
                    //Debug.Log("is rolling");
                }
                break;
            case PlayerState.DodgeRolling:
                //if the time starting from when the game starts is greater and equal to the dodge roll end time player stops rolling state
                if (Time.time >= dodgeRollEndTime)
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
