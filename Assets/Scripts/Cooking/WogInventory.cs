using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WogInventory : MonoBehaviour
{
    public Animator whiteboxAnim;
    public string nextScene;
    public int ingredientsCollected = 0;
    public int maxIngredients = 5;

    [SerializeField] private List<GameObject> collected = new List<GameObject>();

    private void Update()
    {

        if (ingredientsCollected == 5)
        {
            StartCoroutine(Fade());
        }
    }
    // Collect an ingredient (projectile)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ingredient"))
        {
            //deactivate ingredient
            collision.gameObject.SetActive(false);

            collected.Add(collision.gameObject);

            //Debug.Log("Item collected: " + collision.gameObject.name);

        }
    }
    public List<GameObject> GetCollectedItems()
    {
        return collected;
    }

    // Load the next scene

    public IEnumerator Fade()
    {
        whiteboxAnim.SetBool("IsFadingIn", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextScene);
    }
}
