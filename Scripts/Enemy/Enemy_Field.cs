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

        exclamationMarkSprite.gameObject.SetActive(true);               //����ǥ�� ����.
        LookPlayer(CharacterManager.instance.basePlayer.gameObject);    //�÷��̾ �Ĵٺ���.



        //��Ʋ ������ ����
        SceneDataManager.instance.EnterBattleScene(npcSO.id);
    }
}
