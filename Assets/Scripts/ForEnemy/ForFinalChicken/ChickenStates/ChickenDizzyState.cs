using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDizzyState : ChickenBaseState
{
    public GameObject player;
    public override void EnterState(ChickenStateManager chicken)
    {
        Debug.Log("Healthy Chicken");
    }
    public override void UpdateState(ChickenStateManager chicken)
    {
        
    }
}
