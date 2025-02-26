using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //attacking variables
    PlayerMovement playermovement;
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

    //2nd attack variables
    private bool canPerformAttack2 = false;
    public float attack2Window;

    //item pickup and throw variables
    //public Transform holdingPoint;
    //public float throwForce = 20f;
    //private bool isHoldingItem = false;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("IsAttacking", false);
        playermovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Attack()
    {
        if (isAttacking || !canAttack)
            return;
        //to see if the cooldown has finished or not
        //if (!canAttack)
        //    return;
        if(c_HandleAttackDelay == null)
        {
            c_HandleAttackDelay = StartCoroutine(HandleAttackDelay());
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
        animator.SetBool("IsAttacking", true);

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
        //allows attack 2 window to start
        StartCoroutine(Attack2Window());

        StartCoroutine(TimeHandler());

        c_HandleAttackDelay = null;
    }

    private IEnumerator Attack2Window()
    {
        canPerformAttack2 = true;
        yield return new WaitForSeconds(attack2Window);
        canPerformAttack2 = false;
    }

    IEnumerator TimeHandler()
    {
        //Handles the countdown of 0.3 seconds for the attacks lifetime
        yield return new WaitForSeconds(attackTime);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("Attack2", false);
        Dissapear();
        //Debug.Log("Time " + attackTime);
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
        if (isAttacking && collision.CompareTag("Boss"))
        {
            BossHealth something = collision.gameObject.GetComponent<BossHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            HitStop.Instance.StopTime(0.1f);
            Debug.Log("damaging");

        }
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
            if (canPerformAttack2)
            {
                PerformAttack2();
            }
            Attack();
        }

        if (!canAttack && Time.time >= lastAttackTime + attackCooldown)
        {
            canAttack = true;
        }
        
    }
    private void PerformAttack2()
    {
        if (!canPerformAttack2) return;
        animator.SetBool("IsAttacking", false);
        animator.SetBool("Attack2", true);
        if (c_HandleAttackDelay == null)
        {
            c_HandleAttackDelay = StartCoroutine(HandleAttackDelay());
        }
        Debug.Log("Performed Attack 2");

        canPerformAttack2 = false;
    }
}
