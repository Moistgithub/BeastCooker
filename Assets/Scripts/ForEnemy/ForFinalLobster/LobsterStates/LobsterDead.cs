using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDead : LobsterBaseState
{
    public LobsterAnimatorController lobAnim;
    public LobsterAttackManager lobsterAttackManager;
    public LobsterAnimatorController lobsterAnimator;
    public BossHealth bossHealth;

    public override void EnterState(LobsterStateManager lobster)
    {
        bossHealth = lobster.GetComponent<BossHealth>();
        bossHealth.isInvincible = true;
        lobsterAttackManager = lobster.GetComponent<LobsterAttackManager>();
        lobsterAnimator = lobster.GetComponent <LobsterAnimatorController>();
        Debug.Log("Dead");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        lobsterAttackManager.canAttack = false;
        Debug.Log("I'm dead mamamia");
    }
}
