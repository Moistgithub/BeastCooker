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
    public GameObject falsePlayer;
    public GameObject chicken;
    public GameObject player;
    public Animator uiAnim;
    public Animator fpAnim;
    public bool canSP = false;
    public bool isSP = false;
    public StateChangeSnap scs;
    public AudioSource aus;
    public AudioSource backgroundMusic;
    public AudioClip chickenRoar;
    public AudioClip explosion;

    public CinemachineImpulseSource cis;
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
    public Vector3 endFPPos;

    public Vector3 finalChickenPos;
    public Vector3 finalBasterPos;
    public Vector3 finalfPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
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
                StartCoroutine(TurkeyButt());
                break;
        }
    }

    private IEnumerator TurkeyButt()
    {
        CameraManager.SwitchCamera(cam2);
        pm.playerSpeed = 0f;
        pm.dodgeRollSpeed = 0f;
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
        yield return new WaitForSeconds(1f);
        murderObject.SetActive(true);
        falsePlayer.SetActive(true);

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
        Vector3 fpstart = falsePlayer.transform.position;
        Vector3 start = murderObject.transform.position;
        Vector3 end = endTBPos;
        Vector3 fpend = endFPPos;

        specialUI.SetActive(false);

        while (elapsedTime < duration)
        {
            murderObject.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            falsePlayer.transform.position = Vector3.Lerp(fpstart, fpend, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        murderObject.transform.position = end;
        if(scs != null)
        {
            scs.StateSoundTransitioner();
            scs.KillTransitioner();
            if(cvh != null)
            {
                cvh.currentAnimator.SetBool("dying", true);
                if (cis != null)
                {
                    CameraShaker.instance.CameraShake(cis);
                }
            }
            HitStop.Instance.StopTime(0.1f);
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
            }
            yield return new WaitForSeconds(0.5f);
            if (aus != null)
            {
                aus.PlayOneShot(chickenRoar);
            }
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(CutsceneEndingChicken());

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
    private IEnumerator CutsceneEndingChicken()
    {
        float duration = 1.5f;
        float fplayerDuration = 1f;
        float elapsedTime = 0f;

        Vector3 chickenStart = chicken.transform.position;
        Vector3 basterStart = murderObject.transform.position;
        Vector3 fplayerStart = falsePlayer.transform.position;

        while (elapsedTime < duration)
        {
            chicken.transform.position = Vector3.Lerp(chickenStart, finalChickenPos, elapsedTime / duration);
            murderObject.transform.position = Vector3.Lerp(basterStart, finalBasterPos, elapsedTime / duration);
            falsePlayer.transform.position = Vector3.Lerp(fplayerStart, finalfPlayerPos, elapsedTime / fplayerDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (fpAnim != null)
        {
            fpAnim.SetBool("Win", true);
        }

        if (aus != null)
        {
            aus.PlayOneShot(explosion);
        }
        if (cis != null)
        {
            CameraShaker.instance.CameraShake(cis);
        }
        yield return new WaitForSeconds(1f);
        if (aus != null)
        {
            aus.PlayOneShot(explosion);
        }
        if (cis != null)
        {
            CameraShaker.instance.CameraShake(cis);
        }
        yield return new WaitForSeconds(0.7f);
        if (aus != null)
        {
            aus.PlayOneShot(explosion);
        }
        if (cis != null)
        {
            CameraShaker.instance.CameraShake(cis);
        }

        chicken.transform.position = finalChickenPos;
        murderObject.transform.position = finalBasterPos;
    }
}
