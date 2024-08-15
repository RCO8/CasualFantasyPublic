using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dungeon8_Portal : MonoBehaviour
{
    [SerializeField] private DungeonLogical DungeonSystem;
    [SerializeField] private TilemapCollider2D _trigger;
    public int NowFloor = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            if (DungeonSystem.thisFloor == NowFloor)
            {
                if (NowFloor == 1)
                {
                    _trigger.enabled = false;
                    DungeonSystem.ShowFloor(2);
                }
                else
                {
                    _trigger.enabled = true;
                    DungeonSystem.HideFloor(2);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (DungeonSystem.thisFloor == 2)
            _trigger.enabled = true;
        DungeonSystem.thisFloor = NowFloor;
    }
}
