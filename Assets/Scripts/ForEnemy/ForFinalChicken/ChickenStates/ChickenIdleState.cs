using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenIdleState : ChickenBaseState
{
    public GameObject player;
    private float detectionRadius = 3f;
    public override void EnterState(ChickenStateManager chicken)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Idle chicken");
    }
    public override void UpdateState(ChickenStateManager chicken)
    {
        if (player != null)
        {
            float distance = Vector3.Distance(chicken.transform.position, player.transform.position);
            if (distance <= detectionRadius)
            {
                chicken.SwitchState(chicken.healthyState);
            }
        }
    }
}
