using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ContentUI : MonoBehaviour,IInitializer
{
    [SerializeField] private Button view3DBtn;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    [SerializeField] private RectTransform prevImageParent;
    [SerializeField] private Image previewImage;
    [SerializeField] private Sprite prevSprite;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    public ModelsName model3dName;
    public bool has3dView;


    public void Setup(PhotoTextContent data)
    {
        descriptionTxt.text = $"{data.titleName}\n{data.description}";
        has3dView = data.has3dView;
        model3dName = data.modelName;
        view3DBtn.gameObject.SetActive(has3dView ? true : false) ;
        foreach (var item in data.display)
        {
            sprites.Add(item);
        }

        for (int i = 0; i < sprites.Count; i++)
        {
            var imgGO = Instantiate(previewImage.gameObject);
            imgGO.transform.SetParent(prevImageParent,false);
            imgGO.SetActive(true);

            Image contentImg = imgGO.GetComponent<Image>();
            if (contentImg != null)
            {
                contentImg.sprite = sprites[i];
            }
        }
    }

    public void Setup(string title, string description, Sprite preview, bool has3d)
    {
        descriptionTxt.text = $"{title}/n{description}";
        prevSprite = preview;
        previewImage.sprite = preview;
        has3dView = has3d;
        if (has3dView)
        {
            view3DBtn.enabled = true;
        }
        else
        {
            view3DBtn.enabled = false;
        }

    }

    public void Initialize()
    {
        view3DBtn.onClick.AddListener(OnViewIn3D);
    }

    public void OnViewIn3D()
    {
        GameManager.instance.uiManager.Open3DView();
        GameManager.instance.modelController.GetModel(model3dName);
    }
}
