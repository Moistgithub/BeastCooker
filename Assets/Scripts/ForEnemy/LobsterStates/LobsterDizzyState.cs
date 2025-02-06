using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDizzyState : LobsterBaseState
{
    public LobsterAttackManager lobsterAttackManager;
    public BossHealth bossHealth;

    public override void EnterState(LobsterStateManager lobster)
    {
        bossHealth = lobster.GetComponent<BossHealth>();
        bossHealth.isInvincible = true;
        lobsterAttackManager = lobster.GetComponent<LobsterAttackManager>();
        Debug.Log("Huhhhh Owwww Whaaaaats going on?");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        lobsterAttackManager.canAttack = false;
        if(bossHealth.currentHealth >= 5f)
        {
            lobster.SwitchState(lobster.damagedAState);
        }
        // Destroy the object that triggered the collider
        Debug.Log("my head hurts");
    }
}
