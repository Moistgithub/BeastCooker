using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCrippler : MonoBehaviour
{
    public PlayerAttack playerAttack;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerAttack != null)
            {
                playerAttack = other.GetComponent<PlayerAttack>();
                if (playerAttack != null)
                {
                    playerAttack.blockAttacks = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerAttack != null)
        {
            playerAttack = other.GetComponent<PlayerAttack>();
            if (playerAttack != null)
            {
                playerAttack.blockAttacks = false;
            }
        }
    }
}
