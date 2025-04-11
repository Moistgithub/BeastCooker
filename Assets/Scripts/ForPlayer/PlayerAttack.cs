using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //attacking variables
    public float attackDamage;
    public GameObject attackPoint;
    public float attackTime;
    private Vector3 lastAttackPosition;
    protected bool isAttacking = false;
    public float attackCooldown;
    private float lastAttackTime;
    public float attackDistance;
    public Animator animator;
    public bool canAttack = true;
    private AudioSource audioSource;
    public AudioClip sound;
    public float attackDelay;
    public float distanceBetweenImages;
    public float resetAttackStateTime;
    public float pushbackForce = 5f;

    private int attackStateIndex = 0;
    private float resetAttackIndexTimer = 0f;
    private float lastImageXpos;

    public Rigidbody2D rb;

    [Header("Reference")]
    public KillBoxEnemy kbe;

    // Start is called before the first frame update
    void Start()
    {
        attackStateIndex = 0;
        animator.SetBool("IsAttacking", false);
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Attack()
    {
        if (isAttacking || !canAttack)
            return;

        //uses modulos to toggle between 1 - 3
        attackStateIndex = (attackStateIndex + 1) % 3;
        //to see if the cooldown has finished or not
        //if (!canAttack)
        //    return;
        if (c_HandleAttackDelay == null)
        {
            c_HandleAttackDelay = StartCoroutine(HandleAttackDelay());
        }
        if (attackStateIndex == 0)
        {
            /*if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
            {
                GameObject afterImage = PlayerAfterImagePool.Instance.GetFromPool();
                afterImage.transform.position = transform.position;
                afterImage.transform.rotation = transform.rotation;

                PlayerAfterImage playerAfterImage = afterImage.GetComponent<PlayerAfterImage>();
                if (playerAfterImage != null)
                {
                    playerAfterImage.ActiveTime = attackTime;
                }
                lastImageXpos = transform.position.x;
            }*/
        }
    }

    private Coroutine c_HandleAttackDelay = null;
    private IEnumerator HandleAttackDelay()
    {
        // Wait for the specified delay before proceeding
        yield return new WaitForSeconds(attackDelay);
        //prevents multiple attack spam
        if (!canAttack) yield break;
        //get mouse position and set the attack point 
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector3 attackDirection = (mouseWorldPosition - transform.position).normalized;
        attackPoint.transform.position = transform.position + attackDirection * attackDistance;
        animator.SetBool("IsWalking", false);
        isAttacking = true;
        if (attackStateIndex == 0)
        {
            if (kbe != null)
            {
                kbe.hsDur = 0.25f;
            }
            animator.SetTrigger("Attack3");
        }
        else if (attackStateIndex == 1)
        {
            if (kbe != null)
            {
                kbe.hsDur = 0.02f;
            }
            animator.SetTrigger("Attack");
        }
        else if (attackStateIndex == 2)
        {
            animator.SetTrigger("Attack2");
        }
        animator.SetBool("IsAttacking", true);

        ApplyPushback();

        //play the attack sound
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
        lastAttackTime = Time.time;
        canAttack = false;

        yield return new WaitForSeconds(0.3f);
        //enable the attack point and start the attack animation
        attackPoint.SetActive(true);
        StartCoroutine(TimeHandler());

        c_HandleAttackDelay = null;
    }

    IEnumerator TimeHandler()
    {
        //Handles the countdown of 0.3 seconds for the attacks lifetime
        yield return new WaitForSeconds(attackTime);
        animator.SetBool("IsAttacking", false);
        Dissapear();
        //Debug.Log("Time " + attackTime);
    }
    //it doesnt pllay pushback... but it works?
    private void ApplyPushback()
    {
        //get mouse position and calculate attack direction
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        Vector3 attackDirection = (mouseWorldPosition - transform.position).normalized;

        //apply pushback in the opposite direction of the attack and invert dir
        Vector2 pushbackDirection = -new Vector2(attackDirection.x, attackDirection.y);
        rb.AddForce(pushbackDirection * pushbackForce, ForceMode2D.Impulse);
    }

    public void Dissapear()
    {
        //attack literally dies
        attackPoint.SetActive(false);
        isAttacking = false;
        //Debug.Log("Im not running");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isAttacking && collision.CompareTag("BreakableEnemy"))
        {
            EnemyHealth something = collision.gameObject.GetComponent<EnemyHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            HitStop.Instance.StopTime(0.1f);
            Debug.Log("damaging");

        }
        /*f (isAttacking && collision.CompareTag("Boss"))
        {
            NBossHealth something = collision.gameObject.GetComponent<NBossHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            HitStop.Instance.StopTime(0.1f);
            Debug.Log("damaging");

        }*/
        if (isAttacking && collision.CompareTag("Ingredient"))
        {
            IngredientHealth something = collision.gameObject.GetComponent<IngredientHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            Debug.Log("damaging");

        }
        if (isAttacking && collision.gameObject.CompareTag("Box"))
        {
            BoxHealth something = collision.gameObject.GetComponent<BoxHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            Debug.Log("Box is hurt");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            resetAttackIndexTimer = 0f;
        }

        resetAttackIndexTimer += Time.deltaTime;

        if (resetAttackIndexTimer >= resetAttackStateTime)
        {
            attackStateIndex = 0;
            resetAttackIndexTimer = 0f;
        }

        if (!canAttack && Time.time >= lastAttackTime + attackCooldown)
        {
            canAttack = true;
        }
        
    }
}
