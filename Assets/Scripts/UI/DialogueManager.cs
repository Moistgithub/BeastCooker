using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;
    private static DialogueManager Instance;
    public bool dialoguePlaying { get; private set; }
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public bool chat = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("found more than one dialogue manager");
        }
        Instance = this;
    }
    public static DialogueManager GetInstance()
    {
        return Instance;
    }
    private void Start()
    {
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }
    private void Update()
    {
        if (!dialoguePlaying)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        chat = true;
        currentStory = new Story(inkJSON.text);
        dialoguePlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
        if (playerAttack != null)
        {
            playerAttack.enabled = false;
        }
    }
    private void ExitDialogueMode()
    {
        chat = false;
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        if (playerAttack != null)
        {
            playerAttack.enabled = true;
        }
        dialogueText.text = "";
    }
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
}
