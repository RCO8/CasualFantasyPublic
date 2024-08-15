using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Death : Boss4ActionNodeFunction
{
    public Boss4Death(Boss4_Battle _enemy, Boss4AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(EnemyAIController.EBossState.DEATH);
    }

    public INode.ENodeState DeathNode()
    {
        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���, �ݶ��̴� Off
        isFirst(Define_Boss4AniParam.DEATH_BOOL, enemy.bossAnimationData.DeathParameterHash, false);

        return INode.ENodeState.ENS_Running;
    }
}
