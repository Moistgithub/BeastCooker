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
        waitingtime =1.6f;
        //StartCoroutine(enemyAttackManager.StopAttackingTemporarily(2f));
        yield return new WaitForSecondsRealtime(waitingtime);
        //HitStop.Instance.StopTime(2f);
        CameraManager.SwitchCamera(cam1);
        /*if (playerMovement != null)
        {
            playerMovement.SetFrozenState(false);
        }
        */
    }
}
