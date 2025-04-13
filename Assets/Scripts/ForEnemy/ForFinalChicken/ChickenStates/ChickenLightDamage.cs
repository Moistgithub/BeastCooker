using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLightDamage : ChickenBaseState
{
    public NBossHealth bossHealth;
    public StateChangeSnap scs;
    public override void EnterState(ChickenStateManager chicken)
    {
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
