using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SPManager : MonoBehaviour
{
    public GameObject indicator;
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public float itemdelayTime;
    public bool canSP = false;
    public float spDuration;
    public bool isSP = false;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public GameObject specialUI;
    public GameObject specialUI2;
    public GameObject salt;
    public bool canWog = false;
    private Coroutine storedCoroutine;
    // Start is called before the first frame update

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is inside the trigger zone and left mouse button is pressed
        if (Input.GetMouseButtonDown(2) && canSP)
        {

                playerAttack.canAttack = false;
                Debug.Log("SP time");
                //canSP = false; // Prevent multiple presses
                StartCoroutine(PerformAttack(SPAttack.attack1)); // Trigger special attack 1
        }
    }

    private enum SPAttack
    {
        attack1,
    }

    private IEnumerator PerformAttack(SPAttack attack)
    {
        if (isSP) yield break;

        isSP = true;

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
        playerMovement.isInvincible = true;
        CameraManager.SwitchCamera(cam2);
        itemdelayTime = 2f;
        playerMovement.canMove = false;
        playerMovement.speed = 0f;
        playerMovement.SetFrozenState(true);
        playerAttack.canAttack = false;
        playerAttack.enabled = false;
        specialUI.SetActive(true);
        yield return new WaitForSeconds(itemdelayTime);
        salt.SetActive(true);
        yield return new WaitForSeconds(2.8f);
        CameraManager.SwitchCamera(cam1);
        specialUI.SetActive(false);
        playerMovement.SetNormalState(true);
        playerMovement.canMove = true;
        playerAttack.canAttack = true;
        playerAttack.enabled = true;
        playerMovement.speed = 1.5f;
        canSP = false;
        playerMovement.isInvincible = false;

        isSP = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Special"))
        {
            indicator.SetActive(true);
            playerAttack.enabled = false;
            canSP = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Special"))
        {
            indicator.SetActive(false);
            playerAttack.enabled = true;
            canSP = false;
        }

    }
}
