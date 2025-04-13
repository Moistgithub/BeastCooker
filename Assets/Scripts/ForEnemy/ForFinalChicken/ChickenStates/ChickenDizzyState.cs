using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDizzyState : ChickenBaseState
{
    public StateChangeSnap scs;
    public GameObject player;
    public override void EnterState(ChickenStateManager chicken)
    {
        scs = chicken.GetComponent<StateChangeSnap>();
        scs.StateSoundTransitioner();
        Debug.Log("Dizzy Chicken");
    }
    public override void UpdateState(ChickenStateManager chicken)
    {
        
    }
}
