using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wog : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private List<GameObject> collectedItems = new List<GameObject>();
    [SerializeField] private GameObject GoodMeal;
    [SerializeField] private GameObject BadMeal;
    [SerializeField] public Transform spawnPos;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectItems();
        }
    }

    public void CollectItems()
    {
        //checks ifplayer has boss item
        if (playerInventory.GetBoss().Count > 0)
        {
            //transfers items
            List<GameObject> playerCollectedItems = playerInventory.GetCollectedItems();
            List<GameObject> playerBossItems = playerInventory.GetBoss();

            //takes from player
            Debug.Log("Collecting items...");

            //adds collected items
            collectedItems.AddRange(playerCollectedItems);
            collectedItems.AddRange(playerBossItems);

            //transfers items
            foreach (GameObject item in playerCollectedItems)
            {
                item.SetActive(false);
            }

            foreach (GameObject item in playerBossItems)
            {
                item.SetActive(false);
            }

            //clears player inventory
            playerInventory.ClearInventory();
            Debug.Log("Items collected from player.");
            SpawnItemBasedOnCollectionCount();
        }
        else
        {
            Debug.Log("Player must collect a Boss item first!");
        }
    }
    private void SpawnItemBasedOnCollectionCount()
    {
        Vector3 spawnPosition = spawnPos ? spawnPos.position : transform.position;
        if (collectedItems.Count > 2)
        {
            // Spawn the item for a collection count greater than 2
            if (GoodMeal != null)
            {
                Instantiate(GoodMeal, spawnPosition, Quaternion.identity);
                Debug.Log("Spawned item for more than 2 items.");
            }
        }
        else
        {
            // Spawn the item for a collection count less than or equal to 2
            if (BadMeal != null)
            {
                Instantiate(BadMeal, spawnPosition, Quaternion.identity);
                Debug.Log("Spawned item for 2 or fewer items.");
            }
        }
    }
    //wog list
    public List<GameObject> GetCollectedItems()
    {
        return collectedItems;
    }
}