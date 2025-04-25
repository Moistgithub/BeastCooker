using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DragonAngryState : DragonBaseState
{
    public StateChangeSnap scs;
    public bool canTransform = false;
    private float timer = 0f;
    public float timerDuration = 6f;
    public DragonVisualHandler dvh;
    public NBossHealth bossHealth;
    public override void EnterState(DragonStateManager dragon)
    {
        bossHealth = dragon.GetComponent<NBossHealth>();
        bossHealth.isInvincible = true;
        dvh = dragon.GetComponent<DragonVisualHandler>();
        dvh.currentAnimator.SetBool("Dizzy", false);
        dvh.currentAnimator.SetBool("Roar", false);
        dvh.currentAnimator.SetBool("Charge", false);
        CinemachineImpulseSource impulseSource = dragon.GetComponent<CinemachineImpulseSource>();
        if (impulseSource != null)
        {
            CameraShaker.instance.CameraShake(impulseSource);
        }
        scs = dragon.GetComponent<StateChangeSnap>();
        scs.StateSoundTransitioner();
        Debug.Log("Cutscene Dragon");
        timer = 0f;
        //lobsterAttackManager.canAttack = true;
    }
    public override void UpdateState(DragonStateManager dragon)
    {
        timer += Time.deltaTime;
        if (timer >= timerDuration)
        {
            canTransform = true;
        }

        if (canTransform)
        {
            dragon.SwitchState(dragon.damagedBState);
        }
    }
}
