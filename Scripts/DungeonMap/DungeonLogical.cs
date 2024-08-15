using System.Collections.Generic;
using UnityEngine;

public class DungeonLogical : MonoBehaviour
{
    [SerializeField] private List<GameObject> NowFloor;
    public int thisFloor { get; set; }

    private void Start()
    {
        thisFloor = 1;  //배틀 전환시 현재 층을 그대로 물고 올것

        for (int i = 0; i < NowFloor.Count; i++)
            NowFloor[i].SetActive(i == (thisFloor - 1));
    }

    public void ChangeFloor(int _now, int _next)
    {
        NowFloor[_next-1].SetActive(true);
        NowFloor[_now-1].SetActive(false);
    }

    //Dungeon8 전용 메서드
    public void ShowFloor(int _now)
    {
        NowFloor[_now - 1].SetActive(true);
    }
    public void HideFloor(int _now)
    {
        NowFloor[_now - 1].SetActive(false);
    }
}
