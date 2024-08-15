using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private Player_Battle _player;
    private Enemy _enemy;

    public List<GameObject> BackgroundAssets;

    public void Start()
    {
        _enemy = CharacterManager.instance.enemy;   //제대로 들어갔는지 확인
        _player = CharacterManager.instance.basePlayer as Player_Battle;


        SettingBackGround(SceneDataManager.instance.SceneName);
    }

    private void SettingBackGround(string _name)
    {
        for (int i = 0; i < BackgroundAssets.Count; i++)
            BackgroundAssets[i].SetActive(false);

        if (_name.Contains("Dungeon"))
        {
            if(_name.Contains("5") || _name.Contains("6"))
                BackgroundAssets[7].SetActive(true);
            else if (_name.Contains("3") || _name.Contains("7"))
                BackgroundAssets[6].SetActive(true);
            else
                BackgroundAssets[5].SetActive(true);
        }
        else
        {
            if(_name.Contains("5"))
                BackgroundAssets[4].SetActive(true);
            else if(_name.Contains("4"))
                BackgroundAssets[3].SetActive(true);
            else if(_name.Contains("3"))
                BackgroundAssets[2].SetActive(true);
            else if(_name.Contains("2"))
                BackgroundAssets[1].SetActive(true);
            else
                BackgroundAssets[0].SetActive(true);
        }
    }
}
