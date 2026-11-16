using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;


public class ModelNameEditor : EditorWindow
{
    private string partName = "";

    private const string enumFilePath =
        "Assets/Scripts/Scriptable/QuizData.cs";

    private const string enumName = "QuizId";

    private List<string> existingParts = new List<string>();

    [MenuItem("Tools/Model Name Manager")]
    public static void ShowWindow()
    {
        GetWindow<QuizEditor>(
            "Model Name Manager"
        );
    }

    private void OnEnable()
    {
        LoadParts();
    }

    private void OnGUI()
    {
        GUILayout.Label(
            "Model Name Manager",
            EditorStyles.boldLabel
        );

        EditorGUILayout.Space(10);

        if (!File.Exists(enumFilePath))
        {
            EditorGUILayout.HelpBox(
                "Could not find the existing enum script.",
                MessageType.Error
            );

            return;
        }

        partName = EditorGUILayout.TextField(
            "New Model Name",
            partName
        );

        EditorGUILayout.Space(5);

        if (GUILayout.Button("Add Model Id"))
        {
            AddPart();
        }

        EditorGUILayout.Space(15);

        GUILayout.Label(
            "Existing Model Id",
            EditorStyles.boldLabel
        );

        foreach (string part in existingParts)
        {
            EditorGUILayout.LabelField("• " + part);
        }
    }

    private void LoadParts()
    {
        existingParts.Clear();

        if (!File.Exists(enumFilePath))
            return;

        string source = File.ReadAllText(enumFilePath);

        MatchCollection matches = Regex.Matches(
            source,
            @"\b(\w+)\s*(?:=\s*\d+)?\s*,"
        );

        foreach (Match match in matches)
        {
            existingParts.Add(match.Groups[1].Value);
        }
    }

    private void AddPart()
    {
        string newPart = partName.Trim();

        if (string.IsNullOrEmpty(newPart))
            return;

        if (!Regex.IsMatch(
            newPart,
            @"^[a-zA-Z_][a-zA-Z0-9_]*$"))
        {
            EditorUtility.DisplayDialog(
                "Invalid Name",
                "Please enter a valid C# identifier.",
                "OK"
            );

            return;
        }

        if (existingParts.Contains(newPart))
        {
            EditorUtility.DisplayDialog(
                "Already Exists",
                "This model id already exists.",
                "OK"
            );

            return;
        }

        string source = File.ReadAllText(enumFilePath);

        string pattern =
            @"(enum\s+" + enumName +
            @"\s*\{)([\s\S]*?)(\})";

        Match match = Regex.Match(source, pattern);

        if (!match.Success)
        {
            EditorUtility.DisplayDialog(
                "Error",
                "Could not locate the enum declaration.",
                "OK"
            );

            return;
        }

        string enumContent = match.Groups[2].Value.TrimEnd();

        string updatedEnum =
            match.Groups[1].Value +
            enumContent +
            "\n    " + newPart + ",\n" +
            match.Groups[3].Value;

        source = source.Substring(0, match.Index) +
                 updatedEnum +
                 source.Substring(match.Index + match.Length);

        File.WriteAllText(enumFilePath, source);

        partName = "";

        AssetDatabase.Refresh();

        LoadParts();

        EditorUtility.DisplayDialog(
            "Success",
            newPart + " added successfully!",
            "OK"
        );
    }
}
