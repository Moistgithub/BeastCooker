using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDamagedAState : LobsterBaseState
{
    public LobsterAttackManager lobsterAttackManager;
    public BossHealth bossHealth;
    public override void EnterState(LobsterStateManager lobster)
    {
        lobsterAttackManager = lobster.GetComponent<LobsterAttackManager>();
        bossHealth = lobster.GetComponent<BossHealth>();
        Debug.Log("Owww A Lobter");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        lobsterAttackManager.canAttack = true;
        if (bossHealth != null && bossHealth.currentHealth == 1)
        {
            lobster.SwitchState(lobster.damagedBState);
        }
    }
}
