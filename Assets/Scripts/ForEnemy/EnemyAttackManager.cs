using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public GameObject player;
    public bool isAttacking = false;
    public float attackDamage;
    public float dashSpeed = 10f;
    public EnemyMovement enemyMovement;

    //Time based variables
    private float timer;
    public float attackCooldown = 30f;
    private float nextAttackTime = 3f;
    private Coroutine currentAttackCoroutine;
    public float waitingTime;

    //Enemy attack points
    public GameObject enemyattackPoint1;
    public GameObject enemyattackPoint2;
    public GameObject enemyattackPoint3;



    private enum AttackType
    {
        Attack1,
        Attack2,
        Attack3
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAttacking && collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth something = collision.gameObject.GetComponent<PlayerHealth>();
            if (something == null)
                return;
            something.TakeDamage(attackDamage);
            Debug.Log("player is hurt");
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

            if (distance < 5 && Time.time >= nextAttackTime)
            {
                AttackType attack = ChooseRandomAttack();
                StartCoroutine(PerformAttack(attack));
                nextAttackTime = Time.time + attackCooldown;
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
        }
        //nextAttackTime = Time.time + attackCooldown;
        //isAttacking = false;
    }

    private void Attack1()
    {
        enemyattackPoint1.SetActive(true);
        isAttacking = true;
        StartCoroutine(TimeHandler(enemyattackPoint1));
    }
    private void Attack2()
    {
        enemyattackPoint2.SetActive(true);
        isAttacking = true;
        StartCoroutine(TimeHandler(enemyattackPoint2));
    }
    private void Attack3()
    {
        StartCoroutine(DashAttack());
        StartCoroutine(TimeHandler(enemyattackPoint3));
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
        Debug.Log("Attack ended");
    }
    private IEnumerator DashAttack()
    {
        waitingTime = 0.4f;
        enemyMovement.speed = 0f;

        yield return new WaitForSeconds(waitingTime);
        Debug.Log("waiting" + waitingTime);

        //gets players location
        Vector3 direction = (player.transform.position - transform.position).normalized;
        //dashes towards the player
        enemyattackPoint3.SetActive(true);
        isAttacking = true;
        float dashDuration = 0.5f;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            transform.position += direction * dashSpeed * Time.deltaTime;
            yield return null;
        }

        enemyMovement.speed = 1f;
        Debug.Log("It is one");
        Dissapear(enemyattackPoint3);
    }
}
