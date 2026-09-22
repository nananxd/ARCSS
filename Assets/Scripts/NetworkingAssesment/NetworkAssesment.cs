using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NetworkAssesment : MonoBehaviour
{
    [Header("Straigh through sequence")]
    public List<int> correctStraightSequence;
    [Header("Crossover Correct Sequence")]
    public List<int> crossOverCorrectSequence;

    [Header("Straight Through Submitted Answer")]
    public List<int> straightThroughAnswerSequence;
    [Header("Crossover Submitted Answer")]
    public List<int> crossOverAnswerSequence;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckStraightThroughAnswer(correctStraightSequence,straightThroughAnswerSequence);
    }

    
    public bool CheckStraightThroughAnswer(List<int> correctAnswer,List<int> submittedAnswer)
    {
        var isEqual = correctAnswer.SequenceEqual(submittedAnswer);
        Debug.Log(isEqual);
        return isEqual;
    }

    public void CheckCrossOverAnswer()
    {

    }
}
