using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Field : NPC
{
    public Animator animator;
    public EnemyAnimationData bossAnimationData;
    public SpriteRenderer exclamationMarkSprite;

    protected override void Awake()
    {
        base.Awake();

        bossAnimationData = new EnemyAnimationData();
        bossAnimationData.Initialize();
    }

    public override void NPCInteraction()
    {
        //base.NPCInteraction();

        exclamationMarkSprite.gameObject.SetActive(true);               //느낌표를 띄운다.
        LookPlayer(CharacterManager.instance.basePlayer.gameObject);    //플레이어를 쳐다본다.



        //배틀 씬으로 진입
        SceneDataManager.instance.EnterBattleScene(npcSO.id);
    }
}
