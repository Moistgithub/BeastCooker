using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenHeavyDamage : ChickenBaseState
{
    public NBossHealth bossHealth;
    public override void EnterState(ChickenStateManager chicken)
    {
        bossHealth = chicken.GetComponent<NBossHealth>();
        bossHealth.knockbackForce = 200f;
        //lobsterAttackManager = lobster.GetComponent<LobsterAttackManager>();
        Debug.Log("Dying Chicken");
        //lobsterAttackManager.canAttack = true;
    }
    public override void UpdateState(ChickenStateManager chicken)
    {
        if (bossHealth != null && bossHealth.currentHealth <= 0)
        {
            //lobster.SwitchState(lobster.damagedAState);
            chicken.SwitchState(chicken.dizzyState);
        }
    }
}
