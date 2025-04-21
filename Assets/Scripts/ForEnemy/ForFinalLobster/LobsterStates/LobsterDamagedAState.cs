using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LobsterDamagedAState : LobsterBaseState
{
    public StateChangeSnap scs;
    public NBossHealth bossHealth;
    public NewLobsterAttackManager lobsterAttackManager;
    public LobsterVisualHandler lvh;
    public override void EnterState(LobsterStateManager lobster)
    {
        lvh = lobster.GetComponent<LobsterVisualHandler>();
        lvh.currentAnimator.SetBool("Spiky", false);
        lvh.currentAnimator.SetBool("Slash", false);
        scs = lobster.GetComponent<StateChangeSnap>();
        CinemachineImpulseSource impulseSource = lobster.GetComponent<CinemachineImpulseSource>();
        if (impulseSource != null)
        {
            CameraShaker.instance.CameraShake(impulseSource);
        }
        scs.StateSoundTransitioner();

        bossHealth = lobster.GetComponent<NBossHealth>();
        lobsterAttackManager = lobster.GetComponent<NewLobsterAttackManager>();
        Debug.Log("Owww A Lobter");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        if (bossHealth != null && bossHealth.currentHealth <= 10)
        {
            lobster.SwitchState(lobster.dizzyState);
        }
    }
}
