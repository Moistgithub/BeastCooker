using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject meatPrefab;
    public GameObject breakableBodyPartA;
    public GameObject breakableBodyPartB;
    public GameObject eggSpawner;
    public GameObject player;
    public float pushBackForce = 100f;
    public float flashDuration;
    public GameObject chickenHurtHair;
    public GameObject chickenHurtBody;
    private Coroutine flashCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Flash();
        if (currentHealth == 30)
        {
            Destroy(breakableBodyPartA);
            if(breakableBodyPartA != true)
            {
                if (chickenHurtHair.activeSelf)
                {
                    chickenHurtHair.SetActive(false);
                }
            }
        }

        if (currentHealth == 10)
        {
            Destroy(breakableBodyPartB);
            eggSpawner.SetActive(true);
        }


        if (currentHealth <= 0)
        {
            eggSpawner.SetActive(false);
            DeathSpawn();
        }

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

        if (breakableBodyPartA != null)
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
}
