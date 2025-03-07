using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AnimationCurve movementCurve;
    public float time;

    //reguular variables
    public float speed;
    private Rigidbody2D rb;
    public Vector2 movementDir;
    private Vector2 lastMovementDR;

    //dodge variables
    private Vector3 dodgerollDir;
    public float dodgerollSpeed;
    public float dodgerollDuration;
    private float dodgeRollEndTime;
    private float dodgeRollCooldown = 1.75f;
    private float lastDodgeRollTime;
    private bool canRoll = true;
    public bool isInvincible = false;
    private PlayerState state;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    public AudioClip sound;
    public bool pushed = false;
    public DialogueManager chatting;


    public bool canMove = true;
    //creates a simple state handler
    public enum PlayerState
    {
        Normal,
        DodgeRolling,
        Frozen,
    }

    private void Awake()
    {
        //deciding our first state
        state = PlayerState.Normal;
    }
    // Start is called before the first frame update
    void Start()
    {
        //speed = movementCurve.Evaluate(time);
        //time += Time.deltaTime;


        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        chatting = GetComponent<DialogueManager>();
        if (chatting == null)
        {
            //Debug.LogError("DialogueManager is not assigned to PlayerMovement!");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }
        if (DialogueManager.GetInstance().chat == true)
        {
            return;
        }
        /*if (DialogueManager.GetInstance().dialoguePlaying)
        {
            SetFrozenState(true);
        }
        if (!DialogueManager.GetInstance().dialoguePlaying)
        {
            SetFrozenState(false);
            canRoll = true;
        }
        */
        FlipSpriteBasedOnMouse();


        //make a switch case for normal and rolling for movement and the dodge roll movement
        switch (state)
        {
            case PlayerState.Normal:
                //uses horizontal and vertical axis' to get raw movement. its also normalized to avoid 1.4 speed in diagonals
                movementDir.x = Input.GetAxisRaw("Horizontal");
                movementDir.y = Input.GetAxisRaw("Vertical");

                //animator.SetFloat("Horizontal", movementDir.x);
                //animator.SetFloat("Vertical", movementDir.y);
                //animator.SetFloat("Speed", movementDir.sqrMagnitude);

                //made it normalized here
                movementDir = new Vector2(movementDir.x, movementDir.y).normalized;

                if (movementDir.x != 0 || movementDir.y != 0)
                {
                    animator.SetBool("IsWalking", true);
                    lastMovementDR = movementDir;
                }
                else
                {
                    animator.SetBool("IsWalking", false);
                }

                //if the player isn't moving at all
                //if (movementDir.x != 0 || movementDir.y != 0)
                //{
                //    //ensures that player can still roll
                //    lastMovementDR = movementDir;
                //}

                if (Input.GetMouseButtonDown(1) && canRoll)
                {
                    //yeaaa no mouse dodgeroll is not good ngl
                    //get cursor position and calculates dodge roll towards it
                    //Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //mouseWorldPosition.z = 0f;
                    //dodgerollDir = (mouseWorldPosition - transform.position).normalized;
                    if (sound != null)
                    {
                        audioSource.PlayOneShot(sound);
                    }
                    dodgerollDir = lastMovementDR;
                    dodgeRollEndTime = Time.time + dodgerollDuration;
                    lastDodgeRollTime = Time.time;
                    state = PlayerState.DodgeRolling;
                    canRoll = false;
                    //Debug.Log("is rolling");
                    animator.SetBool("IsRolling", true);
                    isInvincible = true;
                }
                else
                {
                    animator.SetBool("IsRolling", false);
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
            case PlayerState.Frozen:
                movementDir = Vector2.zero;
                animator.SetBool("IsFrozen", true);
                animator.SetBool("IsWalking", false);
                break;
        }
        if (!canRoll && Time.time >= lastDodgeRollTime + dodgeRollCooldown)
        {
            canRoll = true;
        }
    }

    void FixedUpdate()
    {
        if (pushed == true)
        {
            return;
        }
        switch (state)
        {
            case PlayerState.DodgeRolling:
                rb.velocity = dodgerollDir * dodgerollSpeed;
                break;
            case PlayerState.Normal:
                rb.velocity = movementDir * speed;
                break;
            case PlayerState.Frozen:
                rb.velocity = Vector2.zero;
                break;
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
    public void SetFrozenState(bool isFrozen)
    {
        if (isFrozen)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsFrozen", true);
            state = PlayerState.Frozen;
        }
        else
        {
            state = PlayerState.Normal;
        }
    }
    public void SetNormalState(bool notFrozen)
    {
        if (notFrozen)
        {
            state = PlayerState.Normal;
        }
    }
}