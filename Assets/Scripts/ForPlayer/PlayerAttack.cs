using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //attacking variables
    PlayerMovement playermovement;
    public float attackDamage;
    public GameObject attackPoint;
    private float attackTime = 0.2f;
    private Vector3 lastAttackPosition;
    private bool isAttacking = false;
    private float attackCooldown = 0.75f;
    private float lastAttackTime;
    private bool canAttack = true;
    private AudioSource audioSource;
    public AudioClip sound;

    //item pickup and throw variables
    public Transform holdingPoint;
    private GameObject heldItem;
    private Rigidbody2D heldItemRb;
    public float throwForce = 20f;
    private bool isHoldingItem = false;


    // Start is called before the first frame update
    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Attack()
    {
        //stopping attacks from working if holding item
        if (isHoldingItem)
            return;
        //to see if the cooldown has finished or not
        if (!canAttack)
            return;
        //creates scenario where the player isnt moving but turns the attack direction to the last used
        if (playermovement.movementDir.x != 0 || playermovement.movementDir.y != 0)
        {
            lastAttackPosition = playermovement.movementDir;
        }
        //uses reference from player movement to change location of attack point

        attackPoint.transform.localPosition = lastAttackPosition; 

        attackPoint.SetActive(true);
        isAttacking = true;
        StartCoroutine(TimeHandler());

        //playes the sound
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
        //Debug.Log("Im running");
        lastAttackTime = Time.time;
        canAttack = false;
    }

    IEnumerator TimeHandler()
    {
        //Handles the countdown of 0.3 seconds for the attacks lifetime
        yield return new WaitForSeconds(attackTime);
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
