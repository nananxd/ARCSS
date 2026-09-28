using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MultipleChoiceManager : MonoBehaviour
{
    public QuizData currentQuiz;
    public GameObject questionPrefab;
    public RectTransform parent;
    [SerializeField] private Button submitAnswer;
    [SerializeField] private List<MultipleChoiceController> choicesController = new List<MultipleChoiceController>();
    [SerializeField] private List<QuizData> quizzes = new List<QuizData>();


    private void Awake()
    {
        submitAnswer.onClick.AddListener(OnSubmitAnswers);
    }

    private void Start()
    {
        InitializeQuestions();
    }

    #region GetQuiz
    public void GetQuizById(QuizId id)
    {
        currentQuiz = quizzes.Find( x => x.quizId == id);
        //if (currentQuiz != null)
        //{
        //    return currentQuiz;
        //}

        //return null;
    }

    #endregion

    public void InitializeQuestions()
    {
        for (int i = 0; i < currentQuiz.quizzes.Count; i++)
        {
            GameObject go = Instantiate(questionPrefab);
            go.transform.SetParent(parent);
            go.SetActive(true);
            MultipleChoiceController choiceControl = go.GetComponent<MultipleChoiceController>();
            choicesController.Add(choiceControl);
            choiceControl.Setup(currentQuiz.quizzes[i]);
        }
    }

    public void ClearList()
    {
        foreach (var item in choicesController)
        {
            Destroy(item);
        }

        choicesController.Clear();
    }

    public int CheckAnswers()
    {
        var count = choicesController.Count(x => x.IsCorrectAnswer());
        return count;
    }

    public void OnSubmitAnswers()
    {
        Debug.Log(CheckAnswers().ToString());
        GameManager.instance.uiManager.SetResultUI(CheckAnswers(),choicesController.Count);
        GameManager.instance.uiManager.ShowResultScreen();

        // save score 

    }
}
