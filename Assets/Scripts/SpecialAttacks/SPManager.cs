using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SPManager : MonoBehaviour
{
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
    public GameObject salt;
    public GameObject seasoning;
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
    }

    private IEnumerator SeasonSplash()
    {
        CameraManager.SwitchCamera(cam2);
        itemdelayTime = 2f;
        playerMovement.canMove = false;
        playerMovement.speed = 0f;
        playerMovement.SetFrozenState(true);
        playerAttack.canAttack = false;
        playerAttack.enabled = false;
        specialUI.SetActive(true);
        yield return new WaitForSeconds(itemdelayTime);
        seasoning.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        CameraManager.SwitchCamera(cam1);
        specialUI.SetActive(false);
        playerMovement.SetNormalState(true);
        playerMovement.canMove = true;
        playerAttack.canAttack = true;
        playerAttack.enabled = true;
        playerMovement.speed = 1.5f;
        canSP = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);

        canSP = true;
        if (other.CompareTag("Special") && canSP) // && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("sp time");
            canSP = false;
            StartCoroutine(PerformAttack(SPAttack.attack1));
        }
    }
}
