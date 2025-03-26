using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAttackManager : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject player;
    public ChickenVisualHandler chickenAnimator;

    public bool canAttack = true;
    public bool isAttacking = false;
    public bool firstAttackPerformed = false;
    public bool counterAttacked = false;

    public float dashSpeed = 10f;
    public float waitingTime;
    public float attackCooldown;
    public float lastAttackTime;

    [Header("Private Variables")]
    private float timer;
    private float nextAttackTime;
    //private AttackType currentAttackType = AttackType.Attack1;

    [Header("References")]
    public ChickenStateManager csm;
    public ChickenMovement cm;
    public Rigidbody2D rb;
    public NBossHealth bh;

    [Header("Attack Object Hitboxes")]
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;

    private enum AttackType
    {
        //Attack1 will be the shoot
        Attack1,
        //Attack2 the dash
        Attack2,
        //Attack3 the rest period
        Attack3,
        //Attack4 the boom
        Attack4
    }

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        player = GameObject.FindGameObjectWithTag("Player");
        bh = GetComponent<NBossHealth>();
        rb = GetComponent<Rigidbody2D>();
        cm = GetComponent<ChickenMovement>();
        csm = GetComponent<ChickenStateManager>();
        chickenAnimator = GetComponentInChildren<ChickenVisualHandler>();
        //chickenAnimator.currentAnimator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update called");
        if (player == null || !canAttack)
            return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        //If within range, handle attack cycles
        if (distance < 3 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
        {
            canAttack = true;
            Debug.Log("Distance Check working");
            AttackCycle(distance);
        }
    }

    private void AttackCycle(float distance)
    {
        if (csm.currentStateName == "ChickenHealthyState")
        {
            Debug.Log("Successfully checked state");
            /*PerformAttack(AttackType.Attack1);
            Debug.Log("Attacking working");
            PerformAttack(AttackType.Attack4);
            PerformAttack(AttackType.Attack2);
            PerformAttack(AttackType.Attack3);*/
            
            /*if (!counterAttacked)
            {
                PerformAttack(AttackType.Attack1);
                if (distance < 1f) //if too close, switch to Attack4
                {
                    PerformAttack(AttackType.Attack4);
                    counterAttacked = true; //flag to prevent repeating Attack4
                }
                else
                {
                    PerformAttack(AttackType.Attack2);
                    PerformAttack(AttackType.Attack3);
                }
            }
            else
            {
                //return to regular attack cycle
                counterAttacked = false;
                PerformAttack(AttackType.Attack2);
                PerformAttack(AttackType.Attack3);
            }*/
        }
        else if (csm.currentStateName == "ChickenLightDamage")
        {
            PerformAttack(AttackType.Attack1);
            PerformAttack(AttackType.Attack4);
            PerformAttack(AttackType.Attack2);
            PerformAttack(AttackType.Attack3);
        }

        // Update last attack time after the cycle is handled
        lastAttackTime = Time.time;
    }

    private IEnumerator PerformAttack(AttackType attack)
    {
        if (Time.time - lastAttackTime < attackCooldown)
        {
            yield break;
        }
        isAttacking = true;
        switch (attack)
        {
            case AttackType.Attack1:
                Debug.Log("Attack1");
                Attack1();
                break;
            case AttackType.Attack2:
                Debug.Log("Attack2");
                Attack2();
                break;
            case AttackType.Attack3:
                Debug.Log("Attack3");
                Attack3();
                break;
            case AttackType.Attack4:
                Debug.Log("Attack4");
                Attack4();
                break;
        }
        yield return new WaitForSeconds(waitingTime);
        //nextAttackTime = Time.time + attackCooldown;
        isAttacking = false;
    }

    private void Attack1()
    {
        StartCoroutine(Shoot());
    }
    private void Attack2()
    {
        StartCoroutine(Dash());
    }
    private void Attack3()
    {
        StartCoroutine(Explode());
    }
    private void Attack4()
    {
    }
    private IEnumerator Shoot()  
    {
        chickenAnimator.currentAnimator.SetBool("idle", true);
        /*if (sound != null)
        {
            audioSource.PlayOneShot(pew);
        }*/
        //popcorn.SetActive(true);
        cm.speed = 0f;
        isAttacking = true;
        //hissTime = 1f;
        waitingTime = 2f;
        //yield return new WaitForSeconds(hissTime);
        attack1.SetActive(true);
        yield return new WaitForSeconds(waitingTime);
        attack1.SetActive(false);
        //popcorn.SetActive(false);
        cm.speed = 1f;
        chickenAnimator.currentAnimator.SetBool("idle", false);
        attack1.SetActive(false);
        isAttacking = false;
    }

    private IEnumerator Dash()
    {
        chickenAnimator.currentAnimator.SetBool("anticipation", true);
        waitingTime = 1.5f;
        cm.speed = 0f;
        yield return new WaitForSeconds(waitingTime);
        chickenAnimator.currentAnimator.SetBool("anticipation", false);

        bh.isInvincible = true;
        //gets players location and plays dash anim
        chickenAnimator.currentAnimator.SetBool("dash", true);
        Vector3 direction = (player.transform.position - transform.position).normalized;

        //dashes towards the player
        attack2.SetActive(true);
        isAttacking = true;
        float dashDuration = 0.4f;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            transform.position += direction * dashSpeed * Time.deltaTime;
            yield return null;
        }
        chickenAnimator.currentAnimator.SetBool("dash", false);
        chickenAnimator.currentAnimator.SetBool("anticipation", false);
        cm.speed = 1f;
        attack2.SetActive(false);
        bh.isInvincible = false;
        isAttacking = false;
    }

    private IEnumerator Explode()
    {
        chickenAnimator.currentAnimator.SetBool("roar", true);
        /*if (sound != null)
        {
            audioSource.PlayOneShot(sound);
        }*/
        cm.speed = 0f;
        isAttacking = true;
        //hissTime = 2f;
        waitingTime = 0.3f;
        rb.mass = 900;
        //yield return new WaitForSeconds(hissTime);
        /*if (boom != null)
        {
            audioSource.PlayOneShot(boom);
        }*/
        attack3.SetActive(true);
        yield return new WaitForSeconds(waitingTime);
        attack1.SetActive(false);
        rb.mass = 100;
        cm.speed = 1f;
        chickenAnimator.currentAnimator.SetBool("roar", false);
        //isAttacking = false;
        attack3.SetActive(false);
        isAttacking = false;
    }
}
