 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LobsterBaseState
{
    public abstract void EnterState(LobsterStateManager lobster);
    public abstract void UpdateState(LobsterStateManager lobster);
}
