using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionSetup : MonoBehaviour
{
    [SerializeField]
    private List<QuestionData> questions;
    private QuestionData currentQuestion;

    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TextMeshProUGUI categoryText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private AnswerButton[] answerButtons;
    
    private int score = 0;

    [SerializeField]
    private int correctAnswerChoice;

    private bool questionsAvailable = true;

    private void Awake() 
    {
        GetQuestionAssets();    
    }

    void Start()
    {
        SelectNewQuestion();
        SetQuestionValues();
        SetAnswerValues();    
    }

    public bool QuestionsAvailable
    {
        get {return questionsAvailable;}
    }

    private void GetQuestionAssets()
    {
        questions = new List<QuestionData>(Resources.LoadAll<QuestionData>("Questions"));
    }

    private void SelectNewQuestion()
    {
        if (questions.Count > 0)
        {
            int randomQuestionIndex = Random.Range(0, questions.Count);
            currentQuestion = questions[randomQuestionIndex];
            questions.RemoveAt(randomQuestionIndex);
        }
        else
        {
            Debug.Log("No more questions!");
            questionsAvailable = false;
        }
    }

    private void SetQuestionValues()
    {
        questionText.text = currentQuestion.question;
        categoryText.text = currentQuestion.category;
    }

    private void SetAnswerValues()
    {
        List<string> answers = RandomizeAnswers(new List<string>(currentQuestion.answers));

        for (int i = 0; i < answerButtons.Length; i++)
        {
            bool isCorrect = false;

            if (i == correctAnswerChoice)
            {
                isCorrect = true;
            }

            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
        }
    }

    private List<string> RandomizeAnswers(List<string> originalList)
    {
        bool correctAnswerChosen = false;

        List<string> newList = new List<string>();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int random = Random.Range(0, originalList.Count);

            if (random == 0 && !correctAnswerChosen)
            {
                correctAnswerChoice = i;
                correctAnswerChosen = true;
            }

            newList.Add(originalList[random]);
            originalList.RemoveAt(random);
        }

        return newList;
    }

    public void OnAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("CORRECT");
            score += 100;
            UpdateScoreText();
            SelectNewQuestion();
            SetQuestionValues();
            SetAnswerValues();
        }
        else
        {
            Debug.Log("FALSE");
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
