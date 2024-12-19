using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIngredients : MonoBehaviour
{
    public float fallSpeed = 2f;
    private bool isFalling = false;

    // Update is called once per frame
    void Update()
    {
        if (!isFalling)
        {
            isFalling = true;
            GetComponent<Rigidbody2D>().gravityScale = 0.05f;
        }
    }

    // When the object collides with the player, it will trigger the inventory update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Access the player's inventory component and add 1 to the inventory
            WogInventory inventory = collision.gameObject.GetComponent<WogInventory>();
            if (inventory != null)
            {
                inventory.ingredientsCollected += 1;  // Add one item to the player's inventory
                Debug.Log("Item collected by player. Total ingredients: " + inventory.ingredientsCollected);

                // Optionally, deactivate or destroy the falling item after collection
                gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.CompareTag("destroylider"))
        {
            Destroy(gameObject);
        }
    }

}
