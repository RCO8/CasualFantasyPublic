using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4_Battle : Enemy
{
    //������ �ö�� ��, ��ȯ�Ǵ� �� ������Ʈ
    [SerializeField] private GameObject _digObject;

    //������ �������� Ȯ�ο� ����
    public bool isJumpEnd { get; private set; } = false;


    protected override void Awake()
    {
        base.Awake();

        //ĳ���� �Ŵ��� ����
        CharacterManager.instance.enemy = this;
    }

    public void JumpToPlayer()
    {
        //�÷��̾� ������Ʈ
        GameObject _player = CharacterManager.instance.basePlayer.gameObject;

        //���� ����
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
