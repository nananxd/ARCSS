using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TopicAssesmentUI : MonoBehaviour
{
    public TextMeshProUGUI detailsTxt;
    public string details;
    public int score;
    public int items;
    public bool isAssesment;

    public void Setup(string detail, bool isAssesment, int currentScore = 0,int itemCount = 0)
    {
        details = detail;
        score = currentScore;
        items = itemCount;
        this.isAssesment = isAssesment;

        if (this.isAssesment)
        {
            detailsTxt.text = $"{details} | {score}";
        }
        else
        {
            detailsTxt.text = $"{details}";
        }
       
    }
}
