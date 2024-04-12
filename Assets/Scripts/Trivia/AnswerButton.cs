using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
    [SerializeField]
    private TextMeshProUGUI answerText;

    private bool isInteractable = true;

    public void SetInteractable(bool value)
    {
        isInteractable = value;
    }

    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void OnClick()
    {
        if (isInteractable)
        {
            if (FindObjectOfType<QuestionSetup>().QuestionsAvailable)
            {
                FindObjectOfType<QuestionSetup>().OnAnswer(isCorrect);
            }
            else
            {
                Debug.Log("NO MORE QUESTIONS AVAILABLE!");
            }
        }
    }
}
