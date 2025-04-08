using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCutsceneIdleState : ChickenBaseState
{
    public bool canTransform = false;
    private float timer = 0f;
    public float timerDuration = 7f;
    public override void EnterState(ChickenStateManager chicken)
    {
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
