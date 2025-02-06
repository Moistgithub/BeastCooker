using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LobsterAttackManager : MonoBehaviour
{
    public GameObject player;
    public GameObject chicken;
    public bool isAttacking = false;
    //public bool attackComplete = true;
    public float attackDamage;
    public float dashSpeed = 10f;

    //Animator coz i gave up on the manager
    public Animator lobsterAnimator;

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
    public GameObject bubble;

    [SerializeField]
    public string currentAttackName;

    //public GameObject tentacle1;
    //public GameObject tentacle2;

    private bool firstAttackPerformed = false;

    public CinemachineImpulseSource impulseSource;

    public BossHealth bossHealth;

    public bool canAttack = true;

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
        lobsterAnimator = GetComponentInChildren<Animator>();
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
    /*void Update()
    {
        if (player == null || !canAttack)
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
        if(bossHealth.currentHealth == 15f)
        {
            bossHealth.triggerSpecialAttack = true;
        }
    }*/
    void Update()
    {
        if (player == null || !canAttack)
            return;
        /*if(spbub.hitByBubble == true)
        {
            //isAttacking = false;
        }*/
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 10)
        {
            timer += Time.deltaTime;

            if (distance < 5 && Time.time >= nextAttackTime && !isAttacking)
            {
                AttackType attack;

                //check if the special attack should be triggered
                if (bossHealth.triggerSpecialAttack)
                {
                    //special attack is guaranteed if triggerSpecialAttack is true
                    attack = AttackType.SpecialAttack;
                }
                else
                {
                    //ELSEselect a random attack or perform Attack1
                    attack = firstAttackPerformed ? ChooseRandomAttack() : AttackType.Attack1;
                }

                StartCoroutine(PerformAttack(attack));

                //after the first attack is performed, set firstAttackPerformed to true
                if (!firstAttackPerformed)
                {
                    firstAttackPerformed = true;
                }

                nextAttackTime = Time.time + attackCooldown;

                //reset special attack flag after performing it
                if (bossHealth.triggerSpecialAttack)
                {
                    bossHealth.triggerSpecialAttack = false;
                }
            }
        }

        //trigger the special attack when health reaches a specific threshold
        if (bossHealth.currentHealth == 15f)
        {
            bossHealth.triggerSpecialAttack = true;
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
        //attackComplete = false;
        switch (attack)
        {
            case AttackType.Attack1:
                currentAttackName = "Attack1";
                Attack1();
                break;
            case AttackType.Attack2:
                currentAttackName = "Attack2";
                Attack2();
                break;
            case AttackType.Attack3:
                currentAttackName = "Attack3";
                Attack3();
                break;
            case AttackType.SpecialAttack:
                currentAttackName = "Special";
                SpecialAttack();
                break;
        }
        //nextAttackTime = Time.time + attackCooldown;
        //isAttacking = false;
        //attackComplete = true;
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
        lobsterAnimator.SetBool("Special", true);
        isAttacking = true;
        yield return new WaitForSeconds(1f);
        bubble.SetActive(true);
        bossHealth.isInvincible = true;
    }
    private IEnumerator Idle()
    {
        isAttacking = true;
        waitingTime = 1.2f;
        yield return new WaitForSeconds(waitingTime);
        isAttacking = false;
    }

    private IEnumerator Slash()
    {
        waitingTime = 0.3f;
        lobsterAnimator.SetBool("Attack1", true);
        isAttacking = true;
        yield return new WaitForSeconds(2.8f);
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
        lobsterAnimator.SetBool("Attack1", false);
        Dissapear(enemyattackPoint1);
    }

    private IEnumerator PewPew()
    {
        isAttacking = true;
        hissTime = 1f;
        waitingTime = 2f;
        lobsterAnimator.SetBool("Attack2", true);
        yield return new WaitForSeconds(hissTime);
        enemyattackPoint2.SetActive(true);
        enemyattackPoint3.SetActive(true);
        yield return new WaitForSeconds(waitingTime);
        Debug.Log("attackone");
        //isAttacking = false;
        Dissapear(enemyattackPoint2);
        Dissapear(enemyattackPoint3);
        lobsterAnimator.SetBool("Attack2", false);
    }
}
