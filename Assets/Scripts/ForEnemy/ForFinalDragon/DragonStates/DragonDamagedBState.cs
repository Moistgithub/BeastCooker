using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDamagedBState : DragonBaseState
{
    public StateChangeSnap scs;
    public NBossHealth bossHealth;
    public DragonVisualHandler dvh;


    public override void EnterState(DragonStateManager dragon)
    {
        dvh = dragon.GetComponent<DragonVisualHandler>();
        dvh.currentAnimator.SetBool("Dizzy", false);
        dvh.currentAnimator.SetBool("Roar", false);
        dvh.currentAnimator.SetBool("Charge", false);

        bossHealth = dragon.GetComponent<NBossHealth>();
        bossHealth.isInvincible = false;
    }
    public override void UpdateState(DragonStateManager dragon)
    {
        if (bossHealth != null && bossHealth.currentHealth <= 30)
        {
            dragon.SwitchState(dragon.dizzyState);
        }
    }
}
