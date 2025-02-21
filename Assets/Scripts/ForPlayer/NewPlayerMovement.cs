using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class NewPlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float playerSpeed;
    public AnimationCurve movementCurve;
    public Animator animator;
    //public float time;
    public float smoothbetweenTime;
    public SpriteRenderer spriteRenderer;

    private Vector2 movementInput;
    private Vector2 smoothmovementInput;
    private Vector2 smoothmovementVelocity;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();    
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        smoothmovementInput = Vector2.SmoothDamp(
            smoothmovementInput, movementInput,
            ref smoothmovementVelocity,
            movementCurve.Evaluate(smoothbetweenTime));
        rigidBody.velocity = smoothmovementInput * playerSpeed;

        if (movementInput.magnitude > 0.1f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
        animator.SetBool("IsRolling", false);
    }
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
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
        //FlipSpriteBasedOnMouse();
    }
}
