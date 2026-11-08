using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class NetworkAssesment : MonoBehaviour
{
    [Header("Straigh through sequence")]
    public List<int> correctStraightSequence;
    public List<int> correctStraightSequence2;
    [Header("Crossover Correct Sequence")]
    public List<int> crossOverCorrectSequence;
    public List<int> crossOverCorrectSequence2;

    [Header("Straight Through Submitted Answer")]
    public List<int> straightThroughAnswerSequence;
    
    [Header("Crossover Submitted Answer")]
    public List<int> crossOverAnswerSequence;

    public List<int> selectedSequence = new List<int>();
    public List<int> selectedSequence2 = new List<int>();
         
    public int count = 0;
    public LanWireInteraction firstSelected,secondSelected;
    public RJ45Interaction firstRj45, secondRj45;


    [Header("Object References")]
    [SerializeField] private LanWireInteraction [] lanObjects;
    [SerializeField] private LanWireInteraction[] lanObjects2;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckStraightThroughAnswer(correctStraightSequence,straightThroughAnswerSequence);
    }

    public void EnableRj45Collider(bool isEnable)
    {
        firstRj45.rj45Collider.enabled = isEnable;
        secondRj45.rj45Collider.enabled = isEnable;

        firstRj45.EnableCollider(!isEnable);
        secondRj45.EnableCollider(!isEnable);

    }

    public void SetLanwire(LanWireInteraction lanwire)
    {
        if (firstSelected == null)
        {
            firstSelected = lanwire;
            firstSelected.SetLanOutline(true);
            return;
        }

        if (firstSelected == lanwire)
        {
           
            firstSelected = null;

            return;
        }

        secondSelected = lanwire;
        secondSelected.SetLanOutline(true);
        //StartCoroutine(DelaySwap());
        Swap();
    }

    private IEnumerator DelaySwap()
    {
        yield return new WaitForSeconds(1f);

        firstSelected.SetLanOutline(false);
        secondSelected.SetLanOutline(false);

        firstSelected = null;
        secondSelected = null;
        
    }

    public void Swap()
    {
        Vector3 firstPosition = firstSelected.gameObject.transform.position;

        TweenHelper.Move(firstSelected.transform,secondSelected.transform.position);
        TweenHelper.Move(secondSelected.transform, firstPosition);

        StartCoroutine(DelaySwap());
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


        int[] currentSequence2 = lanObjects2
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
                selectedSequence2 = correctStraightSequence2;
                break;
            case Mode.CROSSOVER:
                selectedSequence = crossOverCorrectSequence;
                selectedSequence2 = crossOverCorrectSequence2;
                break;
            default:
                break;
        }

        // Compare sequences
        bool isCorrect = currentSequence.SequenceEqual(selectedSequence);
        bool isCorrect2 = currentSequence2.SequenceEqual(selectedSequence2);

        if (isCorrect && isCorrect2)
        {
            Debug.Log("Correct Sequence!");
            NetworkAssesmentManager.Instance.uiManager.OnGameFinished(false);
        }
        else
        {
            Debug.Log("Incorrect Sequence!");
            NetworkAssesmentManager.Instance.uiManager.OnGameFinished(true);
        }
    }
}
