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
        //상태 확인
        return CheckFunction(EnemyAIController.EBossState.READY);
    }

    public INode.ENodeState ReadyNode()
    {
        //처음 이 노드를 시작하면 애니메이션 재생
        isFirst(Define_Boss4AniParam.READY_BOOL, enemy.bossAnimationData.IdleParameterHash);

        //2초를 잰다. 2초가 넘으면 true를 반환한다.
        if (controller.SetTimeAndCheck(2.0f))
        {
            //Idle 애니메이션 중지
            controller.animator.SetBool(enemy.bossAnimationData.IdleParameterHash, false);

            //상태 변경
            controller.RandomAttackState();

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }
}
