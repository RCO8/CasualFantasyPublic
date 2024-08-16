<h2>게임 데이터 와이어프레임</h2>

![와이어프레임](https://github.com/user-attachments/assets/cb83e4b5-9d52-4a65-b15c-44054a6d2d80)

기술 구조
GameManager에서 JsonControll을 필드로 생성해 저장하는 메서드를 만들어
저장버튼에 연결하고 클릭하면 "Save0, Save1, ... "의 json파일이 생성된다.

![image](https://github.com/user-attachments/assets/a66d5103-3340-4fc1-a121-775f3312048f)

⚠️트러블슈팅
* 에디터에서 실행할 때 저장파일은 생성됐으나 빌드하고나서 실행하더니 저장이 제대로 되지 않았다.

❗해결방법
* 유니티 로그뷰를 따로 가져와 실행했더니 필드에서 basePlayer를 BasePlayer라는 클래스 형식으로 받아서 생성자 안에 <br>
  CharacterManager의 basePlayer라는 속성을 가져왔는데, Null이 나와서 저장이 이루어지지 않았다. 이유는 타이틀에서 시작해 필드로 넘어가는데 CharacterManager의 여부가 모호한 현상이 나타나는 이유였다. <br>
  그래서 필드에서 자유롭게 데이터를 불러오기 위해 CharacterManager에 있는 fieldPlayer 오브젝트의 좌표값을 가져와 저장하도록 해결되었다. <br>

💡이 기술에 대한 활용방안
* 이전 프로젝트 기간에서도 활용했지만, 나중에 사이드 프로젝트에서도 데이터 동기화, 관리등을 활용할 수 있다.
* 알기로는 Json이 멀티플랫폼까지 지원할 수 있는 걸로 알고 있어서 활용도가 유용할 것이다.

#코드 샘플
```
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
            gameData.Enemies.Add(new ClearEnemy(dat.Key, dat.Value.isClear));
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
```
