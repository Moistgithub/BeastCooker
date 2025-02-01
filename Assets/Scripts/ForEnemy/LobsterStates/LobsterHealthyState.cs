using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterHealthyState : LobsterBaseState
{
    public BossHealth bossHealth;
    public override void EnterState(LobsterStateManager lobster)
    {
        bossHealth = lobster.GetComponent<BossHealth>();
        Debug.Log("Hello I'm Healthy Lobter");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        if(bossHealth != null && bossHealth.currentHealth <= 10)
        {
            lobster.SwitchState(lobster.damagedAState);
        }
    }
}
