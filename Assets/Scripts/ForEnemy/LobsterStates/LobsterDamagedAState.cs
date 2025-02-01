using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDamagedAState : LobsterBaseState
{
    public BossHealth bossHealth;
    public override void EnterState(LobsterStateManager lobster)
    {
        bossHealth = lobster.GetComponent<BossHealth>();
        Debug.Log("Owww A Lobter");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        if (bossHealth != null && bossHealth.currentHealth <= 5)
        {
            lobster.SwitchState(lobster.damagedBState);
        }
    }
}
