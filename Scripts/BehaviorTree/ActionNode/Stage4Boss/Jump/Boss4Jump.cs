
public class Boss4Jump : Boss4ActionNodeFunction
{
    private bool _ready = false;
    private bool _jump = false;

    public Boss4Jump(Boss4_Battle _enemy, Boss4AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //상태 확인
        return CheckFunction(controller.jumpState);
    }

    public INode.ENodeState DashAttackNode()
    {
        //준비됐는지 확인
        if (!_ready)
        {
            //플레이어를 쳐다본다.
            enemy.LookPlayer(CharacterManager.instance.basePlayer.gameObject);

            //애니메이션 재생
            controller.animator.SetTrigger(Define_Boss4AniParam.JUMPATTACK_TRIGGER);

            //준비됨
            _ready = true;
        }

        if (IsPlayingAnimation(controller.jumpAttackNode, 0.75f) == 2 && !_jump)
        {
            //점프함
            _jump = true;

            //플레이어에게 대시
            enemy.JumpToPlayer();
        }

        //점프가 끝나면
        if (enemy.isJumpEnd)
        {
            //옵션 초기화
            _ready = false;
            _jump = false;

            //상태 변경
            controller.ChangeState(EnemyAIController.EBossState.STANDBY);

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }
}
