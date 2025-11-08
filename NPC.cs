using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 
using System; // Importar System para usar Action

// Supondo que IInteractable e PauseController já existem ou serão criados

public class NPC : MonoBehaviour, IInteractable
{
    // --- NOVO: Definição do Evento ---
    // Usamos Action para criar um evento que notifica quando o diálogo acaba.
    public static event Action OnDialogueEnded; 
    // -----------------------------------

    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
        {
            return;
        }
        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.name);
        portraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);

        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        // ✅ AJUSTE FINAL:
        // 1. Verifica o limite do array primeiro para evitar IndexOutOfRangeException.
        // 2. Usa a variável 'autoProgressDelay' do seu ScriptableObject.
        if (dialogueData.autoProgressLines != null && 
            dialogueIndex < dialogueData.autoProgressLines.Length && 
            dialogueData.autoProgressLines[dialogueIndex])
        {
            // Substituímos 'yield return new WaitForSeconds(1.0f);'
            yield return new WaitForSeconds(dialogueData.autoProgressDelay); 
            NextLine();
        }
    }
    
    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        PauseController.SetPause(false);
        
        OnDialogueEnded?.Invoke();
    }
}