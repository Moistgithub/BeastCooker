using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHealthyState : DragonBaseState
{
    public NBossHealth bossHealth;

    public override void EnterState(DragonStateManager dragon)
    {
        bossHealth = dragon.GetComponent<NBossHealth>();
    }

    public override void UpdateState(DragonStateManager dragon)
    {
        if (bossHealth != null && bossHealth.currentHealth <= 160)
        {
            dragon.SwitchState(dragon.damagedAState);
        }
    }
}
