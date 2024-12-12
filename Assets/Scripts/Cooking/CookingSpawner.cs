using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSpawner : MonoBehaviour
{
    public GameObject currentSpawner;
    public GameObject nextSpawner;
    public GameObject ingredientPrefab; 
    public Transform spawnPoint; 
    public float fallSpeed;        
    public float spawnInterval = 2f;
    public float spawnTime;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnIngredient());
    }
   /* private void SpawnIngredient()
    {
        GameObject ingredient = Instantiate(ingredientPrefab, spawnPoint.position, Quaternion.identity);
        nextSpawner.SetActive(true);
        currentSpawner.SetActive(false);
        Rigidbody rb = ingredient.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = ingredient.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;
        rb.drag = fallSpeed;
        nextSpawner.SetActive(true);
    }
    */
    private IEnumerator SpawnIngredient()
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject ingredient = Instantiate(ingredientPrefab, spawnPoint.position, Quaternion.identity);
        nextSpawner.SetActive(true);
        currentSpawner.SetActive(false);
        Rigidbody rb = ingredient.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = ingredient.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;
        rb.drag = fallSpeed;
    }
}
