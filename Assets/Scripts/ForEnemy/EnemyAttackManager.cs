using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public GameObject player;
    public GameObject chicken;
    public bool isAttacking = false;
    public float attackDamage;
    public float dashSpeed = 10f;
    public ChickenMovement enemyMovement;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteRenderer2;
    //Time based variables
    private float timer;
    public float attackCooldown = 30f;
    private float nextAttackTime = 3f;
    //private Coroutine currentAttackCoroutine;
    public float waitingTime;

    //Enemy attack points
    public GameObject enemyattackPoint1;
    public GameObject enemyattackPoint2;
    public GameObject enemyattackPoint3;

    //sound based
    private AudioSource audioSource;
    public AudioClip sound;
    private bool firstAttackPerformed = false;

    public EnemyHealth currentEAnimator;

    //public Animator currentanimator;
    private enum AttackType
    {
        Attack1,
        Attack2,
        Attack3,
        Attack4
    }

    // Start is called before the first frame update
    void Start()
    {
        currentEAnimator = GetComponentInChildren<EnemyHealth>();
        //currentEAnimator.currentanimator.SetBool("anticipation", false);
        currentEAnimator.currentanimator.SetBool("dash", false);
        player = GameObject.FindGameObjectWithTag("Player");
        enemyMovement = GetComponent<ChickenMovement>();
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //spriteRenderer2 = transform.Find("Fluff").GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    //private void OnTriggerEnter2D(Collision2D collision)
    //{

    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth something = collision.gameObject.GetComponent<PlayerHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            Debug.Log("player is hurt");
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
        if (currentEAnimator == null)
            return;
        if (player == null)
            return;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 10)
        {
            timer += Time.deltaTime;

            if (distance < 5 && Time.time >= nextAttackTime && !isAttacking)
            {
                //to check if first attack is done and ensures its no.4
                AttackType attack = firstAttackPerformed ? ChooseRandomAttack() : AttackType.Attack4;
                //AttackType attack = ChooseRandomAttack();
                StartCoroutine(PerformAttack(attack));
                //to make chicken roar first
                if (!firstAttackPerformed)
                {
                    firstAttackPerformed = true;
                }
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
    private AttackType ChooseRandomAttack()
    {
        return (AttackType)Random.Range(0, 4);
    }

    private IEnumerator PerformAttack(AttackType attack)
    {
        if (isAttacking) yield break;

        isAttacking = true;
        switch (attack)
        {
            case AttackType.Attack1:
                Attack1();
                break;
            case AttackType.Attack2:
                Attack2();
                break;
            case AttackType.Attack3:
                Attack3();
                break;
            case AttackType.Attack4:
                Attack4();
                break;
        }
        //nextAttackTime = Time.time + attackCooldown;
        //isAttacking = false;
    }

    private void Attack1()
    {
        StartCoroutine(BoomBoom());
        Debug.Log("attackone");
    }
    private void Attack2()
    {
        enemyattackPoint2.SetActive(true);

        isAttacking = true;
        StartCoroutine(TimeHandler(enemyattackPoint2));
        Debug.Log("attacktwo");
    }
    private void Attack3()
    {
        StartCoroutine(DashAttack());
        StartCoroutine(TimeHandler(enemyattackPoint3));
        Debug.Log("attackthree");
    }
    private void Attack4()
    {
        StartCoroutine(DoTheRoar());
        Debug.Log("attackfour");
    }
    IEnumerator TimeHandler(GameObject attackpoint)
    {
        //Handles the countdown of 0.3 seconds for the attacks lifetime
        yield return new WaitForSeconds(2f);
        Dissapear(attackpoint);

    }
    public void Dissapear(GameObject atkpoint)
    {
        //attack literally dies
        atkpoint.SetActive(false);
        isAttacking = false;
    }
    private IEnumerator DashAttack()
    {
        currentEAnimator.currentanimator.SetBool("anticipation", true);
        waitingTime = 1.5f;
        enemyMovement.speed = 0f;
        yield return new WaitForSeconds(waitingTime);
        currentEAnimator.currentanimator.SetBool("anticipation", false);
        //Debug.Log("waiting" + waitingTime);

        //gets players location
        currentEAnimator.currentanimator.SetBool("dash", true);
        Vector3 direction = (player.transform.position - transform.position).normalized;

        //dashes towards the player
        enemyattackPoint3.SetActive(true);
        isAttacking = true;
        float dashDuration = 0.4f;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            transform.position += direction * dashSpeed * Time.deltaTime;
            yield return null;
        }
        currentEAnimator.currentanimator.SetBool("dash", false);
        enemyMovement.speed = 1f;
        Dissapear(enemyattackPoint3);
    }
    private IEnumerator DoTheRoar()
    {
        currentEAnimator.currentanimator.SetBool("roar", true);
        isAttacking = true;
        waitingTime = 2f;
        enemyMovement.speed = 0f;
        //playes the sound
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
        yield return new WaitForSeconds(waitingTime);
        currentEAnimator.currentanimator.SetBool("roar", false);
        enemyMovement.speed = 1f;
        isAttacking = false;
    }
    private IEnumerator BoomBoom()
    {
        currentEAnimator.currentanimator.SetBool("idle", true);
        enemyMovement.speed = 0f;
        waitingTime = 0.3f;
        enemyattackPoint1.SetActive(true);
        isAttacking = true;
        yield return new WaitForSeconds(waitingTime);
        enemyattackPoint1.SetActive(false);
        enemyMovement.speed = 1f;
        Debug.Log("attackone");
        currentEAnimator.currentanimator.SetBool("idle", false);
        isAttacking = false;
    }
}
