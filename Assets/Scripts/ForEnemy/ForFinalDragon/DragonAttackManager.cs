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
    public EnemyShotSpawner ess;
    public GameObject attack2;
    public GameObject attack3;
    public GameObject attack4;
    public GameObject attack5;
    public GameObject waffleattack6;
    public GameObject waffleattack7;
    public GameObject attack7;
    public GameObject dragonLASER;
    public GameObject dragon;

    [Header("Attack Vector3")]
    public Vector3 flylocation;
    public Vector3 startpos;

    [Header("Attack Timing Variables")]
    public float hissTime;

    [Header("Sound Effects")]
    private AudioSource audioSource;
    //public AudioClip charge;
    //public AudioClip slash;
    public AudioClip roarWhite;
    public AudioClip roarBrown;
    public AudioClip roarPink;

    private int attackPatternIndex = 0;
    private AttackType[] healthyAttackPattern = new AttackType[] { AttackType.Attack1, AttackType.Attack2 };

    private AttackType[] damagedAttackPattern = new AttackType[] { AttackType.Attack3, AttackType.Attack4 };

    private AttackType[] damagedBttackPattern = new AttackType[] { AttackType.Attack5, AttackType.Attack6 };

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
        //Attack2 the
        Attack2,
        //Attack3 the 
        Attack3,
        //Attack4 the 
        Attack4,
        Attack5,
        Attack6,
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
            StartCoroutine(PerformAttack(healthyAttackPattern[attackPatternIndex]));
            attackPatternIndex = (attackPatternIndex + 1) % healthyAttackPattern.Length;
        }
        else if (dsm.currentStateName == "DragonDamagedAState")
        {
            StartCoroutine(PerformAttack(damagedAttackPattern[attackPatternIndex]));
            attackPatternIndex = (attackPatternIndex + 1) % healthyAttackPattern.Length;
        }
        else if (dsm.currentStateName == "DragonDamagedBState")
        {
            StartCoroutine(PerformAttack(damagedBttackPattern[attackPatternIndex]));
            attackPatternIndex = (attackPatternIndex + 1) % healthyAttackPattern.Length;
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
            case AttackType.Attack5:
                Debug.Log("Attack5");
                Attack5();
                break;
            case AttackType.Attack6:
                Debug.Log("Attack6");
                Attack6();
                break;
        }
        yield return new WaitForSeconds(attackCooldown);
        lastAttackTime = Time.time;
        isAttacking = false;
    }

    private void Attack1()
    {
        StartCoroutine(ChocoShot());
    }
    private void Attack2()
    {
        StartCoroutine(Waffles());
    }
    private void Attack3()
    {
        StartCoroutine(VanillaShot());
        //StartCoroutine(IceCreamShoot());
    }
    private void Attack4()
    {
        StartCoroutine(LASERS());
        //StartCoroutine(PinkDeath());
    }
    private void Attack5()
    {
        StartCoroutine(PinkDeath());
        //StartCoroutine(PinkDeath());
    }
    private void Attack6()
    {
        StartCoroutine(PinkErasure());
        //StartCoroutine(PinkDeath());
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
    private IEnumerator VanillaShot()
    {
        //dragonAnimator.currentAnimator.SetBool("Idle", false);
        dragonAnimator.currentAnimator.SetBool("Charge", true);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(roarWhite);
        }
        attack7.SetActive(true);
        yield return new WaitForSeconds(5f);
        attack7.SetActive(false);
        dragonAnimator.currentAnimator.SetBool("Charge", false);
        //dragonAnimator.currentAnimator.SetBool("Idle", true);
        if (ess != null)
        {
            ess.timer = 0;
        }
        StartCoroutine(WaitTimer());
        isAttacking = false;
    }

    private IEnumerator ChocoShot()
    {
        //dragonAnimator.currentAnimator.SetBool("Idle", false);
        dragonAnimator.currentAnimator.SetBool("Charge", true);
        if(audioSource != null)
        {
            audioSource.PlayOneShot(roarBrown);
        }
        attack1.SetActive(true);
        yield return new WaitForSeconds(5f);
        attack1.SetActive(false);

        dragonAnimator.currentAnimator.SetBool("Charge", false);
        //dragonAnimator.currentAnimator.SetBool("Idle", true);
        if (ess != null)
        {
            ess.timer = 0;
        }
        StartCoroutine(WaitTimer());
        isAttacking = false;
    }

    private IEnumerator PinkDeath()
    {
        //dragonAnimator.currentAnimator.SetBool("Idle", false);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(roarPink);
        }
        dragonAnimator.currentAnimator.SetBool("Charge", true);
        attack4.SetActive(true);
        yield return new WaitForSeconds(3f);
        attack4.SetActive(false);
        yield return new WaitForSeconds(1f);
        attack4.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        attack4.SetActive(false);

        dragonAnimator.currentAnimator.SetBool("Charge", false);
        dragonAnimator.currentAnimator.SetBool("Dizzy", true);

        yield return new WaitForSeconds(4f);


        dragonAnimator.currentAnimator.SetBool("Dizzy", false);
        //dragonAnimator.currentAnimator.SetBool("Idle", true);
        if (ess != null)
        {
            ess.timer = 0;
        }
        StartCoroutine(WaitTimer());
        isAttacking = false;
    }

    private IEnumerator Chomp()
    {
        //dragonAnimator.currentAnimator.SetBool("Idle", false);
        dragonAnimator.currentAnimator.SetBool("Roar", true);
        yield return new WaitForSeconds(1.5f);
        attack2.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attack2.SetActive(false);
        dragonAnimator.currentAnimator.SetBool("Roar", false);
        //dragonAnimator.currentAnimator.SetBool("Idle", true);
        StartCoroutine(WaitTimer());
        isAttacking = false;
    }
    private IEnumerator Waffles()
    {
        float duration = 1.5f;
        float elapsedTime = 0f;

        Vector3 dragonStart = dragon.transform.position;
        dragonAnimator.currentAnimator.SetBool("Roar", true);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(roarBrown);
        }

        yield return new WaitForSeconds(2f);

        dragonAnimator.currentAnimator.SetBool("Roar", false);

        while (elapsedTime < duration)
        {
            dragon.transform.position = Vector3.Lerp(dragonStart, flylocation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dragon.transform.position = flylocation;
        waffleattack6.SetActive(true);

        yield return new WaitForSeconds(6f);

        waffleattack6.SetActive(false);

        elapsedTime = 0f;
        Vector3 currentPos = dragon.transform.position;

        while (elapsedTime < duration)
        {
            dragon.transform.position = Vector3.Lerp(currentPos, startpos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dragon.transform.position = startpos;
        dragonAnimator.currentAnimator.SetBool("Dizzy", true);

        yield return new WaitForSeconds(4f);

        dragonAnimator.currentAnimator.SetBool("Dizzy", false);
        StartCoroutine(WaitTimer());
        isAttacking = false;
    }
    private IEnumerator PinkErasure()
    {
        float duration = 1.5f;
        float elapsedTime = 0f;
        if (audioSource != null)
        {
            audioSource.PlayOneShot(roarPink);
        }
        Vector3 dragonStart = dragon.transform.position;
        dragonAnimator.currentAnimator.SetBool("Roar", true);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(roarPink);
        }

        yield return new WaitForSeconds(2f);

        dragonAnimator.currentAnimator.SetBool("Roar", false);

        while (elapsedTime < duration)
        {
            dragon.transform.position = Vector3.Lerp(dragonStart, flylocation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dragon.transform.position = flylocation;
        waffleattack7.SetActive(true);


        yield return new WaitForSeconds(6f);
        waffleattack7.SetActive(false);

        elapsedTime = 0f;
        Vector3 currentPos = dragon.transform.position;

        while (elapsedTime < duration)
        {
            dragon.transform.position = Vector3.Lerp(currentPos, startpos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dragon.transform.position = startpos;
        dragonAnimator.currentAnimator.SetBool("Dizzy", true);

        yield return new WaitForSeconds(4f);

        dragonAnimator.currentAnimator.SetBool("Dizzy", false);
        StartCoroutine(WaitTimer());
        isAttacking = false;
    }
    private IEnumerator LASERS()
    {
        float duration = 1.5f;
        float elapsedTime = 0f;

        Vector3 dragonStart = dragon.transform.position;
        dragonAnimator.currentAnimator.SetBool("Roar", true);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(roarWhite);
        }

        yield return new WaitForSeconds(2f);

        dragonAnimator.currentAnimator.SetBool("Roar", false);

        while (elapsedTime < duration)
        {
            dragon.transform.position = Vector3.Lerp(dragonStart, flylocation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dragon.transform.position = flylocation;
        dragonLASER.SetActive(true);

        yield return new WaitForSeconds(15f);

        dragonLASER.SetActive(false);

        elapsedTime = 0f;
        Vector3 currentPos = dragon.transform.position;

        while (elapsedTime < duration)
        {
            dragon.transform.position = Vector3.Lerp(currentPos, startpos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dragon.transform.position = startpos;
        dragonAnimator.currentAnimator.SetBool("Dizzy", true);

        yield return new WaitForSeconds(4f);

        dragonAnimator.currentAnimator.SetBool("Dizzy", false);
        StartCoroutine(WaitTimer());
        isAttacking = false;
    }
}
