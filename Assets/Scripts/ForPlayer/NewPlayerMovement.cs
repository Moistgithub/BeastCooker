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

    private Vector2 movementInput;
    private Vector2 smoothmovementInput;
    private Vector2 smoothmovementVelocity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsRolling", false);
        smoothmovementInput = Vector2.SmoothDamp(
            smoothmovementInput, movementInput,
            ref smoothmovementVelocity,
            movementCurve.Evaluate(smoothbetweenTime));
        rigidBody.velocity = smoothmovementInput * playerSpeed;
    }
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
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
        
    }
}
