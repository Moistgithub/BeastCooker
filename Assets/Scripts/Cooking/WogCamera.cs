using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WogCamera : MonoBehaviour
{
    public CinemachineVirtualCamera playercam;
    public CinemachineVirtualCamera wogcam;
    public float waitingtime;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        if (playercam != true)
            return;
        if (wogcam != true)
            return;
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemy == null)
        {
            StartCoroutine(SwitcherWog());
        }
    }
    private IEnumerator SwitcherWog()
    {
        CameraManager.SwitchCamera(wogcam);
        waitingtime = 1.6f;
        //StartCoroutine(enemyAttackManager.StopAttackingTemporarily(2f));
        yield return new WaitForSecondsRealtime(waitingtime);
        //HitStop.Instance.StopTime(2f);
        CameraManager.SwitchCamera(playercam);
    }
}
