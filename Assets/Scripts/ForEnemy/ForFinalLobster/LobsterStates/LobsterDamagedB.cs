using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LobsterDamagedB : LobsterBaseState
{
    public StateChangeSnap scs;
    public NewLobsterAttackManager nlam;

    public override void EnterState(LobsterStateManager lobster)
    {
        nlam = lobster.GetComponent<NewLobsterAttackManager>();
        nlam.attack1.SetActive(false);
        nlam.attack2.SetActive(false);
        nlam.attack3.SetActive(false);
        nlam.attack3Light.SetActive(false);
        nlam.attack3fast.SetActive(false);
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
