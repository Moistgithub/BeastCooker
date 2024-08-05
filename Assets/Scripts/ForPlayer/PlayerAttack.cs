using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovement playermovement;
    public float attackDamage = 10f;
    public GameObject attackPoint;
    private float attackTime = 0.3f;
    private Vector3 lastAttackPosition;
    private bool isAttacking = false;
    private float attackCooldown = 0.75f;
    private float lastAttackTime;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
    }
    public void Attack()
    {
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
        //playermovement.movementDir;

        attackPoint.SetActive(true);
        isAttacking = true;
        StartCoroutine(TimeHandler());

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isAttacking && collision.gameObject.CompareTag("BreakableEnemy"))
        {
            EnemyHealth something = collision.gameObject.GetComponent<EnemyHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            //(gameObject);
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

        if (!canAttack && Time.time >= lastAttackTime + attackCooldown)
        {
            canAttack = true;
        }
    }
}
