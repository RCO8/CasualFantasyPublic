using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Death : Boss1ActionNodeFunction
{
    public Boss1Death(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override INode.ENodeState CheckNode()
    {
        //상태 확인
        return CheckFunction(Boss1AIController.EBossState.DEATH);
    }

    public INode.ENodeState DeathNode()
    {
        //처음 이 노드를 시작하면 애니메이션 재생, 콜라이더 Off
        isFirst(Define_Boss1AniParam.DEATH_BOOL, enemy.bossAnimationData.DeathParameterHash, false);

        return INode.ENodeState.ENS_Running;
    }
}
