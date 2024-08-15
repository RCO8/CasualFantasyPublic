
public class Boss3StandBy : Boss3ActionNodeFunction
{
    public Boss3StandBy(Boss3_Battle _enemy, Boss3AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //상태 확인
        return CheckFunction(Boss1AIController.EBossState.STANDBY);
    }

    public INode.ENodeState StandByNode()
    {
        //처음 이 노드를 시작하면 애니메이션 재생
        isFirst(Define_Boss3AniParam.READY_BOOL, enemy.bossAnimationData.IdleParameterHash);

        //1초를 잰다. 1.5초(공격 스피드에 따라 달라짐)가 넘으면 true를 반환한다.
        if (controller.SetTimeAndCheck(1.5f / enemy.enemyStatHandler.stat.attackSpeed))
        {
            //Idle 애니메이션 중지
            controller.animator.SetBool(enemy.bossAnimationData.IdleParameterHash, false);

            //상태 변경
            controller.RandomAttackState();

            return INode.ENodeState.ENS_Success;
        }
        else return INode.ENodeState.ENS_Running;
    }
}
