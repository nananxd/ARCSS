using UnityEngine;

public class AssesmentSaveManager : MonoBehaviour
{
    public ArTopics topic;
    public string assesmentName;
    public int score;

    public SaveData saveFile;
    public PlayerSaveData currentSelectedPlayer;
    private void Awake()
    {
        LoadFile();
    }

    public void CompleteAssesment()
    {
        var progress = currentSelectedPlayer.assesment.Find(x => x.assesmentName == assesmentName);
        if (progress == null)
        {
            progress = new AssesmentProgressData
            {
                assesmentName = this.assesmentName,
                topic = this.topic.ToString(),
                score = 0,
                isCompleted = true

            };

            currentSelectedPlayer.assesment.Add(progress);
        }
        else
        {
            progress.isCompleted = true;
        }
        Save();
       

        
    }

    public void Save()
    {
        GeneralUtility.SaveFile(saveFile);
    }

    public void LoadFile()
    {
        saveFile = GeneralUtility.LoadFile<SaveData>();
        if (saveFile == null)
        {
            saveFile = new SaveData();
        }
    }
}
