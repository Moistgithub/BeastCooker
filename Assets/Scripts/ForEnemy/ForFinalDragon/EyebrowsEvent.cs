using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EyebrowsEvent : MonoBehaviour
{
    [Header("Public Variables")]
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public float waitingtime;
    public EyebrowsEvent evs;
    public AudioSource backgroundMusic;
    public AudioSource backgroundMusic2;


    public GameObject Eyebrow1;
    public GameObject Eyebrow2;


    public AudioSource AudioSource;
    public AudioClip shockSound;

    [Header("References")]
    public DragonStateManager dsm;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        dsm = GetComponent<DragonStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dsm.currentStateName == "DragonAngryState")
        {
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
            }
            if (!hasStartedSwitcheroo)
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
        yield return new WaitForSecondsRealtime(3f);


        if (shockSound != null && !hasPlayedAudio)
        {
            Eyebrow1.SetActive(true);
            Eyebrow2.SetActive(true);
            AudioSource.PlayOneShot(shockSound);
        }
        yield return new WaitForSecondsRealtime(1.25f);
        if (backgroundMusic2 != null)
        {
            backgroundMusic2.Play();
        }
        CameraManager.SwitchCamera(cam1);
        evs.enabled = false;
    }
}
