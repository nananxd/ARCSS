using UnityEngine;
using UnityEngine.UI;

public class BadgesUI : MonoBehaviour
{
    public Sprite moduleSprite,assesmentSprite;
    public GameObject moduleFinishedIcon;
    public Color normalColor, finishedColor;
    public Image badgeImage;

    public string moduleNameId;
    public string quizId;
    public bool isAssesment;
    public bool isDoneReading;

    public void Initialize()
    {
        if (isAssesment)
        {
            badgeImage.sprite = assesmentSprite;
            badgeImage.color = isDoneReading ? finishedColor : normalColor;
        }
        else
        {
            badgeImage.sprite = moduleSprite;
            moduleFinishedIcon.SetActive(isDoneReading? true:false);
        }
    }

    public void Setup(bool assesment,bool isDoneReading,string moduleName = "",string quizId = "")
    {
        isAssesment = assesment;
        this.isDoneReading = isDoneReading;
        moduleNameId = moduleName;
        this.quizId = quizId;
    }

    public void SetColor(bool isFinished)
    {
        badgeImage.color = isFinished ? finishedColor : normalColor;
    }
}
