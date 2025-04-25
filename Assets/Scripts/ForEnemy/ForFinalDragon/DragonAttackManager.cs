using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackManager : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject player;
    public DragonVisualHandler dragonAnimator;
    public Transform dragonPoint;

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

    [Header("References")]
    public DragonStateManager dsm;
    public NBossHealth bh;

    [Header("Attack Object Hitboxes")]
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;
    public GameObject attack4;

    [Header("Attack Timing Variables")]
    public float hissTime;

    [Header("Sound Effects")]
    private AudioSource audioSource;
    public AudioClip charge;
    public AudioClip slash;
    public AudioClip roar;
    public AudioClip roar2;


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
        Attack4,
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
        if (dsm.currentStateName == "DragonHealthyState")
        {
            Debug.Log("Attack chceking healthy state");
            float distance = Vector2.Distance(dragonPoint.position, player.transform.position);

            if (distance <= 1.25 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {

            }
            else if (distance > 1.25 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {

            }
            else
            {
                Debug.Log("no attack");
            }
        }
        else if (dsm.currentStateName == "DragonDamagedAState")
        {
            waitTimer = 1f;
            float distance = Vector2.Distance(dragonPoint.position, player.transform.position);
            if (distance <= 2.2 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {

            }
            else if (distance >= 2.21 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {

            }
            else
            {
                Debug.Log("no attack");
            }
        }
        else if (dsm.currentStateName == "DragonDamagedBState")
        {
            waitTimer = 1f;
            float distance = Vector2.Distance(dragonPoint.position, player.transform.position);
            if (distance <= 2.2 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {

            }
            else if (distance >= 2.21 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {

            }
            else
            {
                Debug.Log("no attack");
            }
        }
        else
        {
            isAttacking = false;
            Debug.Log("lobster is doomed pray for him");
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
        dsm = GetComponent<DragonStateManager>();
        dragonAnimator = GetComponent<DragonVisualHandler>();
        //chickenAnimator.currentAnimator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(lobsterPoint.position, player.transform.position, Color.red);
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

    private void Attack1()
    {
    }
    private void Attack2()
    {
    }
    private void Attack3()
    {
    }
    private void Attack4()
    {
    }
    private IEnumerator Slash()
    {
        /*lobsterAnimator.currentAnimator.SetTrigger("Slash");
        if (audioSource != null)
        {
            audioSource.PlayOneShot(charge);
        }
        yield return new WaitForSeconds(1.2f);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(slash);
        }
        attack1.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attack1.SetActive(false);
        StartCoroutine(WaitTimer());
        isAttacking = false;*/
        yield return new WaitForSeconds(0.2f);

    }
    private IEnumerator KillerQueen()
    {
        /*if (audioSource != null)
        {
            audioSource.PlayOneShot(roar);
        }
        lobsterAnimator.currentAnimator.SetBool("Spiky", true);
        yield return new WaitForSeconds(1.7f);
        attack2.SetActive(true);
        yield return new WaitForSeconds(4f);
        lobsterAnimator.currentAnimator.SetBool("Spiky", false);
        attack2.SetActive(false);
        StartCoroutine(WaitTimer());
        isAttacking = false;*/
        yield return new WaitForSeconds(0.2f);
    }
    private IEnumerator SpearsOfLobJustice()
    {
        /* if (audioSource != null)
        {
            int roarChoice = Random.Range(1, 3);

            if (roarChoice == 1)
            {
                audioSource.PlayOneShot(roar);
            }
            else
            {
                audioSource.PlayOneShot(roar2);
            }
        }
        lobsterAnimator.currentAnimator.SetBool("Thunder", true);
        attack3Light.SetActive(true);
        attack3.SetActive(true);
        yield return new WaitForSeconds(4f);
        lobsterAnimator.currentAnimator.SetBool("Thunder", false);
        attack3Light.SetActive(false);
        attack3.SetActive(false);
        StartCoroutine(WaitTimer());
        isAttacking = false;*/
        yield return new WaitForSeconds(0.2f);
    }
    private IEnumerator SlashSpike()
    {
        /*lobsterAnimator.currentAnimator.SetTrigger("Slash");
        attack3Light.SetActive(true);
        attack3fast.SetActive(true);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(charge);
        }
        yield return new WaitForSeconds(1.2f);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(slash);
        }
        attack1.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attack1.SetActive(false);
        attack3Light.SetActive(false);
        attack3fast.SetActive(false);
        StartCoroutine(WaitTimer());
        isAttacking = false;*/
        yield return new WaitForSeconds(0.2f);
    }
}
