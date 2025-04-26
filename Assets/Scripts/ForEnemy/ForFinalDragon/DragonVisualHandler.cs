using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonVisualHandler : MonoBehaviour
{
    public Animator currentAnimator;
    public DragonStateManager dsm;
    public GameObject brownHead;
    public GameObject whiteHead;
    public GameObject player;
    public NewPlayerMovement pm;
    public PlayerHealth ph;
    public NBossHealth bossHealth;
    public GameObject eyebrow1;
    public GameObject eyebrow2;
    public GameObject Spawner1;
    public GameObject Spawner2;


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
        bossHealth = GetComponent<NBossHealth>();
        currentAnimator = GetComponentInChildren<Animator>();
        dsm = GetComponent<DragonStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dsm.currentStateName == "DragonHealthyState")
        {
            //currentAnimator.SetBool("idle", false);
            Debug.Log("Healthy Dragon");
        }
        if (dsm.currentStateName == "DragonDamagedAState")
        {
            brownHead.SetActive(false);

            Debug.Log("Lobster Ouch A");
        }
        if (dsm.currentStateName == "DragonAngryState")
        {
            ph.cantbeHurt = true;
            //bossHealth.isInvincible = true;
            whiteHead.SetActive(false);
            Debug.Log("Lobster Oucvh");
        }
        if (dsm.currentStateName == "DragonDamagedBState")
        {
            ph.cantbeHurt = false;
            //bossHealth.isInvincible = false;
        }
        if (dsm.currentStateName == "DragonDizzyState")
        {
            currentAnimator.SetBool("Roar", false);
            currentAnimator.SetBool("Charge", false);
            bossHealth.isInvincible = true;
            eyebrow1.SetActive(false);
            eyebrow2.SetActive(false);
            Spawner1.SetActive(false);
            Spawner2.SetActive(false);
            ph.cantbeHurt = false;
        }
    }
}
