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
    public bool addIngredients = false;
    public AudioSource aus;
    public AudioClip sound;
    public float MAXQuantity;

    [SerializeField] private List<GameObject> collected = new List<GameObject>();

    private void Start()
    {
        cis = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Manually triggering shake");
            CameraShaker.instance.CameraShake(cis);
        }
        if (ingredientsCollected == MAXQuantity)
        {
            StartCoroutine(Fade());
        }
    }
    private void AddIngredient(GameObject ingredient)
    {
        ingredient.SetActive(false);
        addIngredients = true;
        collected.Add(ingredient);
        ingredientsCollected++;
        addIngredients = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ingredient"))
        {
            AddIngredient(collision.gameObject);
        }
    }

    public List<GameObject> GetCollectedItems()
    {
        return collected;
    }

    public IEnumerator Fade()
    {
        whiteboxAnim.SetBool("IsFadingIn", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextScene);
    }
}