using UnityEngine;
using UnityEngine.UI;

public class ProfilePhotoUI : MonoBehaviour
{
    public PhotoName photoName;
    [SerializeField] private Button profilePhtoBtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        profilePhtoBtn.onClick.AddListener(OnSelectAvatarUI);
    }

   

    public void OnSelectAvatarUI()
    {
        GameManager.instance.accountManager.SetProfilePhoto(photoName.ToString().ToLower());
    }
}

public enum PhotoName
{
    None,
    Avatar1,
    Avatar2,
    Avatar3,
    Avatar4,
    Avatar5
}
