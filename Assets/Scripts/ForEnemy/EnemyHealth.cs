using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject wog;
    public float maxHealth;
    public float currentHealth;
    public GameObject meatPrefab;
    //public GameObject breakableBodyPartA;
    //public GameObject breakableBodyPartB;
    public GameObject eggSpawner;
    public GameObject player;
    public float pushBackForce;
    public float pushBackForce2;
    public float flashDuration;
    public GameObject chickenHurtHair;
    public GameObject chickenHurtBody;
    private Coroutine flashCoroutine;
    public float invincibilityDuration;
    private bool isInvincible = false;
    public ChickenMovement enemyMovement;
    public EnemyAttackManager enemyAttackManager;
    public float stunDuration;
    public Rigidbody2D rb;
    public GameObject chickenFull;
    public GameObject chickenHalf;
    public GameObject chickenNaked;

    public CinemachineImpulseSource impulseSource;

    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public float waitingtime;

    public GameObject itemGain;

    public Animator currentanimator;
    public Animator chickenfullanim;
    public Animator chickenfluffanim;
    public Animator chickennakedanim;
    public TheCrippler cripple;

    public GameObject egg;
    public GameObject fegg;
    public GameObject degg;
    // Start is called before the first frame update
    void Start()
    {
        if (chickenfullanim == null)
            return;
        if (chickenfluffanim == null)
            return;
        if (chickennakedanim == null)
            return;
        cripple = GetComponent<TheCrippler>();
        currentanimator = GetComponent<Animator>();
        currentanimator = chickenfullanim;
        enemyMovement = GetComponent<ChickenMovement>();
        enemyAttackManager = GetComponent<EnemyAttackManager>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;


        if (impulseSource == null)
        {
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }
    }
    public void TakeDamage(float damage)
    {
        if (isInvincible)
            return;

        currentHealth -= damage;
        Flash();
        //testing
        Vector2 pushDirection = (transform.position - player.transform.position).normalized;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(pushDirection * pushBackForce2, ForceMode2D.Impulse);
        }

        StartCoroutine(InvincibilityCooldown());

        //65
        if (currentHealth == 45)
        {
            Destroy(chickenFull);
            currentanimator = chickenfluffanim;
            chickenHalf.SetActive(true);
            if(chickenFull != true)
            {
                if (chickenHurtHair.activeSelf)
                {
                    chickenHurtHair.SetActive(false);
                }
            }
            StartCoroutine(StunChicken());
        }
        //30
        if (currentHealth == 25)
        {
            Destroy(chickenHalf);
            currentanimator = chickennakedanim;
            chickenNaked.SetActive(true);
            if (itemGain != null)
            {
                Instantiate(itemGain, transform.position, transform.rotation);
            }
            StartCoroutine(cripple.Crippling());
            StartCoroutine(EggHatchAnim());
           /* egg.SetActive(false);
            fegg.SetActive(true);  
            StartCoroutine(SwitcherooEgg());
            eggSpawner.SetActive(true);
           */
            StartCoroutine(StunChicken());
        }


        if (currentHealth <= 0)
        {
            eggSpawner.SetActive(false);
            fegg.SetActive(false);
            degg.SetActive(true);
            wog.SetActive(true);
            DeathSpawn();
        }

    }
    private IEnumerator StunChicken()
    {
        //testing cinemachine
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }


        Vector2 pushDirection = (transform.position - player.transform.position).normalized;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        }
        //attacks and speed disabled
        enemyMovement.speed = 0f;
        enemyAttackManager.isAttacking = true;
        yield return new WaitForSeconds(stunDuration);

        //speed reenabled
        enemyMovement.speed = 1f;
        enemyAttackManager.isAttacking = false;
    }      
    private IEnumerator EggHatchAnim()
    {
        StartCoroutine(SwitcherooEgg());
        eggSpawner.SetActive(true);
        yield return new WaitForSeconds(2f);
        egg.SetActive(false);
        fegg.SetActive(true);
    }
    private IEnumerator InvincibilityCooldown()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
    public void DeathSpawn()
    {
        if (meatPrefab != null)
        {
            Instantiate(meatPrefab, transform.position, transform.rotation);
            //meat spawn
        }
        Destroy(gameObject);
    }
    private void Flash()
    {
        chickenHurtBody.SetActive(true);

        if (chickenFull != null)
        {
            chickenHurtHair.SetActive(true);
        }
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(FlashEffect());
    }
    private IEnumerator FlashEffect()
    {
        yield return new WaitForSeconds(flashDuration);

        chickenHurtBody.SetActive(false);
        chickenHurtHair.SetActive(false);
    }
    private IEnumerator SwitcherooEgg()
    {
        CameraManager.SwitchCamera(cam2);
        waitingtime = 4f;
        yield return new WaitForSecondsRealtime(waitingtime);
        //HitStop.Instance.StopTime(1f);
        CameraManager.SwitchCamera(cam1);
    }
}
