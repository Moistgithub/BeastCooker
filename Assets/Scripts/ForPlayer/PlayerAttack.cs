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
    private bool canAttack = true;
    private AudioSource audioSource;
    public AudioClip sound;
    public float attackDelay;

    //item pickup and throw variables
    public Transform holdingPoint;
    private GameObject heldItem;
    private Rigidbody2D heldItemRb;
    public float throwForce = 20f;
    private bool isHoldingItem = false;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("IsAttacking", false);
        playermovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Attack()
    {
        if (isAttacking)
            return;
        //stopping attacks from working if holding item or if cooldown hasnt finished
        if (isHoldingItem || !canAttack)
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
        if (isAttacking && collision.CompareTag("Ingredient"))
        {
            IngredientHealth something = collision.gameObject.GetComponent<IngredientHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            Debug.Log("damaging");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHoldingItem)
            {
                ThrowItem();
            }
            else
            {
                TryPickUp();
            }
        }

        if (!canAttack && Time.time >= lastAttackTime + attackCooldown)
        {
            canAttack = true;
        }
        
    }

    void TryPickUp()
    {
        //creates a circle with a radius that collides and checks for tags to see if you can pick up an item
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D item in items)
        {
            if (item.CompareTag("Pickup"))
            {
                PickUpItem(item.gameObject);
                break;
            }
        }
    }

    void PickUpItem(GameObject item)
    {
        heldItem = item;
        //gets the rigidbody for the variable
        heldItemRb = heldItem.GetComponent<Rigidbody2D>();
        BoxCollider2D collider2D = heldItem.GetComponent<BoxCollider2D>();

        if (heldItemRb != null)
        {
            //to disable the physics for the held item when carried
            heldItemRb.isKinematic = true;

        }

        collider2D.enabled = false;

        heldItem.transform.position = holdingPoint.position;
        heldItem.transform.parent = holdingPoint;
        isHoldingItem = true;
    }

    void ThrowItem()
    {
        BoxCollider2D collider2D = heldItem.GetComponent<BoxCollider2D>();
        if (heldItemRb == null)
            return;

        //re-enables physics
        heldItemRb.isKinematic = false;

        //throws item where player is facing
        heldItemRb.AddForce(transform.up * throwForce, ForceMode2D.Impulse);
        collider2D.enabled = true;

        heldItem.transform.parent = null;
        heldItem = null;
        heldItemRb = null;
        isHoldingItem = false;
    }
}
