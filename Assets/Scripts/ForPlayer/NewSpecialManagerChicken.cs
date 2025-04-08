using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class NewSpecialManagerChicken : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject indicator;
    public GameObject specialUI;
    public GameObject murderObject;
    public bool canSP = false;
    public bool isSP = false;
        
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    [Header("References")]
    public PlayerAttack pa;
    public NewPlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<NewPlayerMovement>();
        pa = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2) && canSP)
        {

            pa.canAttack = false;
            Debug.Log("SP time");
            canSP = false; // Prevent multiple presses
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
                StartCoroutine(TurkeyButt());
                break;

        }
    }

    private IEnumerator TurkeyButt()
    {
        CameraManager.SwitchCamera(cam2);
        pm.playerSpeed = 0f;
        pa.canAttack = false;
        //playerAttack.enabled = false;
        specialUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        murderObject.SetActive(true);
        yield return new WaitForSeconds(2.8f);
        CameraManager.SwitchCamera(cam1);
        specialUI.SetActive(false);
        canSP = false;

        isSP = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Special"))
        {
            indicator.SetActive(true);
            pa.enabled = false;
            canSP = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Special"))
        {
            indicator.SetActive(false);
            pa.enabled = true;
            canSP = false;
        }

    }
}
