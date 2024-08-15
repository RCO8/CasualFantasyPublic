
public class Boss3Ready : Boss3ActionNodeFunction
{
    public Boss3Ready(Boss3_Battle _enemy, Boss3AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(EnemyAIController.EBossState.READY);
    }

    public INode.ENodeState ReadyNode()
    {
        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���
        isFirst(Define_Boss3AniParam.READY_BOOL, enemy.bossAnimationData.IdleParameterHash);

        //2�ʸ� ���. 2�ʰ� ������ true�� ��ȯ�Ѵ�.
        if (controller.SetTimeAndCheck(2.0f))
        {
            //Idle �ִϸ��̼� ����
            controller.animator.SetBool(enemy.bossAnimationData.IdleParameterHash, false);

            //���� ����
            controller.RandomAttackState();

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }
}
