using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public TMP_Dropdown loginNameDropdown;

    [Header("Login")]
    public GameObject loginAccountPrefab;
    public RectTransform loginAccountParent;
    public Image selectedAccountPreview;
    public TextMeshProUGUI selectedAccountTxt;

    [Header("Top area account profile")]
    public RectTransform headerAccountRect;
    public TextMeshProUGUI accountNameTxt;
    public Image accountHeaderPreview;
    public Sprite noAccountSprite;



    private void Awake()
    {
        LoadNames();
    }

    private void Start()
    {
        nameInput.onValueChanged.AddListener(SetName);
        saveBtn.onClick.AddListener(SubmitAccountCreation);
        
       
        
        StartCoroutine(DelayInit());
    }

    private IEnumerator DelayInit()
    {
        yield return new WaitForSeconds(1f);
        //LoadAccount();
        LoadAllAccount();
        //loginNameDropdown.onValueChanged.AddListener(SetCurrentPlayer);
    }

    public void LoadAllAccount()
    {
        foreach (var player in GameManager.instance.saveFile.players)
        {
            var playerGo = Instantiate(loginAccountPrefab);
            playerGo.transform.SetParent(loginAccountParent, false);
            playerGo.SetActive(true);

            LoginAccountUI accountUI =playerGo.GetComponent<LoginAccountUI>();
            if (accountUI != null)
            {
                accountUI.Setup(player.playerName,player.playerId,player.profilePhotoName);
                accountUI.Initialize();
            }
        }
    }

    #region Top/Header Profile
    public void SetHeaderProfile()
    {
        headerAccountRect.localScale = Vector3.one;
        var currentSelectedPlayer = GameManager.instance.currentSelectedPlayer;
        if (currentSelectedPlayer != null)
        {
            accountNameTxt.text = currentSelectedPlayer.playerName;
            accountHeaderPreview.sprite = GetProfilePhoto(currentSelectedPlayer.profilePhotoName);
           
        }
        else
        {
            accountNameTxt.text= string.Empty;
            accountHeaderPreview.sprite = noAccountSprite;
        }

    }

    public void CloseHeaderProfile()
    {
        headerAccountRect.localScale = Vector3.zero;
    }

    #endregion

    public void SetSelectedAccount(string userName, string profileAvatarName)
    {
        selectedAccountTxt.text = userName;
        selectedAccountPreview.sprite = GetProfilePhoto(profileAvatarName);
      

    }
    public void LoadAccount()
    {
        loginNameDropdown.ClearOptions();
        
        List<string> names = new List<string>();
        for (int i = 0; i < GameManager.instance.saveFile.players.Count; i++)
        {
            var currentName = GameManager.instance.saveFile.players[i];
            names.Add(currentName.playerName);
        }
        loginNameDropdown.AddOptions(names);

        if (GameManager.instance.saveFile.players.Count == 1)
        {
            SetCurrentPlayer(0);
        }
    }

    public void SetName(string name)
    {
        playerNameText.text = name;
        playerName = name;
    }

    public void SetCurrentPlayer( int index)
    {
        var currentPlayerProfile = GameManager.instance.saveFile.players[index];
        Debug.Log($"index:{index}");
        GameManager.instance.currentSelectedPlayer = currentPlayerProfile;

    }

    public void SetCurrentPlayer(string guid)
    {
        var currentPlayerProfile = GameManager.instance.saveFile.players.Find( x => x.playerId == guid);
        if (currentPlayerProfile != null)
        {
            GameManager.instance.currentSelectedPlayer = currentPlayerProfile;
            PlayerPrefs.SetString(GeneralUtility.LOCAL_SAVE_PLAYERID,currentPlayerProfile.playerId);
            SetHeaderProfile();
        }
    }

    public void LoadLocalSavePlayer()
    {
       var saveID = PlayerPrefs.GetString(GeneralUtility.LOCAL_SAVE_PLAYERID);
        SetCurrentPlayer(saveID);
    }

    public void SetProfilePhoto(string photoName)
    {
        profileImage.sprite = avatarSprites.Find(x => x.name == photoName);
        profileImage.transform.localScale = Vector3.one;
        avatarName = photoName;
    }

    public Sprite GetProfilePhoto(string photoName)
    {
       var foundPhoto = avatarSprites.Find(x => x.name == photoName);
        //foundPhoto.transform.localScale = Vector3.one;
        //avatarName = photoName;
        return foundPhoto;
    }

    public void SubmitAccountCreation()
    {
        if (!string.IsNullOrEmpty(avatarName) && !string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Account Created");
            GameManager.instance.CreatePlayer();
            GameManager.instance.uiManager.ShowAccountCreatePopup(true);
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
