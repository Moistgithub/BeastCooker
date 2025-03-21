using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChickenBaseState
{
    public abstract void EnterState(ChickenStateManager chicken);
    public abstract void UpdateState(ChickenStateManager chicken);
}
