using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FallingIngredients : MonoBehaviour
{
    public float fallSpeed = 2f;
    private bool isFalling = false;
    public CinemachineImpulseSource cis;

    // Update is called once per frame
    void Update()
    {
        if (!isFalling)
        {
            isFalling = true;
            GetComponent<Rigidbody2D>().gravityScale = 0.05f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(cis != null)
            {
                CameraShaker.instance.CameraShake(cis);
            }
            //add 1 to inv
            WogInventory inventory = collision.gameObject.GetComponent<WogInventory>();
            if (inventory != null)
            {
                inventory.ingredientsCollected += 1;
                //this line here
                Debug.Log("Item collected by player. Total ingredients: " + inventory.ingredientsCollected);
                //just in cASE
                gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.CompareTag("destroylider"))
        {
            Destroy(gameObject);
        }
    }

}
