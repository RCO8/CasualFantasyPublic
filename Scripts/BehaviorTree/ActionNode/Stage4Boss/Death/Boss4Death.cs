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
        //상태 확인
        return CheckFunction(EnemyAIController.EBossState.DEATH);
    }

    public INode.ENodeState DeathNode()
    {
        //처음 이 노드를 시작하면 애니메이션 재생, 콜라이더 Off
        isFirst(Define_Boss4AniParam.DEATH_BOOL, enemy.bossAnimationData.DeathParameterHash, false);

        return INode.ENodeState.ENS_Running;
    }
}
