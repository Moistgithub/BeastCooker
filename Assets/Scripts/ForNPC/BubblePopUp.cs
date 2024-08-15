using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubblePopUp : MonoBehaviour
{
    public GameObject bubble;
    private bool playerInTrigger;
    public string dialogueScene;

    private void Awake()
    {
        playerInTrigger = false;
        bubble.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger)
        {
            bubble.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(dialogueScene);
            }
        }
        else
        {
            bubble.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInTrigger = false;
        }
    }
}
