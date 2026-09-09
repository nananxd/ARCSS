using NUnit.Framework;
using System.Collections.Generic;
using Unity.Multiplayer.PlayMode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera modelCamera;

    [Header("Managers")]
    public TopicManager topicManager;
    public UIManager uiManager;
    public AccountManager accountManager;

    [Header("Controller")]
    public ModelController modelController;
    public ModuleContentController moduleContentController;

    public SaveData saveFile;
    public PlayerSaveData currentSelectedPlayer;
    private void Awake()
    {
        instance = this;
        LoadFile();
    }

    private void Start()
    {
        //CreatePlayer();
    }

    public void CreatePlayer()
    {
        PlayerSaveData player = new PlayerSaveData();
        //string playerName = accountManager.GenerateName();

        player.playerId = System.Guid.NewGuid().ToString();
        player.playerName = accountManager.playerName;
        player.profilePhotoName = accountManager.avatarName;

        saveFile.players.Add(player);

        currentSelectedPlayer = player;


        Save();
    }

    public bool SelectPlayer(string playerID)
    {
        PlayerSaveData player = saveFile.players.Find(x => x.playerId == playerID);

        if (player == null)
            return false;

        currentSelectedPlayer = player;

        return true;
    }

    public void Save()
    {
        GeneralUtility.SaveFile(saveFile);
    }

    public void LoadFile()
    {
        saveFile = GeneralUtility.LoadFile<SaveData>();
        if (saveFile ==null)
        {
            saveFile = new SaveData();
        }
    }
}




[System.Serializable]
public class PlayerData
{
    public string playerName;
    public string playerId;

}

[System.Serializable]
public class AssesmentSaveData
{

}

[System.Serializable]
public class ModuleSaveData
{
    public string topicName;
    public List<bool> completedModule = new List<bool>();
}
