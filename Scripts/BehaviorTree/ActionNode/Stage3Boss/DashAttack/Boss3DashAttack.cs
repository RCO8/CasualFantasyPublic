
public class Boss3DashAttack : Boss3ActionNodeFunction
{
    private bool _ready = false;
    private bool _jump = false;

    public Boss3DashAttack(Boss3_Battle _enemy, Boss3AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(EnemyAIController.EBossState.RUSH);
    }

    public INode.ENodeState DashAttackReadyNode()
    {
        //�غ�ƴ��� Ȯ��
        if (!_ready) 
        {
            //�÷��̾ �Ĵٺ���.
            enemy.LookPlayer(CharacterManager.instance.basePlayer.gameObject);

            //�ִϸ��̼� ���
            controller.animator.SetTrigger(Define_Boss3AniParam.DASH_TRIGGER);

            //�غ��
            _ready = true;
        }

        //�ִϸ��̼��� ������ ���� ���� ����
        if (IsPlayingAnimation(Define_AnimationNodeName.SKILL_BOW) == 2)
        {
            return INode.ENodeState.ENS_Success;
        }
        
        return INode.ENodeState.ENS_Running;
    }

    public INode.ENodeState DashAttackStartNode()
    {
        //������ �ߴ��� ���ߴ��� Ȯ��
        if (!_jump)
        {
            //�غ��
            _jump = true;

            //�÷��̾ �Ĵٺ���
            enemy.LookPlayer(CharacterManager.instance.battlePlayer.gameObject);

            //�÷��̾�� ���
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
        //�ɼ� �ʱ�ȭ
        _ready = false;
        _jump = false;

        //���� ����
        controller.ChangeState(EnemyAIController.EBossState.STANDBY);

        return INode.ENodeState.ENS_Success;
    }
}
