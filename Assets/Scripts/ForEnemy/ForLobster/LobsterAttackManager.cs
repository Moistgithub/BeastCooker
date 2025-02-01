using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LobsterAttackManager : MonoBehaviour
{
    public GameObject player;
    public GameObject chicken;
    public bool isAttacking = false;
    public float attackDamage;
    public float dashSpeed = 10f;

    //Time based variables
    private float timer;
    public float attackCooldown;
    private float nextAttackTime;

    //private Coroutine currentAttackCoroutine;
    private float waitingTime;
    public float hissTime;

    //Enemy attack points
    public GameObject enemyattackPoint1;
    public GameObject enemyattackPoint2;
    public GameObject enemyattackPoint3;
    public GameObject enemyattackPoint4;
    public GameObject enemyattackPoint5;

    //public GameObject tentacle1;
    //public GameObject tentacle2;

    private bool firstAttackPerformed = false;

    public bool canAttack = false;
    public CinemachineImpulseSource impulseSource;

    public BossHealth bossHealth;

    private enum AttackType
    {
        Attack1,
        Attack2,
        Attack3,
        SpecialAttack,
    }

    // Start is called before the first frame update
    void Start()
    {
        if (impulseSource == null)
        {
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }
        player = GameObject.FindGameObjectWithTag("Player");
        bossHealth = GetComponent<BossHealth>();
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //spriteRenderer2 = transform.Find("Fluff").GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth something = collision.gameObject.GetComponent<PlayerHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            //Debug.Log("player is hurt");
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
        if (player == null)
            return;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 10)
        {
            timer += Time.deltaTime;

            if (distance < 5 && Time.time >= nextAttackTime && !isAttacking)
            {

                //checks if special can be used
                AttackType attack = bossHealth.triggerSpecialAttack ? AttackType.SpecialAttack : ( firstAttackPerformed ? ChooseRandomAttack() : AttackType.Attack1);
                //AttackType attack = ChooseRandomAttack();
                StartCoroutine(PerformAttack(attack));
                //to make chicken roar first
                if (!firstAttackPerformed)
                {
                    firstAttackPerformed = true;
                }
                nextAttackTime = Time.time + attackCooldown;
                // Reset special attack flag after performing it
                if (bossHealth.triggerSpecialAttack)
                {
                    bossHealth.triggerSpecialAttack = false;
                }
            }
        }
    }
    private AttackType ChooseRandomAttack()
    {
        return (AttackType)Random.Range(0, 3);
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
            case AttackType.SpecialAttack:
                SpecialAttack();
                break;
        }
        //nextAttackTime = Time.time + attackCooldown;
        //isAttacking = false;
    }

    private void Attack1()
    {
        StartCoroutine(Slash());
        Debug.Log("attackone");
    }
    private void Attack2()
    {
        StartCoroutine(PewPew());
        Debug.Log("attacktwo");
    }
    private void Attack3()
    {
        StartCoroutine(Idle());
        Debug.Log("Idle");
    }
    private void SpecialAttack()
    {
        Debug.Log("special");
        StartCoroutine(Desperation());
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
    private IEnumerator Desperation()
    {
        waitingTime = 15f;
        yield return new WaitForSeconds(waitingTime);
        enemyattackPoint2.SetActive(true);
        enemyattackPoint3.SetActive(true);
        enemyattackPoint4.SetActive(true);
        enemyattackPoint5.SetActive(true);

    }
    private IEnumerator Idle()
    {
        isAttacking = true;
        waitingTime = 2f;
        yield return new WaitForSeconds(waitingTime);
        isAttacking = false;
    }

    private IEnumerator Slash()
    {
        isAttacking = true;
        hissTime = 2f;
        waitingTime = 0.3f;
        yield return new WaitForSeconds(hissTime);
        //using vectors to cheat
        if (Vector2.Distance(transform.position, player.transform.position) < 1.3f)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Pain");
            }
        }
        enemyattackPoint1.SetActive(true);
        yield return new WaitForSeconds(waitingTime);
        enemyattackPoint1.SetActive(false);
        //isAttacking = false;
        Dissapear(enemyattackPoint1);
    }

    private IEnumerator PewPew()
    {
        isAttacking = true;
        hissTime = 1f;
        waitingTime = 2f;
        yield return new WaitForSeconds(hissTime);
        enemyattackPoint2.SetActive(true);
        enemyattackPoint3.SetActive(true);
        yield return new WaitForSeconds(waitingTime);
        Debug.Log("attackone");
        //isAttacking = false;
        Dissapear(enemyattackPoint2);
        Dissapear(enemyattackPoint3);
    }
}
