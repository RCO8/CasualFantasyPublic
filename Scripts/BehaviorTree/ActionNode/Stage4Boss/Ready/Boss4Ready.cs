using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Ready : Boss4ActionNodeFunction
{
    public Boss4Ready(Boss4_Battle _enemy, Boss4AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(EnemyAIController.EBossState.READY);
    }

    public INode.ENodeState ReadyNode()
    {
        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���
        isFirst(Define_Boss4AniParam.READY_BOOL, enemy.bossAnimationData.IdleParameterHash);

        //2�ʸ� ���. 2�ʰ� ������ true�� ��ȯ�Ѵ�.
        if (controller.SetTimeAndCheck(2.0f))
        {
            //Idle �ִϸ��̼� ����
            controller.animator.SetBool(enemy.bossAnimationData.IdleParameterHash, false);

            //���� ����
            controller.RandomAttackState();

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }
}
