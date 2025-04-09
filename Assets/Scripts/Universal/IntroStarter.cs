using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroStarter : MonoBehaviour
{
    public NewPlayerMovement pm;
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public PolygonCollider2D pc;
    public float waitingtime;
    public GameObject wallA;
    public GameObject wallB;
    public GameObject BridgeA;
    public GameObject BridgeB;
    public GameObject Boss;
    public Vector2 targetPosition;
    //public GameObject music;

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
            StartCoroutine(SwitcherooIntro());
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    private IEnumerator SwitcherooIntro()
    {
        wallA.SetActive(true);
        if(pm!= null)
        {
            pm.playerSpeed = 0f;
        }
        CameraManager.SwitchCamera(cam2);
        yield return new WaitForSecondsRealtime(waitingtime);
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
        //music.SetActive(true);
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
