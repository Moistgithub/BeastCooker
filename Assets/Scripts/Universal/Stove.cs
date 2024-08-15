using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public string itemTag = "Pickup";
    public GameObject cookedmealPrefab;
    public int maxItems;

    private List<GameObject> collectedIngredients = new List<GameObject>();


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(itemTag) && collectedIngredients.Count < maxItems)
        {
            collectedIngredients.Add(collision.gameObject);
            collision.gameObject.SetActive(false);

            if (collectedIngredients.Count >= maxItems)
            {
                DisableCollectedItems();
                SpawnCookedMeal();
            }
        }
    }

    private void DisableCollectedItems()
    {
        foreach (GameObject item in collectedIngredients)
        {
            item.SetActive(false);
        }
        collectedIngredients.Clear();
    }

    private void SpawnCookedMeal()
    {
        if (cookedmealPrefab != null)
        {
            Instantiate(cookedmealPrefab, transform.position, Quaternion.identity);
        }
    }
}
