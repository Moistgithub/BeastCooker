using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroStarter : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public CinemachineVirtualCamera cam3;
    public GameObject Chicken;
    public PolygonCollider2D pc;
    public float waitingtime;
    public GameObject Fur;
    public PlayerMovement playerMovement;
    public GameObject wallA;
    public GameObject wallB;
    public EnemyAttackManager eam;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PolygonCollider2D>();
        eam = GetComponent<EnemyAttackManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc.enabled = false;
            StartCoroutine(SwitcherooIntro());
            Chicken.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    private IEnumerator SwitcherooIntro()
    {
        /*if (playerMovement != null)
        {
            playerMovement.SetFrozenState(true);
        }
        */
       // StartCoroutine(Cripple());
        wallA.SetActive(true);
        wallB.SetActive(true);
        CameraManager.SwitchCamera(cam2);
        waitingtime = 2f;
        yield return new WaitForSecondsRealtime(waitingtime);
        //HitStop.Instance.StopTime(2f);
        CameraManager.SwitchCamera(cam1);
        /*if (playerMovement != null)
        {
            playerMovement.SetFrozenState(false);
        }
        */
    }
    private IEnumerator Cripple()
    {
        if (eam != null)
        {
            eam.enabled = false;
        }

        yield return new WaitForSecondsRealtime(waitingtime);
        if (eam != null)
        {
            eam.enabled = true;
        }
    }
}
