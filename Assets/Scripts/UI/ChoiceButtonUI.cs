using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ChoiceButtonUI : MonoBehaviour
{
    [SerializeField] private MultipleChoiceController choiceController;
    public ChoiceLetter letter;
    public string answer;
    public bool isCorrectAnswer;
    [SerializeField] private TextMeshProUGUI displayTxt;
    [SerializeField] private Button choiceBtn;
    [SerializeField] private Image btnBg;
    [SerializeField] private Color selectedColor, defaultColor;


    public void Setup(MultipleChoiceController choiceController,ChoiceLetter currentLetter,string ans,bool isCorrect)
    {
        choiceBtn = GetComponent<Button>();
        displayTxt = GetComponentInChildren<TextMeshProUGUI>();
        choiceBtn.onClick.AddListener(SubmitAnswer);
        btnBg.color = defaultColor;
        choiceBtn.image.color = defaultColor;

        this.choiceController = choiceController;
        letter = currentLetter;
        answer = ans;
        isCorrectAnswer = isCorrect;
        displayTxt.text = answer;
    }
    
    public void SubmitAnswer()
    {
        btnBg.color = selectedColor;
        choiceBtn.image.color = selectedColor;
        choiceController.SetChoiceButtonUI(this);
        choiceController.SubmitAnswer(this);
    }

    public void ResetColor()
    {
        btnBg.color = defaultColor;
        choiceBtn.image.color = defaultColor;
    }
}
