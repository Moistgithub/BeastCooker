using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NewSpecialManagerLobster : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject indicator;
    public GameObject specialUI;
    public GameObject murderObject;
    public GameObject murderCollider;

    public Animator uiAnim;

    public bool canSP = false;
    public bool isSP = false;

    public StateChangeSnap scs;
    public AudioSource aus;
    public AudioSource backgroundMusic;
    public AudioClip roar;
    public AudioClip kill;
    public AudioClip snap;


    public CinemachineImpulseSource cis;
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    [Header("References")]
    public PlayerAttack pa;
    public NewPlayerMovement pm;
    public LobsterVisualHandler lvh;

    void Start()
    {
        aus = GetComponent<AudioSource>();
        cis = GetComponent<CinemachineImpulseSource>();
        //backgroundMusic = GameObject.Find("Song").GetComponent<AudioSource>();
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
                StartCoroutine(LobsterDeath());
                break;
        }
    }

    private IEnumerator LobsterDeath()
    {
        indicator.SetActive(false);
        CameraManager.SwitchCamera(cam2);
        pm.playerSpeed = 0f;
        pm.dodgeRollSpeed = 0f;
        pa.canAttack = false;
        murderCollider.SetActive(true);

        specialUI.SetActive(true);

        yield return new WaitForSeconds(2f);

        murderObject.SetActive(true);


        yield return new WaitForSeconds(1.3f);
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
        specialUI.SetActive(false);
        if (lvh != null)
        {
            lvh.currentAnimator.SetBool("Dizzy", false);
            lvh.currentAnimator.SetBool("Death", true);
            aus.PlayOneShot(roar);
        }
        HitStop.Instance.StopTime(0.2f);
        aus.PlayOneShot(kill);
        aus.PlayOneShot(snap);
        //yield return new WaitForSeconds(0.1f);

        //scs.StateSoundTransitionerShortest();
        scs.KillTransitioner();
        if (cis != null)
        {
            CameraShaker.instance.CameraShake(cis);
        }
        yield return new WaitForSeconds(1f);
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
