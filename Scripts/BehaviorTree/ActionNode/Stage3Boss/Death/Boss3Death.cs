
public class Boss3Death : Boss3ActionNodeFunction
{
    public Boss3Death(Boss3_Battle _enemy, Boss3AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(EnemyAIController.EBossState.DEATH);
    }

    public INode.ENodeState DeathNode()
    {
        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���, �ݶ��̴� Off
        isFirst(Define_Boss3AniParam.DEATH_BOOL, enemy.bossAnimationData.DeathParameterHash, false);

        return INode.ENodeState.ENS_Running;
    }
}
