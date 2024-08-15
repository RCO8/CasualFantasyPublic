
public class Boss1Ready : Boss1ActionNodeFunction
{
    public Boss1Ready(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override sealed INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(Boss1AIController.EBossState.READY);
    }

    public INode.ENodeState ReadyNode()
    {
        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���
        isFirst(Define_Boss1AniParam.READY_BOOL, enemy.bossAnimationData.IdleParameterHash);

        //2�ʸ� ���. 2�ʰ� ������ true�� ��ȯ�Ѵ�.
        if (controller.SetTimeAndCheck(2.0f))
        {
            //Idle �ִϸ��̼� ����
            controller.animator.SetBool(enemy.bossAnimationData.IdleParameterHash, false);

            //���� ����
            controller.RandomAttackState();

            return INode.ENodeState.ENS_Success;
        }
        else return INode.ENodeState.ENS_Running;
    }
}
