using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragonBaseState
{
    public abstract void EnterState(DragonStateManager dragon);
    public abstract void UpdateState(DragonStateManager dragon);
}
