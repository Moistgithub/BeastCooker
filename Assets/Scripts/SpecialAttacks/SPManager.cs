using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPManager : MonoBehaviour
{
    public bool canSP = false;
    public float spDuration;
    public bool isSP = false;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public GameObject specialUI;
    public GameObject salt;
    // Start is called before the first frame update

    void Start()
    {
        playerMovement.GetComponent<PlayerMovement>();
        playerAttack.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private enum SPAttack
    {
        attack1,
    }

    private IEnumerator PerformAttack(SPAttack attack)
    {
        if (isSP) yield break;

        isSP = true;
        //attackComplete = false;
        switch (attack)
        {
            case SPAttack.attack1:
                Attack1();
                break;

        }
    }
    private void Attack1()
    {
        StartCoroutine(SaltSplash());
    }
    private IEnumerator SaltSplash()
    {
        playerMovement.canMove = false;
        playerMovement.speed = 0f;
        playerMovement.SetFrozenState(true);
        playerAttack.canAttack = false;
        playerAttack.enabled = false;
        specialUI.SetActive(true);
        salt.SetActive(true);
        yield return new WaitForSeconds(2.8f);
        canSP = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);

        canSP = true;
        if (other.CompareTag("Special") && canSP)
        {
            Debug.Log("sp time");
            canSP = false;
            StartCoroutine(PerformAttack(SPAttack.attack1));
        }
    }
}
