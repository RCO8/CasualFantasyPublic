using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4_Battle : Enemy
{
    //땅에서 올라올 때, 소환되는 돌 오브젝트
    [SerializeField] private GameObject _digObject;

    //점프가 끝났는지 확인용 변수
    public bool isJumpEnd { get; private set; } = false;


    protected override void Awake()
    {
        base.Awake();

        //캐릭터 매니저 설정
        CharacterManager.instance.enemy = this;
    }

    public void JumpToPlayer()
    {
        //플레이어 오브젝트
        GameObject _player = CharacterManager.instance.basePlayer.gameObject;

        //점프 시작
        float _x = Mathf.Clamp(_player.transform.position.x, Define.BATTLESCENE_LEFTWALL_X - 1.0f,
            Define.BATTLESCENE_RIGHTWALL_X + 0.5f);
        float _y = gameObject.transform.position.y;
        Vector2 targetPos = new Vector2(_x, _y);
        //transform.DOJump(targetPos, 5.0f, 1, _dashAttackTime).OnStart(() => isJumpEnd = false)
        //    .OnComplete(JumpEnd);
        transform.DOJump(targetPos, 5.0f, 1, 0.25f).OnComplete(JumpEnd);
    }

    public void JumpEnd()
    {
        isJumpEnd = true;

        StartCoroutine(IsJumpEnd_Fasle());
    }

    IEnumerator IsJumpEnd_Fasle()
    {
        yield return new WaitForEndOfFrame();
        isJumpEnd = false;
    }
}
