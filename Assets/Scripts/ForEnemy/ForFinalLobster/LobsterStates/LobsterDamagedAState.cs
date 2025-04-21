using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDamagedAState : LobsterBaseState
{
    public NBossHealth bossHealth;
    public NewLobsterAttackManager lobsterAttackManager;
    public override void EnterState(LobsterStateManager lobster)
    {
        bossHealth = lobster.GetComponent<NBossHealth>();
        lobsterAttackManager = lobster.GetComponent<NewLobsterAttackManager>();
        Debug.Log("Owww A Lobter");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        if (bossHealth != null && bossHealth.currentHealth <= 10)
        {
            lobster.SwitchState(lobster.dizzyState);
        }
    }
}
