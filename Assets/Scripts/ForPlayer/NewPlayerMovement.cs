using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class NewPlayerMovement : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidBody;
    public float playerSpeed;
    public float smoothbetweenTime;
    public float distanceBetweenImages;

    public Vector2 lastMoveDir;

    public bool isInvincible = false;

    public AnimationCurve movementCurve;
    public AnimationCurve dodgeCurve;
     
    public Animator animator;
    //public float time;

    private Vector2 movementInput;
    private Vector2 smoothmovementInput;
    private Vector2 smoothmovementVelocity;

    // Dodge roll variables
    public float dodgeRollCooldown;
    public Vector2 lastDodgeDir;
    public float dodgeRollSpeed;
    public float dodgeRollDuration;

    private Vector2 dodgeRollDir;
    private float dodgeRollEndTime;
    private bool canDodgeRoll = true;
    private float lastImageXpos;

    public PlayerHealth playerHealth;

    private enum PlayerState
    {
        Normal,
        DodgeRolling
    }

    private PlayerState currentState;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        animator.SetBool("IsRolling", false);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();    
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (playerHealth.isHurt)
            return;

        smoothmovementInput = Vector2.SmoothDamp(
            smoothmovementInput, movementInput,
            ref smoothmovementVelocity,
            movementCurve.Evaluate(smoothbetweenTime));
        rigidBody.velocity = smoothmovementInput * playerSpeed;
        switch (currentState)
        {
            case PlayerState.Normal:
                if (movementInput.magnitude > 0.1f)
                {
                    animator.SetBool("IsWalking", true);
                    animator.SetTrigger("Walking");
                }
                else
                {
                    animator.SetBool("IsWalking", false);
                }
                
                break;
                

            case PlayerState.DodgeRolling:
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRolling", true);

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();  //create an afterimage
                    lastImageXpos = transform.position.x;  //update the position where the last afterimage was created
                }

                if (Time.time >= dodgeRollEndTime)
                {
                    currentState = PlayerState.Normal;
                    ResetDodgeRollCooldown();
                    isInvincible = false;
                    animator.SetBool("IsRolling", false);
                }
                break;
        }
        if (currentState == PlayerState.DodgeRolling)
        {
            float dodgeRollSpeedFactor = dodgeCurve.Evaluate((Time.time - (dodgeRollEndTime - dodgeRollDuration)) / dodgeRollDuration);
            rigidBody.velocity = dodgeRollDir * dodgeRollSpeed * dodgeRollSpeedFactor;
        }
        if (!canDodgeRoll && Time.time >= dodgeRollEndTime + dodgeRollCooldown)
        {
            canDodgeRoll = true;
        }
    }
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
        if (movementInput.magnitude > 0.1f)
        {
            lastMoveDir = movementInput.normalized;
        }
    }
    private void FlipSpriteBasedOnMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mousePositionX = mouseWorldPosition.x;

        //flips when on left
        if (mousePositionX < transform.position.x && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
        }
        //flips sprite when on right
        else if (mousePositionX > transform.position.x && spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
        }
    }
    private void StartDodgeRoll()
    {
        //rolls when not moving to last movement made
        dodgeRollDir = (movementInput.magnitude > 0.1f) ? movementInput.normalized : lastDodgeDir;
        //default move left if no input
        if (dodgeRollDir.magnitude < 0.1f) dodgeRollDir = Vector2.left;

        lastDodgeDir = dodgeRollDir;
        //dodge = current direction movement
        //dodgeRollDir = movementInput.normalized;

        dodgeRollEndTime = Time.time + dodgeRollDuration;
        currentState = PlayerState.DodgeRolling;
        isInvincible = true;
        animator.SetBool("IsRolling", true);  // Trigger the roll animation
        canDodgeRoll = false;  // Disable further dodge rolls until cooldown
    }

    public void ResetDodgeRollCooldown()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerSpeed = movementCurve.Evaluate(time);
        //time += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        FlipSpriteBasedOnMouse();
        if (Input.GetMouseButtonDown(1) && canDodgeRoll)
        {
            {
                StartDodgeRoll();
            }
        }
    }
}
