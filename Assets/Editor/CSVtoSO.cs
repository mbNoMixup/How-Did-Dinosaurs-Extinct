using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVtoSO
{
    private static string questionsCSVPath = "/Editor/CSVs/Questions.csv";
    private static int numberOfAnswers = 4;

    [MenuItem("Utilities/Generate Questions")]
    public static void GenerateQuestions()
    {
        Debug.Log("Generated Questions");
        string[] allLines = File.ReadAllLines(Application.dataPath + questionsCSVPath);

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(';');

            QuestionData questionData = ScriptableObject.CreateInstance<QuestionData>();
            questionData.question = splitData[0];
            questionData.category = splitData[1];

            questionData.answers = new string[4];

            for (int i = 0; i < numberOfAnswers; i++)
            {
                questionData.answers[i] = splitData[2 + i];
            }

            if (questionData.question.Contains("?"))
            {
                questionData.name = questionData.question.Remove(questionData.question.IndexOf("?"));
            }
            else
            {
                questionData.name = questionData.question;
            }

            AssetDatabase.CreateAsset(questionData, $"Assets/Resources/Questions/{questionData.name}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
