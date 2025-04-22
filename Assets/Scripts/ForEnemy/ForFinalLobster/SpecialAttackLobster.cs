using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpecialAttackLobster : MonoBehaviour
{

    [Header("Public Variables")]

    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public CinemachineVirtualCamera cam3;

    public GameObject bubbleObj;
    public GameObject demonBubble;
    public GameObject goon1;
    public GameObject goon2;
    public GameObject goon3;

    public float goonScaleDuration = 1.5f;

    public Vector3 maxBubbleScale;

    public float goonTimer;

    [Header("Private Variables")]

    //private Transform playerTransform;

    //private float growthTimer;

    //private bool hasFullyGrown = false;
    private bool specialStart = false;

    [Header("References")]
    public LobsterStateManager lsm;
    public LobsterVisualHandler lvh;


    // Start is called before the first frame update
    void Start()
    {
        lvh = GetComponentInChildren<LobsterVisualHandler>();
        lsm = GetComponent<LobsterStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (specialStart) return;
        Debug.Log("Starting");
        if (lsm.currentStateName == "LobsterDamagedB")
        {
            specialStart = true;
            StartCoroutine(StartSpecialIntro());
            //StartCoroutine(BubbleGrow());
            //StartCoroutine(GoonStatus());
        }
        else
        {
            return;
        }
    }

    private IEnumerator StartSpecialIntro()
    {
        CameraManager.SwitchCamera(cam3);
        yield return new WaitForSeconds(3.2f);
        goon1.SetActive(true);
        goon2.SetActive(true);
        goon3.SetActive(true);

        CameraManager.SwitchCamera(cam2);
        lvh.currentAnimator.SetBool("Special", true);
        bubbleObj.SetActive(true);

        yield return new WaitForSeconds(2f);
        CameraManager.SwitchCamera(cam1);

        Vector3 initialScale = bubbleObj.transform.localScale;

        float timer = 0f;

        while (timer < goonTimer)
        {
            timer += Time.deltaTime;
            bubbleObj.transform.localScale = Vector3.Lerp(initialScale, maxBubbleScale, timer / goonTimer);

            if (goon1 == null && goon2 == null && goon3 == null)
            {
                yield return StartCoroutine(DestroyBubble());
                yield break;
            }

            yield return null;
        }

        bubbleObj.SetActive(false);
        
        demonBubble.SetActive(true);
    }
    private IEnumerator DestroyBubble()
    {
        CameraManager.SwitchCamera(cam2);
        yield return new WaitForSeconds(2f);
        bubbleObj.SetActive(false);
        yield return new WaitForSeconds(1f);
        lvh.currentAnimator.SetBool("Special", false);
        lsm.SwitchState(lsm.dizzyState);
        CameraManager.SwitchCamera(cam1);
    }

}
