using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizData", menuName = "Scriptable Objects/QuizData")]
public class QuizData : ScriptableObject
{
    public QuizId quizId;
    public List<Quiz> quizzes;
}



[System.Serializable]
public class Quiz
{
    public string question;
    public List<QuizChoices> choices;
}


[System.Serializable]
public class QuizChoices
{
    public ChoiceLetter letter;
    public string answer;
    public bool isCorrectAnswer;
}

public enum QuizId
{
    None,
    ComputerSystem1,
    ComputerSystem2, 
    ComputerSystem3,
}
