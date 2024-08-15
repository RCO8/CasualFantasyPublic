
public class Boss3DashAttack : Boss3ActionNodeFunction
{
    private bool _ready = false;
    private bool _jump = false;

    public Boss3DashAttack(Boss3_Battle _enemy, Boss3AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //상태 확인
        return CheckFunction(EnemyAIController.EBossState.RUSH);
    }

    public INode.ENodeState DashAttackReadyNode()
    {
        //준비됐는지 확인
        if (!_ready) 
        {
            //플레이어를 쳐다본다.
            enemy.LookPlayer(CharacterManager.instance.basePlayer.gameObject);

            //애니메이션 재생
            controller.animator.SetTrigger(Define_Boss3AniParam.DASH_TRIGGER);

            //준비됨
            _ready = true;
        }

        //애니메이션이 끝나면 다음 노드로 진행
        if (IsPlayingAnimation(Define_AnimationNodeName.SKILL_BOW) == 2)
        {
            return INode.ENodeState.ENS_Success;
        }
        
        return INode.ENodeState.ENS_Running;
    }

    public INode.ENodeState DashAttackStartNode()
    {
        //점프를 했는지 안했는지 확인
        if (!_jump)
        {
            //준비됨
            _jump = true;

            //플레이어를 쳐다본다
            enemy.LookPlayer(CharacterManager.instance.battlePlayer.gameObject);

            //플레이어에게 대시
            enemy.JumpToPlayer();
        }

        if(enemy.isJumpEnd)
        {
            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }

    public INode.ENodeState DashAttackEndNode()
    {
        //옵션 초기화
        _ready = false;
        _jump = false;

        //상태 변경
        controller.ChangeState(EnemyAIController.EBossState.STANDBY);

        return INode.ENodeState.ENS_Success;
    }
}
