using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterDizzyState : LobsterBaseState
{
    public NBossHealth bossHealth;
    public NewLobsterAttackManager lobsterAttackManager;
    public GameObject spRange;
    public override void EnterState(LobsterStateManager lobster)
    {
        bossHealth = lobster.GetComponent<NBossHealth>();
        lobsterAttackManager = lobster.GetComponent<NewLobsterAttackManager>();
        Debug.Log("Huhhhh Owwww Whaaaaats going on?");


        foreach (Transform child in lobster.transform)
        {
            if (child.CompareTag("Special"))
            {
                spRange = child.gameObject;
                spRange.SetActive(true);
                break;
            }
        }
    }
    public override void UpdateState(LobsterStateManager lobster)
    {

    }
}
