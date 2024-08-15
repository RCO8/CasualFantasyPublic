using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class NPCData
{
    //�ӽ÷� ���� (��ųʸ��� Ȱ���ϴ°� ����ȭ�鿡�� �� ����)
    //public int iD;
    public GameObject prefab;
    public bool isClear = false;
}

public class NPCDataManager : MonoBehaviour
{
    public static NPCDataManager instance { get; set; }

    [SerializedDictionary("ID", "Data")]
    public SerializedDictionary<int, NPCData> npcDataDictionary;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    //private void Start()
    //{
    //    //���� �ε��ϸ� ��ųʸ� ���� �����Ϳ� �°� �ʱ�ȭ�ϴ� �۾�
    //}
}
