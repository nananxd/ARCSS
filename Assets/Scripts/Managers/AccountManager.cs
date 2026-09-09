using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    [SerializeField] private TextAsset playerNames;
    [SerializeField] private List<string> names = new List<string>();

    [Header("Photo UI")]
    [SerializeField] private List<ProfilePhotoUI> profilePhotoUIs = new List<ProfilePhotoUI>();
    [SerializeField] private List<Sprite> avatarSprites = new List<Sprite>();

    [Header("Create Panel")]
    public Image profileImage;
    public TextMeshProUGUI playerNameText;
    public TMP_InputField nameInput;
    public Button saveBtn;
    public string avatarName;
    public string playerName;

    private void Awake()
    {
        LoadNames();
    }

    private void Start()
    {
        nameInput.onValueChanged.AddListener(SetName);
        saveBtn.onClick.AddListener(SubmitAccountCreation);
    }

    public void SetName(string name)
    {
        playerNameText.text = name;
        playerName = name;
    }

    public void SetProfilePhoto(string photoName)
    {
        profileImage.sprite = avatarSprites.Find(x => x.name == photoName);
        profileImage.transform.localScale = Vector3.one;
        avatarName = photoName;
    }

    public void SubmitAccountCreation()
    {
        if (!string.IsNullOrEmpty(avatarName) && !string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Account Created");
            GameManager.instance.CreatePlayer();
        }
    }

    private void LoadNames()
    {
        string[] lines = playerNames.text.Split(new[] { '\r','\n'},System.StringSplitOptions.RemoveEmptyEntries);
        names.AddRange(lines);
    }

    public string GenerateName()
    {
        if (names.Count == 0)
        {
            return "Player";
        }

        return names[Random.Range(0,names.Count)].Trim();
    }
    public void CreateAccount()
    {

    }

   public void LoginAccount()
   {

   }
}
