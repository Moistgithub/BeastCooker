using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterHealthyState : LobsterBaseState
{
    public BossHealth bossHealth;
    public LobsterAttackManager lobsterAttackManager;
    public override void EnterState(LobsterStateManager lobster)
    {
        bossHealth = lobster.GetComponent<BossHealth>();
        lobsterAttackManager = lobster.GetComponent<LobsterAttackManager>();
        Debug.Log("Hello I'm Healthy Lobter");
        lobsterAttackManager.canAttack = true;
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        if(bossHealth != null && bossHealth.currentHealth <= 10)
        {
            //lobster.SwitchState(lobster.damagedAState);
            lobster.SwitchState(lobster.dizzyState);
        }
    }
}
