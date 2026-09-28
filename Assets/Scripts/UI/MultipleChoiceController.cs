using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

public class MultipleChoiceController : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public List<ChoiceButtonUI> choicesBtns;

    public string correctAnswer;
    public ChoiceLetter correctLetter;


    public string submittedAnswer;
    public ChoiceLetter submittedLetter;
    public bool isCorrect;

    public ChoiceButtonUI submittedChoice;
   
    public void Setup(Quiz data)
    {
        questionText.text = data.question;
        for (int i = 0; i < choicesBtns.Count; i++)
        {
            var currentBtn = choicesBtns[i];
            var currentQuizChoice = data.choices[i];

            currentBtn.Setup(this,currentQuizChoice.letter,currentQuizChoice.answer,currentQuizChoice.isCorrectAnswer);
        }

    }

    public void SetChoiceButtonUI(ChoiceButtonUI choice)
    {
        foreach (var item in choicesBtns)
        {
            if (item != choice)
            {
                item.ResetColor();
            }
        }
    }


    public void SubmitAnswer(ChoiceLetter letter,string answer,bool isCorrectAnswer)
    {
        submittedAnswer = answer;
        submittedLetter = letter;
    }

    public void SubmitAnswer(ChoiceButtonUI choice)
    {
        submittedChoice = choice;
    }

    public bool IsCorrectAnswer()
    {
        return submittedChoice.isCorrectAnswer;
    }
}

public enum ChoiceLetter
{
    A,
    B,
    C,
    D
}
