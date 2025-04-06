using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroStarter : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public PolygonCollider2D pc;
    public float waitingtime;
    public GameObject wallA;
    public GameObject music;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            music.SetActive(true);
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
        CameraManager.SwitchCamera(cam2);
        yield return new WaitForSecondsRealtime(waitingtime);
        CameraManager.SwitchCamera(cam1);

    }
}
