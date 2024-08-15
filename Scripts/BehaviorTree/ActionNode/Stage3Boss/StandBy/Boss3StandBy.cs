
public class Boss3StandBy : Boss3ActionNodeFunction
{
    public Boss3StandBy(Boss3_Battle _enemy, Boss3AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(Boss1AIController.EBossState.STANDBY);
    }

    public INode.ENodeState StandByNode()
    {
        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���
        isFirst(Define_Boss3AniParam.READY_BOOL, enemy.bossAnimationData.IdleParameterHash);

        //1�ʸ� ���. 1.5��(���� ���ǵ忡 ���� �޶���)�� ������ true�� ��ȯ�Ѵ�.
        if (controller.SetTimeAndCheck(1.5f / enemy.enemyStatHandler.stat.attackSpeed))
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
