using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroStarter : MonoBehaviour
{
    public AudioSource music;
    public NewPlayerMovement pm;
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public PolygonCollider2D pc;
    public float waitingtime;
    public float shadowTime;
    public float bridgeTime;
    public GameObject wallA;
    public GameObject wallB;
    public GameObject BridgeA;
    public GameObject BridgeB;
    public GameObject Boss;
    public GameObject cShadow;
    public AudioSource aus;
    public AudioClip woosh;
    public Vector2 targetPosition;
    // public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        aus = GetComponent<AudioSource>();
        pc = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc.enabled = false;
            StartCoroutine(SwitcherooIntro());
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    private IEnumerator SwitcherooIntro()
    {
        /*wallA.SetActive(true);
        if(pm!= null)
        {
            pm.playerSpeed = 0f;
        }
        CameraManager.SwitchCamera(cam2);
        yield return new WaitForSecondsRealtime(waitingtime);
        music.Play ();
        Teleport();
        wallB.SetActive(false);
        if (pm != null)
        {
            pm.playerSpeed = 1.7f;
        }
        CameraManager.SwitchCamera(cam1);
        yield return new WaitForSecondsRealtime(2f);
        BridgeA.SetActive(false);
        BridgeB.SetActive(true);
        //music.SetActive(true);*/
        if (pm != null)
        {
            pm.playerSpeed = 0f;
            pm.dodgeRollSpeed = 0f;
        }
        CameraManager.SwitchCamera(cam2);
        yield return new WaitForSecondsRealtime(waitingtime);
        cShadow.SetActive(true);
        if (woosh != null)
        {
            aus.PlayOneShot(woosh);
        }
        yield return new WaitForSecondsRealtime(shadowTime);
        Teleport();
        wallB.SetActive(true);
        cShadow.SetActive(false);
        music.Play();
        if (pm != null)
        {
            pm.playerSpeed = 1.7f;
            pm.dodgeRollSpeed = 9f;
            yield return new WaitForSecondsRealtime(0.5f);
            wallA.SetActive(false);
        }
        CameraManager.SwitchCamera(cam1);
        yield return new WaitForSecondsRealtime(bridgeTime);
        if(BridgeA && BridgeB != null)
        {
            BridgeA.SetActive(false);
            BridgeB.SetActive(true);
        }
        Destroy(gameObject);
    }
    private void Teleport()
    {
        if (Boss != null)
        {
            Boss.transform.position = targetPosition;
        }
        else
        {
            Debug.LogError("assign it");
        }
    }

}
