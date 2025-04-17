using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChickenCutsceneIdleState : ChickenBaseState
{
    public StateChangeSnap scs;
    public bool canTransform = false;
    private float timer = 0f;
    public float timerDuration = 6f;
    public override void EnterState(ChickenStateManager chicken)
    {
        CinemachineImpulseSource impulseSource = chicken.GetComponent<CinemachineImpulseSource>();
        if (impulseSource != null)
        {
            CameraShaker.instance.CameraShake(impulseSource);
        }
        scs = chicken.GetComponent<StateChangeSnap>();
        scs.StateSoundTransitioner();
        Debug.Log("Cutscene Chicken");
        timer = 0f;
        //lobsterAttackManager.canAttack = true;
    }
    public override void UpdateState(ChickenStateManager chicken)
    {
        timer += Time.deltaTime;
        if (timer >= timerDuration)
        {
            canTransform = true;
        }

        if (canTransform)
        {
            chicken.SwitchState(chicken.dyingState);
        }
    }
}
