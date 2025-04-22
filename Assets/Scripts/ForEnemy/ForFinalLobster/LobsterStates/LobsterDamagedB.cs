using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LobsterDamagedB : LobsterBaseState
{
    public StateChangeSnap scs;

    public override void EnterState(LobsterStateManager lobster)
    {
        CinemachineImpulseSource impulseSource = lobster.GetComponent<CinemachineImpulseSource>();
        if (impulseSource != null)
        {
            CameraShaker.instance.CameraShake(impulseSource);
        }
        scs = lobster.GetComponent<StateChangeSnap>();
        scs.StateSoundTransitioner();
        Debug.Log("Desperation Time");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {

    }
}
