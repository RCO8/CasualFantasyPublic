using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Death : Boss1ActionNodeFunction
{
    public Boss1Death(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(Boss1AIController.EBossState.DEATH);
    }

    public INode.ENodeState DeathNode()
    {
        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���, �ݶ��̴� Off
        isFirst(Define_Boss1AniParam.DEATH_BOOL, enemy.bossAnimationData.DeathParameterHash, false);

        return INode.ENodeState.ENS_Running;
    }
}
