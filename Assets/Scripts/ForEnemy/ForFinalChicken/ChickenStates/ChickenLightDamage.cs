using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLightDamage : ChickenBaseState
{
    public NBossHealth bossHealth;
    public override void EnterState(ChickenStateManager chicken)
    {
        bossHealth = chicken.GetComponent<NBossHealth>();
        bossHealth.knockbackForce = 170f;
        //lobsterAttackManager = lobster.GetComponent<LobsterAttackManager>();
        Debug.Log("Hurt Chicken");
        //lobsterAttackManager.canAttack = true;
    }
    public override void UpdateState(ChickenStateManager chicken)
    {
        if (bossHealth != null && bossHealth.currentHealth <= 40)
        {
            chicken.SwitchState(chicken.cutsceneState);
        }
    }
}
