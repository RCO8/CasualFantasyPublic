
public class Boss3Smash : Boss3ActionNodeFunction
{
    //��ų�� ���� �ð�
    private float _shootSkillTime = 0.3f;
    //ó������
    private bool _isFirst = true;

    public Boss3Smash(Boss3_Battle _enemy, Boss3AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(EnemyAIController.EBossState.SMASH);
    }

    public INode.ENodeState SmashNode()
    {
        //�ִϸ��̼� ��� ������ Ȯ�� ( ����� �����ٸ� ENS_Success�� ��ȯ )
        if (IsPlayingAnimation(Define_AnimationNodeName.SKILL_MAGIC) == 2)
        {
            //�÷��̾ �Ĵٺ���
            enemy.LookPlayer(CharacterManager.instance.battlePlayer.gameObject);

            //���� �߻�
            enemy.SpawnSmashObject();

            //�ɼ� �ʱ�ȭ
            _isFirst = true;

            //StandBy ���� ����
            controller.ChangeState(EnemyAIController.EBossState.STANDBY);

            return INode.ENodeState.ENS_Success;
        }

        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���
        if(_isFirst)
        {
            _isFirst = false;

            //�÷��̾ �Ĵٺ���
            enemy.LookPlayer(CharacterManager.instance.battlePlayer.gameObject);

            //Smash �ִϸ��̼� ���
            controller.animator.SetTrigger(enemy.bossAnimationData.SkillParameterHash);
        }

        return INode.ENodeState.ENS_Running;
    }
}
