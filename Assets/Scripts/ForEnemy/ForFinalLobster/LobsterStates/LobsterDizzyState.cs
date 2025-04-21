using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDizzyState : LobsterBaseState
{
    public NBossHealth bossHealth;
    public NewLobsterAttackManager lobsterAttackManager;
    public override void EnterState(LobsterStateManager lobster)
    {
        bossHealth = lobster.GetComponent<NBossHealth>();
        lobsterAttackManager = lobster.GetComponent<NewLobsterAttackManager>();
        Debug.Log("Huhhhh Owwww Whaaaaats going on?");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {

    }
}
