using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

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

    public List<int> selectedSequence = new List<int>();

    public int count = 0;
    public LanWireInteraction firstSelected,secondSelected;


    [Header("Object References")]
    [SerializeField] private LanWireInteraction [] lanObjects;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckStraightThroughAnswer(correctStraightSequence,straightThroughAnswerSequence);
    }

    public void SetLanwire(LanWireInteraction lanwire)
    {
        if (firstSelected == null)
        {
            firstSelected = lanwire;
            return;
        }

        if (firstSelected == lanwire)
        {
           
            firstSelected = null;

            return;
        }

        secondSelected = lanwire;
        //StartCoroutine(DelaySwap());
        Swap();
    }

    private IEnumerator DelaySwap()
    {
        yield return new WaitForSeconds(1f);
        Swap();
    }

    public void Swap()
    {
        Vector3 firstPosition = firstSelected.gameObject.transform.position;

        //firstSelected.gameObject.transform.position =  secondSelected.transform.position;
        //secondSelected.gameObject.transform.position =  firstPosition;

        TweenHelper.Move(firstSelected.transform,secondSelected.transform.position);
        TweenHelper.Move(secondSelected.transform, firstPosition);

        firstSelected = null;
        secondSelected = null;
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


    public void CheckSequence()
    {
        // Get current object IDs based on their positions
        int[] currentSequence = lanObjects
            .OrderBy(obj => obj.transform.position.x)
            .Select(obj => obj.lanId)
            .ToArray();

        // Display current sequence
        Debug.Log("Current Sequence: " +
            string.Join("", currentSequence));

      
        switch (NetworkAssesmentManager.Instance.mode)
        {
            case Mode.NONE:
                break;
            case Mode.STRAIGHTTHROUGH:
                selectedSequence = correctStraightSequence;
                break;
            case Mode.CROSSOVER:
                selectedSequence = crossOverCorrectSequence;
                break;
            default:
                break;
        }

        // Compare sequences
        bool isCorrect = currentSequence .SequenceEqual(selectedSequence);

        if (isCorrect)
        {
            Debug.Log("Correct Sequence!");
        }
        else
        {
            Debug.Log("Incorrect Sequence!");
        }
    }
}
