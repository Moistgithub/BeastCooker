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


    public EnemyShotSpawner enemyShotSpawner1; 
    public EnemyShotSpawner enemyShotSpawner2;
    //public CapsuleCollider2D lobsterColl;

    public GameObject lobsterSpecialTrigger;
    public GameObject lobsterSpecialRange;
    public GameObject wogSpecialRange;
    public bool nextState = false;
    public GameObject fakeEnding;
    public LobsterDizzierState lobdizi;
    //animator
    public Animator lobsterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        lobsterAnimator = GetComponentInChildren<Animator>();
        //lobsterAnimator = GetComponent<Animator>(); 
        lobsterstateManager = GetComponent<LobsterStateManager>();
        lobsterattackManager = GetComponent<LobsterAttackManager>();
        if (enemyShotSpawner1 != null)
        {
            enemyShotSpawner1.timervalue = 0.3f;
        }

        if (enemyShotSpawner2 != null)
        {
            enemyShotSpawner2.timervalue = 0.3f;
        }
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
            lobsterSpecialRange.SetActive(false);
            lobsterAnimator.SetBool("dizzy", false);
            lobsterAnimator.SetBool("Special", false);
            lobsterAnimator.SetBool("Idle", true);
            lobsterattackManager.canAttack = true;
            if (enemyShotSpawner1 != null)
            {
                enemyShotSpawner1.timervalue = 0.1f;
            }

            if (enemyShotSpawner2 != null)
            {
                enemyShotSpawner2.timervalue = 0.1f;
            }
            tentacleA.SetActive(false);
            tentacleB.SetActive(false);
            tentacleC.SetActive(false);
            tentacleD.SetActive(false);
            Debug.Log("its great!");

        }
        if (lobsterstateManager.currentStateName == "LobsterDizzyState")
        {
            lobsterAnimator.SetBool("dizzy", true);
            lobsterSpecialTrigger.SetActive(true);
            lobsterSpecialRange.SetActive(true);
        }
        if (lobsterstateManager.currentStateName == "LobsterDizzierState")
        {
            wogSpecialRange.SetActive(true);
            lobsterAnimator.SetBool("dizzy", true);
            lobsterSpecialTrigger.SetActive(true);
            //lobsterSpecialRange.SetActive(true);
        }
        if (lobsterstateManager.currentStateName == "LobsterDead")
        {
            Debug.Log("dead running");
            lobsterAnimator.SetBool("Dead", true);
            lobsterAnimator.SetBool("dizzy", false);
            lobsterAnimator.SetBool("Idle", false);
            lobsterSpecialTrigger.SetActive(true);
            fakeEnding.SetActive(true);
        } 
    }
}
