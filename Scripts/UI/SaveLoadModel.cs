using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadModel
{
    public string[] SaveFilePaths { get; private set; }

    public SaveLoadModel()
    {
        SaveFilePaths = new string[]
        {
            Path.Combine(Application.persistentDataPath, "save_1.json"),
            Path.Combine(Application.persistentDataPath, "save_2.json"),
            Path.Combine(Application.persistentDataPath, "save_3.json"),
            Path.Combine(Application.persistentDataPath, "save_4.json")
        };
    }

    public void SaveData(int slotIndex, PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SaveFilePaths[slotIndex], json);
    }

    public PlayerData LoadData(int slotIndex)
    {
        string filePath = SaveFilePaths[slotIndex];
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        return null;
    }
}