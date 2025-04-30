using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
public class DialogueManager : MonoBehaviour
{
    //this script is referenced from Rain Studios
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI npcName;

    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip letterSoundLight;
    [SerializeField] private AudioClip letterSoundDark;
    [SerializeField] private AudioClip letterSoundLighter;
    [SerializeField] private AudioClip letterSoundMiddle;

    private AudioClip currentLetterSound;

    private Story currentStory;
    private static DialogueManager Instance;
    public bool dialoguePlaying { get; private set; }
    public NewPlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public bool chat = false;
    public Coroutine displayLineCoroutine;
    private void Awake()
    {
        if (Instance != null)
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

    public void EnterDialogueMode(TextAsset inkJSON, float npcVar, string npcName)
    {
        chat = true;
        currentStory = new Story(inkJSON.text);
        dialoguePlaying = true;
        dialoguePanel.SetActive(true);
        this.npcName.text = npcName;
        SuperCripple();

        if (npcVar == 0)
            currentLetterSound = letterSoundLighter;
        else if (npcVar == 1)
            currentLetterSound = letterSoundLight;
        else if (npcVar == 2)
            currentLetterSound = letterSoundMiddle;
        else if (npcVar == 3)
            currentLetterSound = letterSoundDark;
        else
            currentLetterSound = null;

        ContinueStory();
    }
    private IEnumerator ExitDialogueMode()
    {
        if (audioSource != null)
            audioSource.Stop();

        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
            displayLineCoroutine = null;
        }

        yield return new WaitForSeconds(0.2f);
        SuperUnCripple();
        chat = false;
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            //dialogueText.text = currentStory.Continue();
            Debug.Log("Story continues...");
        }
        else
        {
            Debug.Log("Story finished, exiting dialogue.");
            StartCoroutine(ExitDialogueMode());
        }
    }
    private void SuperCripple()
    {
        if(playerMovement != null)
        {
            playerMovement.playerSpeed = 0f;
            playerMovement.dodgeRollSpeed = 0f;
        }
    }
    private void SuperUnCripple()
    {
        if (playerMovement != null)
        {
            //dialoguePanel.SetActive(false);
            playerMovement.playerSpeed = 1.7f;
            playerMovement.dodgeRollSpeed = 9f;
        }
    }
    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        int letterCount = 0;

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;

            if (!char.IsWhiteSpace(letter))
            {
                letterCount++;

                if (letterCount % 2 == 0 && currentLetterSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(currentLetterSound);
                }
            }

            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
