using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class WogInventory : MonoBehaviour
{
    public Animator whiteboxAnim;
    public string nextScene;
    public int ingredientsCollected = 0;
    public int maxIngredients = 5;
    public CinemachineImpulseSource cis;

    [SerializeField] private List<GameObject> collected = new List<GameObject>();

    private void Update()
    {

        if (ingredientsCollected == 10)
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

            if (cis != null)
            {
                Debug.LogWarning("Impulse Source is null!");
                CameraShaker.instance.CameraShake(cis);
            }

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
