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
    public float dodgerollSpeed;
    private float dodgerollDuration = 0.3f;
    private float dodgeRollEndTime;
    private float dodgeRollCooldown = 1.75f;
    private float lastDodgeRollTime;
    private bool canRoll = true;
    public bool isInvincible = false;

    private PlayerState state;
    public Animator animator;

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

                animator.SetFloat("Horizontal", movementDir.x);
                animator.SetFloat("Vertical", movementDir.y);
                animator.SetFloat("Speed", movementDir.sqrMagnitude);

                //made it normalized here
                movementDir = new Vector2(movementDir.x, movementDir.y).normalized;
                
                //if the player isn't moving at all
                if (movementDir.x != 0 || movementDir.y != 0)
                {
                    //ensures that player can still roll
                    lastMovementDR = movementDir;
                }

                if (Input.GetMouseButtonDown(1) && canRoll)
                {
                    //yeaaa no mouse dodgeroll is not good ngl
                    //get cursor position and calculates dodge roll towards it
                    //Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //mouseWorldPosition.z = 0f;
                    //dodgerollDir = (mouseWorldPosition - transform.position).normalized;

                    dodgerollDir = lastMovementDR;
                    dodgeRollEndTime = Time.time + dodgerollDuration;
                    lastDodgeRollTime = Time.time;
                    state = PlayerState.DodgeRolling;
                    canRoll = false;
                    //Debug.Log("is rolling");
                    isInvincible = true;
                }
                break;

            case PlayerState.DodgeRolling:
                //if the time starting from when the game starts is greater and equal to the dodge roll end time player stops rolling state
                if (Time.time >= dodgeRollEndTime)
                {
                    state = PlayerState.Normal;
                    isInvincible = false;
                }
                break;
        }
        if(!canRoll && Time.time >= lastDodgeRollTime + dodgeRollCooldown)
        {
            canRoll = true;
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
