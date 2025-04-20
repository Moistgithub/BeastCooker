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
        bossHealth.isInvincible = false;
        lobsterAttackManager.canAttack = true;
        if (bossHealth != null && bossHealth.currentHealth == 10)
        {
            lobster.SwitchState(lobster.dizzierState);
        }
    }
}
