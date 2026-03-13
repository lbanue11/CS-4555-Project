using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private PlayerInteractor playerInteractor;

    private string[] lines;
    private int currentLine;
    private bool isOpen;

    private void Awake()
    {
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (isOpen && Keyboard.current.eKey.wasPressedThisFrame)
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueData data)
    {
        playerInteractor.enabled = false;
        lines = data.lines;
        currentLine = 0;
        isOpen = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = lines[currentLine];
    }

    private void NextLine()
    {
        currentLine++;
        if (currentLine >= lines.Length)
        {
            HideDialogue();
            return;
        }
        dialogueText.text = lines[currentLine];
    }

    public void HideDialogue()
    {
        playerInteractor.enabled = true;
        isOpen = false;
        dialoguePanel.SetActive(false);
        lines = null;
    }
}