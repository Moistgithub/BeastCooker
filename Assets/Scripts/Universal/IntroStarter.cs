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
        /*if(Fur == null)
        {
            StartCoroutine(SwitcherooEgg());
            return;
        }
        */
    }
    private IEnumerator SwitcherooIntro()
    {
        CameraManager.SwitchCamera(cam2);
        waitingtime = 1.5f;
        yield return new WaitForSecondsRealtime(waitingtime);
        //HitStop.Instance.StopTime(2f);
        CameraManager.SwitchCamera(cam1);
    }
    private IEnumerator SwitcherooEgg()
    {
        CameraManager.SwitchCamera(cam3);
        waitingtime = 2f;
        yield return new WaitForSecondsRealtime(waitingtime);
        //HitStop.Instance.StopTime(1f);
        CameraManager.SwitchCamera(cam1);
        cam3.Priority = 0;
        cam1.Priority = 10;
    }
}
