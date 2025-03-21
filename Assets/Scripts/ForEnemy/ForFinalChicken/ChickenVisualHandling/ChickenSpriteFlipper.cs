using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpriteFlipper : MonoBehaviour
{
    public Transform player;  // Reference to the player's Transform

    // Update is called once per frame
    void Update()
    {
        // Check if the player is on the right or left based on the x-axis position
        if (player.position.x > transform.position.x)
        {
            // Player is to the right, set the scale to normal (facing right)
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // Player is to the left, flip the scale (facing left)
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
