using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpecialAttackLobster : MonoBehaviour
{

    [Header("Public Variables")]

    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public CinemachineVirtualCamera cam3;

    public ParticleSystem bubbleDeath;

    public CinemachineImpulseSource cis;
    public GameObject bubbleObj;
    public GameObject demonBubble;
    public GameObject goon1;
    public GameObject goon2;
    public GameObject goon3;
    public GameObject evilTentatickle;
    public GameObject glow;

    public GameObject bubbleSpawner;
    public GameObject tentaSpawner;

    public AudioSource aus;
    public AudioClip roar;
    public AudioClip bubblePop;
    public AudioClip charge;

    public StateChangeSnap scs;

    public NewPlayerMovement pm;
    public PlayerHealth ph;

    public float goonScaleDuration = 1.5f;

    public Vector3 maxBubbleScale;

    public float goonTimer;

    [Header("Private Variables")]

    //private Transform playerTransform;

    //private float growthTimer;

    //private bool hasFullyGrown = false;
    private bool specialStart = false;

    [Header("References")]
    public LobsterStateManager lsm;
    public LobsterVisualHandler lvh;
    public NBossHealth bh;


    // Start is called before the first frame update
    void Start()
    {
        scs = GetComponent<StateChangeSnap>();
        if (pm == null)
        {
            Debug.LogError("player not here");
        }
        if (ph == null)
        {
            Debug.LogError("player health not here");
        }
        lvh = GetComponentInChildren<LobsterVisualHandler>();
        lsm = GetComponent<LobsterStateManager>();
        bh = GetComponent<NBossHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (specialStart) return;
        Debug.Log("Starting");
        if (lsm.currentStateName == "LobsterDamagedB")
        {
            specialStart = true;
            StartCoroutine(StartSpecialIntro());
            //StartCoroutine(BubbleGrow());
            //StartCoroutine(GoonStatus());
        }
        else
        {
            return;
        }
    }

    private IEnumerator StartSpecialIntro()
    {
        bubbleSpawner.SetActive(false);
        lvh.currentAnimator.SetBool("Spiky", false);
        CameraManager.SwitchCamera(cam3);
        bh.isInvincible = true;
        ph.cantbeHurt = true;
        pm.playerSpeed = 0f;
        pm.dodgeRollSpeed = 0f;

        goon1.SetActive(true);
        goon2.SetActive(true);
        goon3.SetActive(true);
        yield return new WaitForSeconds(2f);
        CameraManager.SwitchCamera(cam2);
        if(aus != null)
        {
            aus.PlayOneShot(roar);
            aus.PlayOneShot(charge);
        }
        lvh.currentAnimator.SetBool("Special", true);
        glow.SetActive(true);
        bubbleObj.SetActive(true);

        yield return new WaitForSeconds(2f);
        CameraManager.SwitchCamera(cam1);
        ph.cantbeHurt = false;
        pm.playerSpeed = 1.7f;
        pm.dodgeRollSpeed = 9f;
        yield return new WaitForSeconds(1.2f);
        evilTentatickle.SetActive(true);

        Vector3 initialScale = bubbleObj.transform.localScale;

        float timer = 0f;

        while (timer < goonTimer)
        {
            timer += Time.deltaTime;
            bubbleObj.transform.localScale = Vector3.Lerp(initialScale, maxBubbleScale, timer / goonTimer);

            if (goon1 == null && goon2 == null && goon3 == null)
            {
                yield return StartCoroutine(DestroyBubble());
                yield break;
            }

            yield return null;
        }
        tentaSpawner.SetActive(false);
        bubbleObj.SetActive(false);
        
        demonBubble.SetActive(true);
    }
    private IEnumerator DestroyBubble()
    {
        ph.cantbeHurt = true;
        pm.playerSpeed = 0f;
        pm.dodgeRollSpeed = 0f;
        evilTentatickle.SetActive(false);
        CameraManager.SwitchCamera(cam2);
        yield return new WaitForSeconds(2f);
        if (aus != null)
        {
            aus.PlayOneShot(bubblePop);
            aus.PlayOneShot(roar);
        }
        bubbleObj.SetActive(false);
        bubbleDeath.Play();

        lvh.currentAnimator.SetBool("Dizzy", true);
        yield return new WaitForSeconds(1f);

        scs.StateSoundTransitionerShorter();
        glow.SetActive(false);
        CameraShaker.instance.CameraShake(cis);
        yield return new WaitForSeconds(4.2f);
        lvh.currentAnimator.SetBool("Dizzy", true);
        lvh.currentAnimator.SetBool("Special", false);
        lsm.SwitchState(lsm.dizzyState);
        CameraManager.SwitchCamera(cam1);
        pm.playerSpeed = 1.7f;
        pm.dodgeRollSpeed = 9f;
    }

}
