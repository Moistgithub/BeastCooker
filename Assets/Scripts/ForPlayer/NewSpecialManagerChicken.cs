using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class NewSpecialManagerChicken : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject indicator;
    public GameObject specialUI;
    public GameObject murderObject;
    public GameObject chicken;
    public GameObject player;
    public Animator uiAnim;
    public bool canSP = false;
    public bool isSP = false;
    public StateChangeSnap scs;
    public AudioSource aus;
    public AudioClip chickenRoar;
        
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    [Header("References")]
    public PlayerAttack pa;
    public NewPlayerMovement pm;
    public ChickenVisualHandler cvh;

    [Header("Special Move Targets")]
    public Vector3 endChickenPos;
    public Vector3 endTBPos;
    public Vector3 endPlayerPos;

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
            canSP = false; //prevent multiple presses
            StartCoroutine(PerformAttack(SPAttack.attack1));
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

        float chickenDuration = 1.5f;
        float chickenElapsed = 0f;
        Vector3 chickenStart = chicken.transform.position;
        Vector3 chickenEnd = endChickenPos;


        while (chickenElapsed < chickenDuration)
        {
            chicken.transform.position = Vector3.Lerp(chickenStart, chickenEnd, chickenElapsed / chickenDuration);
            chickenElapsed += Time.deltaTime;
            yield return null;
        }
        chicken.transform.position = chickenEnd;
        yield return new WaitForSeconds(3f);
        murderObject.SetActive(true);

        float playerDuration = 1.5f;
        float playerElapsed = 0f;
        Vector3 playerStart = player.transform.position;
        Vector3 playerEnd = endPlayerPos;

        while (playerElapsed < playerDuration)
        {
            player.transform.position = Vector3.Lerp(playerStart, playerEnd, playerElapsed / playerDuration);
            playerElapsed += Time.deltaTime;
            yield return null;
        }

        player.transform.position = playerEnd;

        float duration = 1.5f;
        float elapsedTime = 0f;
        Vector3 start = murderObject.transform.position;
        Vector3 end = endTBPos;

        specialUI.SetActive(false);

        while (elapsedTime < duration)
        {
            murderObject.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        murderObject.transform.position = end;
        if(scs != null)
        {
            scs.StateSoundTransitioner();
            if(cvh != null)
            {
                cvh.currentAnimator.SetBool("dying", true);
            }
            HitStop.Instance.StopTime(1f);
            if(aus != null)
            {
                aus.PlayOneShot(chickenRoar);
            }
        }

        yield return new WaitForSeconds(2.8f);

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
