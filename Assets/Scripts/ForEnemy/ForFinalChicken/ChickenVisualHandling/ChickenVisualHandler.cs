using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenVisualHandler : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject chickenHealthySprite;
    public GameObject chickenHurtSprite;
    public GameObject chickenDyingSprite;
    public GameObject egg1;
    public GameObject egg2;

    public Animator currentAnimator;
    public Animator chickenFull;
    public Animator chickenHalf;
    public Animator chickenNaked;

    [Header("References")]
    public ChickenStateManager csm;
    public NBossHealth bossHealth;
    public ChickenMovement cm;
    public ChickenAttackManager cam;
    public ChickenSpriteFlipper csf;
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
        cam = GetComponent<ChickenAttackManager>();
        csf = GetComponentInChildren<ChickenSpriteFlipper>();
        cm = GetComponent<ChickenMovement>();
        csm = GetComponent<ChickenStateManager>();
        bossHealth = GetComponent<NBossHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (csm.currentStateName == "ChickenIdleState")
        {
            currentAnimator = chickenFull;
            currentAnimator.SetBool("idle", true);
            chickenHurtSprite.SetActive(false);
            chickenDyingSprite.SetActive(false);
            //Debug.Log("it works");
        }
        if (csm.currentStateName == "ChickenHealthyState")
        {
            currentAnimator.SetBool("idle", false);
            //Debug.Log("it works");
        }
        if (csm.currentStateName == "ChickenLightDamage")
        {
            currentAnimator = chickenHalf;
            chickenHealthySprite.SetActive(false);
            chickenHurtSprite.SetActive(true);
            //Debug.Log("it hurts");
        }
        if (csm.currentStateName == "ChickenCutsceneIdleState")
        {
            ph.cantbeHurt = true;
            bossHealth.isInvincible = true;
            currentAnimator = chickenNaked;
            cm.enabled = false;
            chickenHurtSprite.SetActive(false);
            chickenDyingSprite.SetActive(true);
            //Debug.Log("it scenes");
        }
        if (csm.currentStateName == "ChickenHeavyDamage")
        {
            ph.cantbeHurt = false;
            bossHealth.isInvincible = false;
            //its in update so i gotta fix it before it does the move
            cm.enabled = true;
            //currentAnimator = chickenNaked;
            //chickenHurtSprite.SetActive(false);
            //chickenDyingSprite.SetActive(true);
            //Debug.Log("it burns");
        }
        if (csm.currentStateName == "ChickenDizzyState")
        {
            ph.cantbeHurt = true;
            egg1.SetActive(false);
            egg2.SetActive(true);
            csf.transform.localScale = new Vector3(1, 1, 1);
            //transform.localScale = new Vector3(1, 1, 1);
            cm.speed = 0f;
            bossHealth.isInvincible = true;
            currentAnimator.SetBool("dizzy", true);
            csf.enabled = false;
            //Debug.Log("it burns");
        }
    }
}
