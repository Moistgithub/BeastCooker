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
        dvh.currentAnimator.SetBool("Dizzy", true);
        scs = dragon.GetComponent<StateChangeSnap>();

        CinemachineImpulseSource impulseSource = dragon.GetComponent<CinemachineImpulseSource>();
        if (impulseSource != null)
        {
            CameraShaker.instance.CameraShake(impulseSource);
        }
        scs.StateSoundTransitioner();

        bossHealth = dragon.GetComponent<NBossHealth>();
    }
    public override void UpdateState(DragonStateManager dragon)
    {

    }
}
