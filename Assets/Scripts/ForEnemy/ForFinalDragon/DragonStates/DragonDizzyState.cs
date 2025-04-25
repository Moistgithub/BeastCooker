using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DragonDizzyState : DragonBaseState
{
    public StateChangeSnap scs;
    public NBossHealth bossHealth;
    public DragonVisualHandler dvh;

    public override void EnterState(DragonStateManager dragon)
    {
        dvh = dragon.GetComponent<DragonVisualHandler>();
        bossHealth = dragon.GetComponent<NBossHealth>();
        dvh.currentAnimator.SetBool("Dizzy", true);
        scs = dragon.GetComponent<StateChangeSnap>();
        bossHealth.isInvincible = true;
        CinemachineImpulseSource impulseSource = dragon.GetComponent<CinemachineImpulseSource>();
        if (impulseSource != null)
        {
            CameraShaker.instance.CameraShake(impulseSource);
        }
        if(scs != null)
        {
            scs.StateSoundTransitioner();
        }
    }
    public override void UpdateState(DragonStateManager dragon)
    {

    }
}
