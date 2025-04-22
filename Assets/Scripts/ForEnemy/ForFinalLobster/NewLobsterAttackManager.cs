using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLobsterAttackManager : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject player;
    public LobsterVisualHandler lobsterAnimator;
    public Transform lobsterPoint;

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
    public LobsterStateManager lsm;
    public NBossHealth bh;

    [Header("Attack Object Hitboxes")]
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3Light;
    public GameObject attack3;
    public GameObject attack3fast;
    public GameObject finalAttack;


    [Header("Attack Timing Variables")]
    public float hissTime;

    [Header("Sound Effects")]
    private AudioSource audioSource;
    public AudioClip charge;
    public AudioClip slash;


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
        //Attack5 spiky slash
        Attack5
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
        if (lsm.currentStateName == "LobsterHealthyState")
        {
            Debug.Log("Attack chceking healthy state");
            float distance = Vector2.Distance(lobsterPoint.position, player.transform.position);

            if (distance <= 1.25 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("attack 1");
                StartCoroutine(PerformAttack(AttackType.Attack1));
            }
            else if (distance > 1.25 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("attack 3");
                StartCoroutine(PerformAttack(AttackType.Attack3));
            }
            else
            {
                Debug.Log("no attack");
            }
        }
        else if (lsm.currentStateName == "LobsterDamagedAState")
        {
            waitTimer = 1f;
            float distance = Vector2.Distance(lobsterPoint.position, player.transform.position);
            if (distance <= 2.2 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("slash");
                StartCoroutine(PerformAttack(AttackType.Attack5));
            }
            else if (distance >= 2.21 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("Bubble");
                StartCoroutine(PerformAttack(AttackType.Attack2));
            }  
            else
            {
                Debug.Log("no attack");
            }
        }
        else if (lsm.currentStateName == "LobsterDamagedBState")
        {
            attackCooldown = 1f;
            float distance = Vector2.Distance(lobsterPoint.position, player.transform.position);
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
        else if (lsm.currentStateName == "LobsterDesperationState")
        {
            attackCooldown = 1f;
            float distance = Vector2.Distance(lobsterPoint.position, player.transform.position);
            if (distance >= 1.8 && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("attack 2");
                // canAttack = true;
                StartCoroutine(PerformAttack(AttackType.Attack2));
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
        lsm = GetComponent<LobsterStateManager>();
        lobsterAnimator = GetComponent<LobsterVisualHandler>();
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
            case AttackType.Attack5:
                Debug.Log("Attack5");
                Attack5();
                break;
        }
        yield return new WaitForSeconds(attackCooldown);
        lastAttackTime = Time.time;
        isAttacking = false;
    }

    private void Attack1()
    {
        StartCoroutine(Slash());
    }
    private void Attack2()
    {
        StartCoroutine(KillerQueen());
    }
    private void Attack3()
    {
        StartCoroutine(SpearsOfLobJustice());
    }
    private void Attack4()
    {
    }
    private void Attack5()
    {
        StartCoroutine(SlashSpike());
    }
    private IEnumerator Slash()
    {
        lobsterAnimator.currentAnimator.SetTrigger("Slash");
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
        isAttacking = false;

    }
    private IEnumerator KillerQueen()
    {
        lobsterAnimator.currentAnimator.SetBool("Spiky", true);
        attack2.SetActive(true);
        yield return new WaitForSeconds(4f);
        lobsterAnimator.currentAnimator.SetBool("Spiky", false);
        attack2.SetActive(false);
        StartCoroutine(WaitTimer());
        isAttacking = false;
    }
    private IEnumerator SpearsOfLobJustice()
    {
        lobsterAnimator.currentAnimator.SetBool("Thunder", true);
        attack3Light.SetActive(true);
        attack3.SetActive(true);
        yield return new WaitForSeconds(4f);
        lobsterAnimator.currentAnimator.SetBool("Thunder", false);
        attack3Light.SetActive(false);
        attack3.SetActive(false);
        StartCoroutine(WaitTimer());
        isAttacking = false;
    }
    private IEnumerator SlashSpike()
    {
        lobsterAnimator.currentAnimator.SetTrigger("Slash");
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
        isAttacking = false;

    }
}
