using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Queue<Dialogue.DialogueLine> dialogueLines;

    public Animator animator;

    void Start()
    {
        dialogueLines = new Queue<Dialogue.DialogueLine>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        dialogueLines.Clear();

        foreach (Dialogue.DialogueLine line in dialogue.dialogueLines)
        {
            dialogueLines.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue.DialogueLine line = dialogueLines.Dequeue();
        nameText.text = line.speaker;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
