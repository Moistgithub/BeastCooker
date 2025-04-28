using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialogue : MonoBehaviour
{
    //this script is referenced from Rain Studios
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    public bool playerInRange;
    public float npcVar;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }
    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialoguePlaying)
        {
            visualCue.SetActive(true);

            //if (Input.GetKeyDown(KeyCode.E))
            if (Input.GetMouseButtonDown(0))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, npcVar);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerInRange)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerInRange = false;
            }
        }
    }
}
