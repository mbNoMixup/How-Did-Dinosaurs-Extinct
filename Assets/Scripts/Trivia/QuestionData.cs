using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Question", order = 1)]
public class QuestionData : ScriptableObject
{
    public string question;
    public string category;
    public string[] answers;
}
