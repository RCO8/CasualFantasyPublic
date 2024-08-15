using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StandBy : Boss1ActionNodeFunction
{
    public Boss1StandBy(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override sealed INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(Boss1AIController.EBossState.STANDBY);
    }

    public INode.ENodeState StandByNode()
    {
        //temp
        Debug.Log("ReadyNode");

        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���
        isFirst(Define_Boss1AniParam.READY_BOOL, enemy.bossAnimationData.IdleParameterHash);

        //1�ʸ� ���. 1��(���� ���ǵ忡 ���� �޶���)�� ������ true�� ��ȯ�Ѵ�.
        if (controller.SetTimeAndCheck(1.0f / enemy.enemyStatHandler.stat.attackSpeed))
        {
            //Idle �ִϸ��̼� ����
            controller.animator.SetBool(enemy.bossAnimationData.IdleParameterHash, false);

            //���� ����
            controller.RandomAttackState();

            return INode.ENodeState.ENS_Success;
        }
        else return INode.ENodeState.ENS_Running;
    }
}
