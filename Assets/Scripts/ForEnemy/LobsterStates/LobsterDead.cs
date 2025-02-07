using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDead : LobsterBaseState
{
    public LobsterAnimatorController lobAnim;
    public LobsterAttackManager lobsterAttackManager;
    public BossHealth bossHealth;

    public override void EnterState(LobsterStateManager lobster)
    {
        bossHealth = lobster.GetComponent<BossHealth>();
        bossHealth.isInvincible = true;
        lobsterAttackManager = lobster.GetComponent<LobsterAttackManager>();
        Debug.Log("Dead");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        lobsterAttackManager.canAttack = false;
        Debug.Log("I'm dead mamamia");
        if (bossHealth != null && bossHealth.currentHealth == 50 || bossHealth.currentHealth == 45)
        {

        }
    }
}
