using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List <GameObject> collected = new List<GameObject>();
    [SerializeField] private List<GameObject> bossCollected = new List<GameObject>();

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the "Item" tag
        if (collision.CompareTag("Item"))
        {
            //deactivate the item
            collision.gameObject.SetActive(false);

            //add the item to the list
            collected.Add(collision.gameObject);

            Debug.Log("Item collected: " + collision.gameObject.name);
        }
        if (collision.CompareTag("BossItem"))
        {
            //deactivate the item
            collision.gameObject.SetActive(false);

            //add the item to the list
            bossCollected.Add(collision.gameObject);

            Debug.Log("Item collected: " + collision.gameObject.name);
        }
    }
    public List<GameObject> GetCollectedItems()
    {
        return collected;
    }
    public List<GameObject> GetBoss()
    {
        return bossCollected;
    }
    public void ClearInventory()
    {
        collected.Clear();
        bossCollected.Clear();
        Debug.Log("Inventory cleared!");
    }
}
