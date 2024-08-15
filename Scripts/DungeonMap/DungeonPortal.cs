using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonPortal : MonoBehaviour
{
    [SerializeField] private DungeonLogical DungeonSystem;
    private TilemapCollider2D _trigger;
    public int NowFloor = 1;
    public int NextFloor;

    private void Awake()
    {
        _trigger = GetComponent<TilemapCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            if(DungeonSystem.thisFloor == NowFloor)
                DungeonSystem.ChangeFloor(NowFloor, NextFloor);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DungeonSystem.thisFloor = NowFloor;
    }
}
