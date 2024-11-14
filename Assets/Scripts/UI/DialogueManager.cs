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
    }
    private void Update()
    {
        if (!dialoguePlaying)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialoguePlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }
    private void ExitDialogueMode()
    {
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        if (playerMovement != null)
        {
            playerMovement.SetFrozenState(false);
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
