using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    public Transform player;
    public float detectionRadius;
    public float moveSpeed;
    private bool isMoving = false;

    void OnEnable()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("No player.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != true)
        {
            return;
        }
        //checks if player is in radius
        /*float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRadius)
        {
            if (!isMoving)
            {
                isMoving = true;
            }
            MoveTowards();
        }
        else
        {
            isMoving = false;
        }
    }

    private void MoveTowards()
    {
        //move the object towards the player
        Vector3 moveDirection = (player.position - transform.position).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
        */
    }
}