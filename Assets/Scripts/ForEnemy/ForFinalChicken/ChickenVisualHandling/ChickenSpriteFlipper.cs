using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpriteFlipper : MonoBehaviour
{
    public Transform player;  // Reference to the player's Transform

    // Update is called once per frame
    void Update()
    {
        //check if the player is on the right or left based on the x-axis position
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
