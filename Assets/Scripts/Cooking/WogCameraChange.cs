using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class WogCameraChange : MonoBehaviour
{
    public GameObject Enemy;
    public CinemachineVirtualCamera playercam;
    public CinemachineVirtualCamera wogcam;
    public float waitingtime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemy == null)
        {
            StartCoroutine(SwitcherooWog());
        }
    }
    private IEnumerator SwitcherooWog()
    {
        CameraManager.SwitchCamera(wogcam);
        //waitingtime = 1f;
        yield return new WaitForSecondsRealtime(waitingtime);
        CameraManager.SwitchCamera(playercam);
    }
}
