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

    public float waitingTime;
    public float dashSpeed = 10f;
    public float attackCooldown;
    public float lastAttackTime;
    public float waitTimer;
    public float dashPoseTime;

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


    [Header("Attack Timing Variables")]
    public float hissTime;

    [Header("Sound Effects")]
    private AudioSource audioSource;
    public AudioClip explosionNoise;
    public AudioClip chickenRoar;
    public AudioClip flapping;
    public AudioClip predash;
    public AudioClip dash;
    public AudioClip hissSound;

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
            /*if (distance >= 2.4 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("attack 2");
                // canAttack = true;
                StartCoroutine(PerformAttack(AttackType.Attack2));
            }*/
            if (distance >= 1.8  && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("attack 1");
                // canAttack = true;
                StartCoroutine(PerformAttack(AttackType.Attack1));
            }
            else if (distance <= 1.7 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
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
        else if (csm.currentStateName == "ChickenLightDamage")
        {
            attackCooldown = 1.25f;
            waitTimer = 1.5f;
            hissTime = 1.5f;
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance >= 1.8 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("attack 3");
                // canAttack = true;
                StartCoroutine(PerformAttack(AttackType.Attack3));
            }
            else if (distance <= 1.7 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("attack 2");
                // canAttack = true;
                StartCoroutine(PerformAttack(AttackType.Attack2));
            }
            else
            {
                Debug.Log("no attack");
            }
        }
        else if (csm.currentStateName == "ChickenHeavyDamage")
        {
            attackCooldown = 1f;
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance >= 1.8 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("attack 2");
                // canAttack = true;
                StartCoroutine(PerformAttack(AttackType.Attack2));
            }
            else if (distance <= 1.7 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
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
            isAttacking = false;
            Debug.Log("chicken is doomed pray for him");
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
        audioSource = GetComponent<AudioSource>();
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
        Debug.Log("currentstate " + currentState);
        if (currentState == CurrentMiniState.canAttack)
        {
            //currentState = CurrentMiniState.cantAttack;
            //Debug.Log("do it");
            StateChecker(currentState);
            shouldCheckAttack = false;
        }
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

        cm.speed = 0f;
        //chickenAnimator.currentAnimator.SetBool("idle", true);
        chickenAnimator.currentAnimator.SetBool("flap", true);
        if (chickenRoar != null)
        {
            audioSource.PlayOneShot(chickenRoar);
        }
        if (flapping != null)
        {
            audioSource.PlayOneShot(flapping);
        }
        //popcorn.SetActive(true);
        isAttacking = true;
        //hissTime = 1f;
        waitingTime = 2f;
        //yield return new WaitForSeconds(hissTime);
        attack1.SetActive(true);
        yield return new WaitForSeconds(waitingTime);
        attack1.SetActive(false);
        //popcorn.SetActive(false);
        cm.speed = 1f;
        chickenAnimator.currentAnimator.SetBool("flap", false);
        //chickenAnimator.currentAnimator.SetBool("idle", false);
        attack1.SetActive(false);
        StartCoroutine(WaitTimer());
        isAttacking = false;
        //currentState = CurrentMiniState.canAttack;
    }

    private IEnumerator Dash()
    {
        chickenAnimator.currentAnimator.SetBool("anticipation", true);
        waitingTime = 3;
        cm.speed = 0f;
        yield return new WaitForSeconds(waitingTime);
        chickenAnimator.currentAnimator.SetBool("anticipation", false);
        chickenAnimator.currentAnimator.SetTrigger("pose");
        bh.isInvincible = true;
        yield return new WaitForSeconds(dashPoseTime);
        if (predash != null)
        {
            audioSource.PlayOneShot(predash);
        }
        if (dash != null)
        {
            audioSource.PlayOneShot(dash);
        }
        waitingTime = 1;
        //yield return new WaitForSeconds(0.2f);
        //gets players location and plays dash anim
        chickenAnimator.currentAnimator.SetBool("dash", true);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        //dashes towards the player
        attack2.SetActive(true);
        isAttacking = true;
        float dashDuration = 0.6f;
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
        StartCoroutine(WaitTimer());
        isAttacking = false;
        //yield return new WaitForSeconds(4f);
    }

    private IEnumerator Explode()
    {
        //waitTimer = 3f;
        Debug.Log("exploding " + currentState);
        chickenAnimator.currentAnimator.SetBool("roar", true);
        if (chickenRoar != null)
        {
            audioSource.PlayOneShot(chickenRoar);
        }

        if (hissSound != null)
        {
            audioSource.PlayOneShot(hissSound);
        }

        cm.speed = 0f;
        isAttacking = true;
        //hissTime = 2f;
        waitingTime = 0.3f;
        rb.mass = 5000;
        yield return new WaitForSeconds(hissTime);
        /*if (boom != null)
        {
            audioSource.PlayOneShot(boom);
        }*/
        attack3.SetActive(true);

        if (explosionNoise != null)
        {
            audioSource.PlayOneShot(explosionNoise);
        }

        yield return new WaitForSeconds(waitingTime);
        attack1.SetActive(false);
        rb.mass = 125;
        cm.speed = 1f;
        chickenAnimator.currentAnimator.SetBool("roar", false);
        //isAttacking = false;
        attack3.SetActive(false);
        Debug.Log("Endsplosion" + currentState);
        StartCoroutine(WaitTimer());
        isAttacking = false;
        //currentState = CurrentMiniState.canAttack;
    }

}

