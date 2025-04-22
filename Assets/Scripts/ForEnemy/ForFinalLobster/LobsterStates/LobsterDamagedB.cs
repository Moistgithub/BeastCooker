using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDamagedB : LobsterBaseState
{
    public override void EnterState(LobsterStateManager lobster)
    {
        Debug.Log("Desperation Time");
    }
    public override void UpdateState(LobsterStateManager lobster)
    {

    }
}
