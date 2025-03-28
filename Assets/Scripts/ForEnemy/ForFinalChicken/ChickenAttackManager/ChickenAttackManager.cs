using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public CurrentMiniState currentState;

    private Coroutine attacktimerCoroutine;
    private Coroutine attackCoroutine;

    public float dashSpeed = 10f;
    private float waitingTime;
    public float attackCooldown;
    public float lastAttackTime;
    public float waitTimer;

    [Header("Private Variables")]
    private float timer;
    private float nextAttackTime;
    public bool shouldCheckAttack = true;
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

    public enum CurrentMiniState
    {
        canAttack,
        Attacking,
        cantAttack
    }

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

    private void StateChecker(CurrentMiniState ministate)
    {
        Debug.Log("checking" + currentState);
        switch (ministate)
        {
            case CurrentMiniState.Attacking:
                break;
            case CurrentMiniState.cantAttack:
                StartCoroutine(MiniStateChanger());
                break;
                //Debug.Log("attacking");
            case CurrentMiniState.canAttack:
                AttackChecker();
                break;
        }
    }
    private void AttackChecker()
    {
        if (csm.currentStateName == "ChickenHealthyState")
        {
            Debug.Log("Attack chceking healthy state");
            float distance = Vector2.Distance(transform.position, player.transform.position);

            //Debug.Log($"{distance} {Time.time - lastAttackTime >= attackCooldown} {!isAttacking}");
            if (distance >= 5 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("attack 2");
                // canAttack = true;
                StartCoroutine(PerformAttack(AttackType.Attack2));
            }
            else if (distance >= 4  && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("attack 1");
                // canAttack = true;
                StartCoroutine(PerformAttack(AttackType.Attack1));
            }
            else if (distance <= 3 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("attack 3");
                // canAttack = true;
                StartCoroutine(PerformAttack(AttackType.Attack3));
            }
            else
            {
                Debug.Log("no attack");
            }
        }
        else
        {
            Debug.Log("not healthy");
            return;
        }
    }
    private IEnumerator MiniStateChanger()
    {
        yield return new WaitForSeconds(attackCooldown);

        currentState = CurrentMiniState.canAttack;
    }
    private IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(waitTimer);
        currentState = CurrentMiniState.canAttack;
    }


    // Start is called before the first frame update
    void Start()
    {
        //canAttack = true;
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
        //Debug.Log("Update called");
        if (player == null)// || !canAttack)
            return;

        float distance = Vector2.Distance(transform.position, player.transform.position);


        /*if (distance < 10 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
        {
            shouldCheckAttack = true;
        }*/
        //If within range, handle attack cycles
        /*if (distance < 3 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
        {
            canAttack = true;
            Debug.Log("Distance Check working");
            AttackCycle(distance);
        }*/
        Debug.Log("currentstate " + currentState);
        if (currentState == CurrentMiniState.canAttack)
        {
            //currentState = CurrentMiniState.cantAttack;
            //Debug.Log("do it");
            StateChecker(currentState);
            shouldCheckAttack = false;
        }
    }

    private void AttackCycle(float distance)
    {
        /*if (csm.currentStateName == "ChickenHealthyState")
        {

            //Debug.Log("Successfully checked state");   
            if (!counterAttacked)
            {

                if (attackCoroutine != null)
                {
                    attackCoroutine = StartCoroutine(PerformAttack(AttackType.Attack1));
                }


                if (distance < 1f) //if too close, switch to Attack4
                {
                    StartCoroutine(PerformAttack(AttackType.Attack4));
                    counterAttacked = true; //flag to prevent repeating Attack4
                }
                else
                {
                    StartCoroutine(PerformAttack(AttackType.Attack2));
                    //StartCoroutine(thewait(3f));
                    StartCoroutine(PerformAttack(AttackType.Attack3));
                }
            }
            else
            {
                //return to regular attack cycle
                counterAttacked = false;
                StartCoroutine(PerformAttack(AttackType.Attack2));
                StartCoroutine(PerformAttack(AttackType.Attack3));
            }
        }
        else if (csm.currentStateName == "ChickenLightDamage")
        {
            StartCoroutine(PerformAttack(AttackType.Attack1));
            StartCoroutine(PerformAttack(AttackType.Attack2));
            StartCoroutine(PerformAttack(AttackType.Attack3));
            StartCoroutine(PerformAttack(AttackType.Attack4));
        }

        // Update last attack time after the cycle is handled
        lastAttackTime = Time.time;
        */
    }


    private IEnumerator PerformAttack(AttackType attack)
    {
        currentState = CurrentMiniState.Attacking;
        if (Time.time - lastAttackTime < attackCooldown)
        {
            Debug.Log("cooldown functioning");
            yield break; 
        }

        isAttacking = true;
        switch (attack)
        {
            case AttackType.Attack1:
                Debug.Log("Attack1");
                Attack1();
                yield return new WaitForSeconds(2f);
                break;
            case AttackType.Attack2:
                Debug.Log("Attack2");
                Attack2();
                yield return new WaitForSeconds(5f);
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
        yield return new WaitForSeconds(attackCooldown);
        lastAttackTime = Time.time;
        isAttacking = false;
    }

    /*private IEnumerator thewait(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
    }*/
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
        //Debug.Log("Starting explosion");
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
        currentState = CurrentMiniState.canAttack;
    }

    private IEnumerator Dash()
    {
        chickenAnimator.currentAnimator.SetBool("anticipation", true);
        waitingTime = 3;
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
        yield return new WaitForSeconds(4f);
        currentState = CurrentMiniState.canAttack;
    }

    private IEnumerator Explode()
    {
        waitTimer = 3f;
        Debug.Log("exploding " + currentState);
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
        Debug.Log("Endsplosion" + currentState);
        isAttacking = false;
        StartCoroutine(WaitTimer());
        //currentState = CurrentMiniState.canAttack;
    }

}

