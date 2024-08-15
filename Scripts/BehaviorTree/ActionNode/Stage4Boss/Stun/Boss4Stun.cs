using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Boss4Stun : Boss4ActionNodeFunction
{
    public Boss4Stun(Boss4_Battle _enemy, Boss4AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(EnemyAIController.EBossState.STUN);
    }

    public INode.ENodeState StunNode()
    {
        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���, �ݶ��̴� Off
        if (isFirst(Define_Boss4AniParam.STUN_BOOL, enemy.bossAnimationData.StunParameterHash, false))
        {
            //�߾����� �з�����
            KnockBack();

            //�ٿ �ִϸ��̼� ����
            controller.animator.SetBool(enemy.bossAnimationData.BounceParameterHash, false);

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
            controller.ChangeState(EnemyAIController.EBossState.STANDBY);

            //�ݶ��̴� On
            enemy.damageCollider.gameObject.SetActive(true);

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }

    //�߾����� �з������� �Լ�
    public void KnockBack()
    {
        Vector2 targetPos = new Vector2(0.0f, -3.8f);

        enemy.gameObject.transform.DOJump(targetPos, 0.5f, 1, 0.25f);
    }
}
