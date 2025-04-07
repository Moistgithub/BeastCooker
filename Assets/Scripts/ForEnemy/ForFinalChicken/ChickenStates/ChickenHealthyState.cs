using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenHealthyState : ChickenBaseState
{
    public NBossHealth bossHealth;
    public ChickenMovement cm;

    public override void EnterState(ChickenStateManager chicken)
    {
        cm = chicken.GetComponent<ChickenMovement>();
        cm.enabled = true;
        bossHealth = chicken.GetComponent<NBossHealth>();
        Debug.Log("Healthy Chicken");
        //lobsterAttackManager.canAttack = true;
    }
    public override void UpdateState(ChickenStateManager chicken)
    {
        if (bossHealth != null && bossHealth.currentHealth <= 80)
        {
            chicken.SwitchState(chicken.hurtState);
            //chicken.SwitchState(chicken.dizzyState);
        }
    }
}
