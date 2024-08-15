using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class JsonControll
{
    private NPCDataManager NPCs;
    private GameData gameData = new GameData();
    private GameObject basePlayer;
    private Vector2 playerPos;

    string path = Application.persistentDataPath;

    public JsonControll()
    {
        basePlayer = CharacterManager.instance.fieldPlayer;
        NPCs = NPCDataManager.instance;
    }

    private void SaveData()
    {
        gameData.SceneName = SceneManager.GetActiveScene().name.ToString();
        playerPos = basePlayer.transform.position;
        gameData.Position = playerPos;

        foreach (var dat in NPCs.npcDataDictionary)
        {
            //Debug.Log($"{dat.Key}, {dat.Value.isClear}");
            gameData.Enemies.Add(new ClearEnemy(dat.Key, dat.Value.isClear));
        }
    }

    private void LoadData()
    {
        //이거는 다른 씬에서 파일을 불러올 경우 다른 씬으로 이동되는 경우가 있어서
        SceneDataManager.instance.NextFieldScene(gameData.SceneName, gameData.Position);

        foreach (var dat in gameData.Enemies)
            NPCs.npcDataDictionary[dat.ID].isClear = dat.Clear;
    }

    public bool SaveFile(int idx)
    {
        try
        {
            SaveData();
            var json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(Path.Combine(path, $"Save{idx}.json"), json);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool LoadFile(int idx)
    {
        try
        {
            var json = File.ReadAllText(Path.Combine(path, $"Save{idx}.json"));
            gameData = JsonUtility.FromJson<GameData>(json);
            LoadData();
            return true;
        }
        catch
        {
            return false;
        }
    }
}

[System.Serializable]
public class GameData
{
    public string SceneName;
    public Vector2 Position;

    //쓰러트린 적
    public List<ClearEnemy> Enemies = new List<ClearEnemy>();

    public int completedQuests; // 완료된 퀘스트 수
}

[System.Serializable]
public class ClearEnemy
{
    public int ID;
    public bool Clear;

    public ClearEnemy(int _id, bool _clear)
    {
        ID = _id;
        Clear = _clear;
    }
}