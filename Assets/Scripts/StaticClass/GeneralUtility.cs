using System.IO;
using UnityEngine;

public static class GeneralUtility
{
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
}
