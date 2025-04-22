using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterHealthyState : LobsterBaseState
{
    public NBossHealth bossHealth;
    public NewLobsterAttackManager lobsterAttackManager;
    public override void EnterState(LobsterStateManager lobster)
    {
        bossHealth = lobster.GetComponent<NBossHealth>();
        lobsterAttackManager = lobster.GetComponent<NewLobsterAttackManager>();
        Debug.Log("Hello I'm Healthy Lobter");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        if(bossHealth != null && bossHealth.currentHealth <= 100)
        {
            lobster.SwitchState(lobster.damagedAState);
        }
    }
}
