using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FinisherManager : MonoBehaviour
{
    public GameObject indicator;
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public float itemdelayTime;
    public float spDuration;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public GameObject specialUI;
    public GameObject wog;
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
        if (Input.GetMouseButtonDown(2) && canWog)
        {
                playerAttack.canAttack = false;
                Debug.Log("Wog time");
                canWog = false; // Prevent multiple presses
                StartCoroutine(PerformAttack(SPAttack.attack1)); // Trigger special attack 1
        }
    }

    private enum SPAttack
    {
        attack1,
    }

    private IEnumerator PerformAttack(SPAttack attack)
    {
        if (canWog) yield break;

        canWog = true;

        switch (attack)
        {
            case SPAttack.attack1:
                Attack1();
                break;

        }
    }
    private void Attack1()
    {
        StartCoroutine(WogMurder());
    }
    private IEnumerator WogMurder()
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
        wog.SetActive(true);
        CameraManager.SwitchCamera(cam1);
        specialUI.SetActive(false);
        playerMovement.SetNormalState(true);
        playerMovement.canMove = true;
        playerAttack.canAttack = true;
        playerAttack.enabled = true;
        playerMovement.speed = 1.5f;
        canWog = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WogFinish"))
        {
            indicator.SetActive(true);
            playerAttack.enabled = false;
            canWog = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("WogFinish"))
        {
            indicator.SetActive(false);
            playerAttack.enabled = true;
        }
    }
}

