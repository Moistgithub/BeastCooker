using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDizzierState : LobsterBaseState
{
    public LobsterAnimatorController lobAnim;
    public LobsterAttackManager lobsterAttackManager;
    public BossHealth bossHealth;
    public override void EnterState(LobsterStateManager lobster)
    {
        bossHealth = lobster.GetComponent<BossHealth>();
        lobsterAttackManager = lobster.GetComponent<LobsterAttackManager>();
        Debug.Log("Owwwowowowo you will pae");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        lobsterAttackManager.canAttack = false;
        if (bossHealth != null && bossHealth.currentHealth == 0)
        {
            //lobster.SwitchState(lobster.damagedAState);
            lobster.SwitchState(lobster.deadState);
        }
    }
}
