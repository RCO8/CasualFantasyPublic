using UnityEngine;
using DG.Tweening;

public class Boss1Stun : Boss1ActionNodeFunction
{
    public Boss1Stun(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(Boss1AIController.EBossState.STUN);
    }

    public INode.ENodeState StunNode()
    {
        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���, �ݶ��̴� Off
        if (isFirst(Define_Boss1AniParam.STUN_BOOL, enemy.bossAnimationData.StunParameterHash, false))
        {
            //�ڷ� �з�����
            KnockBack();

            //���� �ִϸ��̼� ����
            controller.animator.SetBool(enemy.bossAnimationData.RunParameterHash, false);

            //����Ʈ ����
            controller.enemyEffect.ChangeEffectState(EffectState.Stun);
        }

        //2�ʰ� ������
        if (controller.SetTimeAndCheck(2.0f))
        {
            //����Ʈ ���߱�
            controller.enemyEffect.ChangeEffectState(EffectState.None);

            //Stun �ִϸ��̼� ����
            controller.animator.SetBool(enemy.bossAnimationData.StunParameterHash, false);

            //���� ����
            controller.RandomAttackState();

            //�ݶ��̴� On
            enemy.damageCollider.gameObject.SetActive(true);

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }

    //�ڷ� �з������� �Լ�
    public void KnockBack()
    {
        float _x;

        //������
        if(enemy.transform.position.x > 0.0f) _x = enemy.gameObject.transform.position.x - 3.0f;
        //����
        else _x = enemy.gameObject.transform.position.x + 3.0f;

        Vector2 targetPos = new Vector2(_x, enemy.gameObject.transform.position.y);

        enemy.gameObject.transform.DOJump(targetPos, 0.5f, 1, 0.25f);
    }
}
