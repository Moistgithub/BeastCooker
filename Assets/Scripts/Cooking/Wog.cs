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

    private bool cooking = false;
    private float holdStartTime = 0f;
    private bool isHoldingSpace = false;
    private bool playerinZone = false;
    public Transform foodSpritepos;
    public GameObject food;
    public SpriteRenderer foodSprite;


    private void Start()
    {
        if (food != null)
        {
            foodSprite = food.GetComponent<SpriteRenderer>();
        }
    }
    void Update()
    {
        if (playerinZone && Input.GetKeyDown(KeyCode.Space))
        {
            if (playerInventory.GetBoss().Count > 0)
            {
                CollectItems();
                StartMiniGame();
            }
            else
            {
                Debug.Log("You need a Boss item to start cooking!");
            }
        }
        if (cooking)
        {
            if (!isHoldingSpace && Input.GetKeyDown(KeyCode.Space))
            {
                isHoldingSpace = true;
                holdStartTime = Time.time;
            }

            if (isHoldingSpace && Input.GetKeyUp(KeyCode.Space))
            {
                isHoldingSpace = false;
                CookingEnd(Time.time - holdStartTime);
            }
        }
        if (foodSprite != null && cooking)
        {
            float elapsedTime = Time.time - holdStartTime;

            //update foods color during cooking
            if (elapsedTime >= 5f && elapsedTime < 7f)
            {
                foodSprite.color = Color.yellow;
            }
            else if (elapsedTime >= 7f)
            {
                foodSprite.color = Color.black;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerinZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerinZone = false;
        }
    }


    private void StartMiniGame()
    {
        cooking = true;
        holdStartTime = Time.time;
        if (food == null)
        {
            return;
        }
        StartCoroutine(COOKING());

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
            //SpawnItemBasedOnCollectionCount();
        }
        else
        {
            Debug.Log("Player must collect a Boss item first!");
        }
    }
    //wog list
    public List<GameObject> GetCollectedItems()
    {
        return collectedItems;
    }
    private IEnumerator COOKING()
    {
        food.SetActive(true);
        Debug.Log("Cooking time!");
        float timer = 9f;
        float elapsedTime = 0f;

        while (elapsedTime < timer)
        {
            if (isHoldingSpace)
            {
                elapsedTime = Time.time - holdStartTime;
            }
            yield return null;
        }

        CookingEnd(elapsedTime);
    }
    private void CookingEnd(float holdDuration)
    {
        Vector3 spawnPosition = spawnPos ? spawnPos.position : transform.position;
        if (GoodMeal != null && holdDuration >= 5f  && holdDuration < 7f)
        {
            Instantiate(GoodMeal, spawnPosition, Quaternion.identity);
        }
        if (BadMeal != null && holdDuration < 5f)
        {
            Instantiate(BadMeal, spawnPosition, Quaternion.identity);
        }
        if (BadMeal != null && holdDuration > 7f)
        {
            Instantiate(BadMeal, spawnPosition, Quaternion.identity);
        }
        Destroy(food);
    }
}