using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DragonSpecialAttackManager : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject indicator;
    public GameObject specialUI;
    public GameObject SexyRar;

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
    public DragonVisualHandler dvh;

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
                StartCoroutine(DragonDeath());
                break;
        }
    }

    private IEnumerator DragonDeath()
    {
        indicator.SetActive(false);
        //CameraManager.SwitchCamera(cam2);
        pm.playerSpeed = 0f;
        pm.dodgeRollSpeed = 0f;
        pa.canAttack = false;



        specialUI.SetActive(true);

        yield return new WaitForSeconds(2f);

        SexyRar.SetActive(true);


        yield return new WaitForSeconds(2.2f);

        SexyRar.SetActive(false);

        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
        specialUI.SetActive(false);
        if (dvh != null)
        {
            dvh.currentAnimator.SetBool("Dizzy", false);
            dvh.currentAnimator.SetBool("Death", true);
            aus.PlayOneShot(roar);
        }
        HitStop.Instance.StopTime(0.2f);
        aus.PlayOneShot(kill);
        aus.PlayOneShot(snap);
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
