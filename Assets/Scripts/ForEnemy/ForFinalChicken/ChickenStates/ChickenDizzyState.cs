using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChickenDizzyState : ChickenBaseState
{
    public StateChangeSnap scs;
    public GameObject player;
    public GameObject spRange;
    public override void EnterState(ChickenStateManager chicken)
    {
        CinemachineImpulseSource impulseSource = chicken.GetComponent<CinemachineImpulseSource>();
        if (impulseSource != null)
        {
            CameraShaker.instance.CameraShake(impulseSource);
        }
        scs = chicken.GetComponent<StateChangeSnap>();
        scs.StateSoundTransitioner();
        Debug.Log("Dizzy Chicken");

        foreach (Transform child in chicken.transform)
        {
            if (child.CompareTag("Special"))
            {
                spRange = child.gameObject;
                spRange.SetActive(true);
                break;
            }
        }
    }
    public override void UpdateState(ChickenStateManager chicken)
    {
        
    }
}
