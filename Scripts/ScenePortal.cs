using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Reporting;
//using UnityEditor.SearchService;
using UnityEngine;

public class ScenePortal : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private Vector2 SpawnPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            SceneDataManager.instance.NextFieldScene(SceneName, SpawnPos);
            //씬은 이동하되 캐릭터 위치는 잡고 전환할 것
        }
    }
}
