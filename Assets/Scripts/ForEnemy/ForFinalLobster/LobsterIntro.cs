using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LobsterIntro : MonoBehaviour
{
    [Header("Public Variables")]

    public CinemachineVirtualCamera playercam;
    public CinemachineVirtualCamera zoomoutcam;
    public CinemachineVirtualCamera lobstercam;
    public AudioSource aus;
    public AudioClip roar;
    public LobsterVisualHandler lvh;
    public CinemachineImpulseSource cis;
    public LobsterStateManager lsm;

    public AudioSource music;

    public NewPlayerMovement pm;
    public PolygonCollider2D pc;
    public float waitingtime;
    public GameObject Spikes;
    public GameObject Spikes2;
    public GameObject Spikes3;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc.enabled = false;
            StartCoroutine(BossIntro());
        }
    }
    private IEnumerator BossIntro()
    {
        if (pm != null)
        {
            pm.playerSpeed = 0f;
            pm.dodgeRollSpeed = 0f;
        }
        CameraManager.SwitchCamera(zoomoutcam);

        /*yield return new WaitForSecondsRealtime(0.5f);
        Spikes.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        Spikes2.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        Spikes3.SetActive(true);*/

        CameraManager.SwitchCamera(lobstercam);
        yield return new WaitForSecondsRealtime(1f);
        if (aus != null)
        {
            music.Play();
            if (lvh != null)
            {
                lvh.currentAnimator.SetBool("Spiky", true);
            }
            aus.PlayOneShot(roar);
            if (cis != null)
            {
                CameraShaker.instance.CameraShake(cis);

            }
        }
        yield return new WaitForSecondsRealtime(2f);
        lvh.currentAnimator.SetBool("Spiky", false);
        yield return new WaitForSecondsRealtime(0.5f);
        CameraManager.SwitchCamera(playercam);
        if (pm != null)
        {
            pm.playerSpeed = 1.7f;
            pm.dodgeRollSpeed = 9f;
            yield return new WaitForSecondsRealtime(0.5f);
            if (lsm != null)
            {
                lsm.SwitchState(lsm.healthyState);
            }
        }
        pc.enabled = false;
        // introObject.SetActive(false);

    }
}
