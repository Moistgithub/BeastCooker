using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDizzyState : LobsterBaseState
{
    public LobsterAnimatorController lobAnim;
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
        // Destroy the object that triggered the collider
        Debug.Log("my head hurts");
        if (bossHealth != null && bossHealth.currentHealth == 5)
        {
            //lobster.SwitchState(lobster.damagedAState);
            lobster.SwitchState(lobster.damagedAState);
        }
    }
}
