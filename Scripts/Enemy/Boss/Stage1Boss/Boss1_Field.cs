using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1_Field : Enemy_Field
{
    private void Start()
    {
        //클리어하고 나면 사라지게(비활성)
        if (NPCDataManager.instance.npcDataDictionary[npcSO.id].isClear)
            gameObject.SetActive(false);
    }
}
