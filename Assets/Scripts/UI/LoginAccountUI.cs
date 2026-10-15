using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginAccountUI : MonoBehaviour
{
    public TextMeshProUGUI accountNameTxt;
    public Image accountPreviewImage;
    public Button accountBtn;

    public string accountName;
    public string accountGuid;
    public string avatarName;

    public void Setup(string accountname,string accountguid,string avatarname)
    {
        accountName = accountname;
        accountGuid = accountguid;
        avatarName = avatarname;

        
    }

    public void Initialize()
    {
        accountBtn.onClick.AddListener(OnClickAccount);
        accountNameTxt.text = accountName;
        accountPreviewImage.sprite = GameManager.instance.accountManager.GetProfilePhoto(avatarName);
    }

    public void OnClickAccount()
    {
        GameManager.instance.accountManager.SetCurrentPlayer(accountGuid);
        GameManager.instance.accountManager.SetSelectedAccount(accountName,avatarName);
    }

}
