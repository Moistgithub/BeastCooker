using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EggEventStarter : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject player;
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public float waitingtime;
    public EggEventStarter evs;

    public GameObject egg1;
    public GameObject egg2;

    public Animator eggAnim;

    public AudioSource AudioSource;
    public AudioClip roar;

    [Header("References")]
    public ChickenStateManager csm;
    public ChickenMovement cm;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        csm = GetComponent<ChickenStateManager>();
        cm = GetComponent<ChickenMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (csm.currentStateName == "ChickenCutsceneIdleState")
        {
            if(!hasStartedSwitcheroo)
            {
                StartCoroutine(SwitcherooIntro());
                hasStartedSwitcheroo = true;
            }

        }
        else
        {
            return;
        }
    }

    bool hasStartedSwitcheroo = false;


    private IEnumerator SwitcherooIntro()
    {


        bool hasPlayedAudio = false;
        
        CameraManager.SwitchCamera(cam2);
        cm.enabled = false;
        yield return new WaitForSecondsRealtime(1f);
        egg1.SetActive(false);
        egg2.SetActive(true);

        if (roar != null && !hasPlayedAudio)
        {
            hasPlayedAudio = true;
            AudioSource.PlayOneShot(roar);
        }
        yield return new WaitForSecondsRealtime(1.25f);
        if (eggAnim != null)
        {
            eggAnim.SetBool("Battle", true);
        }
        yield return new WaitForSecondsRealtime(waitingtime);
        cm.enabled = true;
        CameraManager.SwitchCamera(cam1);
        evs.enabled = false;
    }
}
