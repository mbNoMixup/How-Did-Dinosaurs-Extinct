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
    private TextMeshProUGUI timerText;
    [SerializeField]
    private AnswerButton[] answerButtons;

    [SerializeField]
    private AudioClip correctAnswerClip;
    [SerializeField]
    private AudioClip wrongAnswerClip;

    private AudioSource audioSource;
    
    private int score = 0;

    [SerializeField]
    private float timerDuration;
    private float currentTimer;
    private bool timerRunning = true;

    [SerializeField]
    private int correctAnswerChoice;

    private bool questionsAvailable = true;

    private Color defaultScoreColor;
    private Color defaultTimerColor;
    private Color flashColor = Color.red;
    private Color correctColor = Color.green;

    private void Awake() 
    {
        GetQuestionAssets();    
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        defaultScoreColor = scoreText.color;
        defaultTimerColor = timerText.color;
        
        SelectNewQuestion();
        SetQuestionValues();
        SetAnswerValues();
        currentTimer = timerDuration;
        StartCoroutine(StartTimer());    
    }

    public void RestartGame()
    {
        score = 0;
        currentTimer = timerDuration;
        timerRunning = true;
        questionsAvailable = true;

        UpdateScoreText();
        UpdateTimerText();

        StopAllCoroutines();
        StartCoroutine(StartTimer());

        GetQuestionAssets();
        foreach (var button in answerButtons)
        {
            button.SetInteractable(true);
        }

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
            Debug.Log("NO MORE QUESTIONS!");
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
            StartCoroutine(FlashScoreText(correctColor));

            if (correctAnswerClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(correctAnswerClip);
            }

            SelectNewQuestion();
            SetQuestionValues();
            SetAnswerValues();
        }
        else
        {
            Debug.Log("FALSE");
            currentTimer -= 10f;
            UpdateTimerText();
            StartCoroutine(FlashTimerText(flashColor));

            if (wrongAnswerClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(wrongAnswerClip);
            }
        }

        if (questions.Count == 0)
        {
            Debug.Log("ALL QUESTIONS ANSWERED!");
            timerRunning = false;
        }
        else
        {
            SelectNewQuestion();
            SetQuestionValues();
            SetAnswerValues();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    IEnumerator StartTimer()
    {
        while (timerRunning)
        {
            yield return new WaitForSeconds(1f);
            currentTimer -= 1f;
            UpdateTimerText();

            if (currentTimer <= 0f)
            {
                Debug.Log("TIME IS UP!");
                timerRunning = false;

                foreach (var button in answerButtons)
                {
                    button.SetInteractable(false);
                }
            }
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(Mathf.Clamp(currentTimer / 60, 0, Mathf.Infinity));
        int seconds = Mathf.FloorToInt(Mathf.Clamp(currentTimer % 60, 0, Mathf.Infinity));

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private IEnumerator FlashTimerText(Color flashColor)
    {
        for (int i = 0; i < 3; i++)
        {
            timerText.color = flashColor;
            yield return new WaitForSeconds(0.2f);
            timerText.color = defaultTimerColor;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator FlashScoreText(Color flashColor)
    {
        for (int i = 0; i < 3; i++)
        {
            scoreText.color = flashColor;
            yield return new WaitForSeconds(0.2f);
            scoreText.color = defaultScoreColor;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
