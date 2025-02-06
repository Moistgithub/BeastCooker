using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterAnimatorController : MonoBehaviour
{
    //other scripts
    public LobsterStateManager lobsterstateManager;
    public LobsterAttackManager lobsterattackManager;
    //public LobsterAttackManager lobsterattackManager;

    //tentacles
    public GameObject tentacleA;
    public GameObject tentacleB;
    public GameObject tentacleC;
    public GameObject tentacleD;
    public GameObject tentacleE;
    public GameObject tentacleF;
    //public CapsuleCollider2D lobsterColl;

    public GameObject lobsterSpecialTrigger;
    public GameObject lobsterSpecialRange;
    public bool nextState = false;

    //animator
    public Animator lobsterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        lobsterAnimator = GetComponentInChildren<Animator>();
        //lobsterAnimator = GetComponent<Animator>(); 
        lobsterstateManager = GetComponent<LobsterStateManager>();
        lobsterattackManager = GetComponent<LobsterAttackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (lobsterattackManager.attackComplete == true)
        {
            lobsterAnimator.SetBool("Attack1", false);
            lobsterAnimator.SetBool("Attack2", false);
            lobsterAnimator.SetBool("Idle", true);
        }*/
        if (lobsterstateManager.currentStateName == "LobsterHealthyState")
        {
            Debug.Log("it works");
        }
        if (lobsterstateManager.currentStateName == "LobsterDamagedAState")
        {
            lobsterAnimator.SetBool("dizzy", false);
            lobsterattackManager.canAttack = true;
            tentacleA.SetActive(false);
            tentacleB.SetActive(false);
            tentacleC.SetActive(false);
            tentacleD.SetActive(false);
            Debug.Log("its great!");

        }
        if (lobsterstateManager.currentStateName == "LobsterDamagedBState")
        {
            tentacleE.SetActive(false);
            tentacleF.SetActive(false);
            Debug.Log("its even greater!");
        }
        if (lobsterstateManager.currentStateName == "LobsterDizzyState")
        {
            lobsterAnimator.SetBool("dizzy", true);
            lobsterSpecialTrigger.SetActive(true);
            lobsterSpecialRange.SetActive(true);
        }
    }
    /*
    if(lobsterattackManager.currentAttackName == "Attack1")
    {
        lobsterAnimator.SetBool("Attack1", true);
        lobsterAnimator.SetBool("Idle", false);
    }
    if (lobsterattackManager.currentAttackName == "Attack2")
    {
        Debug.Log("Attack 2");
        lobsterAnimator.SetBool("Attack2", true);
        lobsterAnimator.SetBool("Idle", false);
    }
    if (lobsterattackManager.currentAttackName == "Attack3")
    {
        lobsterAnimator.SetBool("Idle", true);
    }*/
    /*public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Changer")) // && Input.GetKeyDown(KeyCode.Space))
        {
            nextState = true;
        }
    }*/
    /*void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name); // Debug what we collided with

        if (collision.CompareTag("Changer"))
        {
            nextState = true;
            Debug.Log("Next state triggered!");
        }
    }*/
}
