using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChickenLightDamage : ChickenBaseState
{
    public NBossHealth bossHealth;
    public StateChangeSnap scs;
    public override void EnterState(ChickenStateManager chicken)
    {
        CinemachineImpulseSource impulseSource = chicken.GetComponent<CinemachineImpulseSource>();
        if (impulseSource != null)
        {
            CameraShaker.instance.CameraShake(impulseSource);
        }
        bossHealth = chicken.GetComponent<NBossHealth>();
        scs = chicken.GetComponent<StateChangeSnap>();
        scs.StateSoundTransitioner();
        bossHealth.knockbackForce = 170f;
        //lobsterAttackManager = lobster.GetComponent<LobsterAttackManager>();
        Debug.Log("Hurt Chicken");
        //lobsterAttackManager.canAttack = true;
    }
    public override void UpdateState(ChickenStateManager chicken)
    {
        if (bossHealth != null && bossHealth.currentHealth <= 50)
        {
            chicken.SwitchState(chicken.cutsceneState);
        }
    }
}
