using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterVisualHandler : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject lobsterExtraHB1;
    public GameObject lobsterExtraHB2;
    public GameObject lobsterPart1;
    public GameObject lobsterPart2;
    public GameObject lobsterPart3;

    public Animator currentAnimator;

    [Header("References")]
    public LobsterStateManager lsm;
    public NBossHealth bossHealth;
    public NewLobsterAttackManager nlam;


    public NewPlayerMovement pm;
    public PlayerHealth ph;
    // Start is called before the first frame update
    void Start()
    {
        if (pm == null)
        {
            Debug.LogError("player not here");
        }
        if (ph == null)
        {
            Debug.LogError("player health not here");
        }
        nlam = GetComponent<NewLobsterAttackManager>();
        lsm = GetComponent<LobsterStateManager>();
        bossHealth = GetComponent<NBossHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lsm.currentStateName == "LobsterCutsceneState")
        {
            //currentAnimator.SetBool("idle", true);
            Debug.Log("Cutscene Lobster");
        }
        if (lsm.currentStateName == "LobsternHealthyState")
        {
            //currentAnimator.SetBool("idle", false);
            Debug.Log("Healthy Lobster");
        }
        if (lsm.currentStateName == "LobsterDamagedAState")
        {
            Debug.Log("Lobster Ouch A");
        }
        if (lsm.currentStateName == "LobsterDamagedBState")
        {
            Debug.Log("Lobster Oucvh");
        }
        if (lsm.currentStateName == "LobsterDizzyState")
        {
            nlam.attack1.SetActive(false);
            nlam.attack2.SetActive(false);
            nlam.attack3.SetActive(false);
            currentAnimator.SetBool("Dizzy", true);
            nlam.enabled = true;
            ph.cantbeHurt = true;
            bossHealth.isInvincible = true;
        }
    }
}

