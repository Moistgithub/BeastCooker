using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDizzyState : LobsterBaseState
{
    public LobsterAttackManager lobsterAttackManager;
    public override void EnterState(LobsterStateManager lobster)
    {
        lobsterAttackManager = lobster.GetComponent<LobsterAttackManager>();
        Debug.Log("Huhhhh Owwww Whaaaaats going on?");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {
        lobsterAttackManager.canAttack = false;
        Debug.Log("my head hurts");
    }
}
