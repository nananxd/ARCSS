using System.IO;
using UnityEngine;

public static class GeneralUtility
{
    public static string CURRENTPLAYERID;
    public static string PATH = Application.persistentDataPath + "/save.json";
    public static void SaveFile<T>(T data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(PATH, json);
        Debug.Log($"SAVE:{json}");
    }

    public static T LoadFile<T>()
    {
        if (!File.Exists(PATH)) return default;
        string json = File.ReadAllText(PATH);
        return JsonUtility.FromJson<T>(json);
    }

    public static void SaveAssesmentProgress(PlayerSaveData currentSelectedPlayer,string assesmentName,ArTopics topic,int currentScore = 0)
    {
        var progress = currentSelectedPlayer.assesment.Find(x => x.assesmentName == assesmentName);
        if (progress == null)
        {
            progress = new AssesmentProgressData
            {
                assesmentName = assesmentName,
                topic = topic.ToString(),
                score = currentScore,
                isCompleted = true

            };

            currentSelectedPlayer.assesment.Add(progress);
        }
        else
        {
            progress.isCompleted = true;
        }
    }
}
