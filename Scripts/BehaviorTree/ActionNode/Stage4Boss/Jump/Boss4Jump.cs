
public class Boss4Jump : Boss4ActionNodeFunction
{
    private bool _ready = false;
    private bool _jump = false;

    public Boss4Jump(Boss4_Battle _enemy, Boss4AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(controller.jumpState);
    }

    public INode.ENodeState DashAttackNode()
    {
        //�غ�ƴ��� Ȯ��
        if (!_ready)
        {
            //�÷��̾ �Ĵٺ���.
            enemy.LookPlayer(CharacterManager.instance.basePlayer.gameObject);

            //�ִϸ��̼� ���
            controller.animator.SetTrigger(Define_Boss4AniParam.JUMPATTACK_TRIGGER);

            //�غ��
            _ready = true;
        }

        if (IsPlayingAnimation(controller.jumpAttackNode, 0.75f) == 2 && !_jump)
        {
            //������
            _jump = true;

            //�÷��̾�� ���
            enemy.JumpToPlayer();
        }

        //������ ������
        if (enemy.isJumpEnd)
        {
            //�ɼ� �ʱ�ȭ
            _ready = false;
            _jump = false;

            //���� ����
            controller.ChangeState(EnemyAIController.EBossState.STANDBY);

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }
}
