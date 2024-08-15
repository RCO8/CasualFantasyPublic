using System.Collections.Generic;

public class Boss3AIController : EnemyAIController
{
    private Boss3_Battle _boss;                 //Boss �ν��Ͻ�

    private Boss3Ready _bossReady;              //�غ�
    private Boss3Smash _bossSmash;              //���Ž�
    private Boss3DashAttack _bossDashAttack;    //��þ���
    private Boss3StandBy _bossStandBy;          //���
    private Boss3Death _bossDeath;              //����

    //ó�� �ʱ�ȭ�� �� ȣ���� �Լ�
    protected sealed override void Init()
    {
        //���� Ŭ���� ĳ��
        _boss = GetComponent<Boss3_Battle>();

        //ActionNode�Լ��� �ʱ�ȭ
        _bossReady = new Boss3Ready(_boss, this);
        _bossSmash = new Boss3Smash(_boss, this);
        _bossDashAttack = new Boss3DashAttack(_boss, this);
        _bossStandBy = new Boss3StandBy(_boss, this);
        _bossDeath = new Boss3Death(_boss, this);

        //���� ���� 2���̴�.
        attackStatesOfNumber = 2;

        //���� ���µ��� ����Ʈ�� �߰��Ѵ�
        _attackStateList.Add(EBossState.RUSH);
        _attackStateList.Add(EBossState.SMASH);
    }

    //BehaviorTreeRunner �����ڷ� �Ѱ��� INode �ν��Ͻ�
    protected sealed override INode SettingBT()
    {
        //��Ʈ ���( ������ )
        return new SelectNode
        (
            new List<INode>()
            {
                //��� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //üũ ��� ( �׼� )
                        new ActionNode(_bossDashAttack.CheckNode),
                        //��� ��� ( �׼� )
                        new ActionNode(_bossDashAttack.DashAttackReadyNode),
                        new ActionNode(_bossDashAttack.DashAttackStartNode),
                        new ActionNode(_bossDashAttack.DashAttackEndNode)
                    }
                ),
                //���Ž� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //üũ ��� ( �׼� )
                        new ActionNode(_bossSmash.CheckNode),
                        //���� ��� ( �׼� )
                        new ActionNode(_bossSmash.SmashNode)
                    }
                ),
                //��� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //üũ ��� ( �׼� )
                        new ActionNode(_bossStandBy.CheckNode),
                        //��� ��� ( �׼� )
                        new ActionNode(_bossStandBy.StandByNode)
                    }
                ),
                //�غ� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //üũ ��� ( �׼� )
                        new ActionNode(_bossReady.CheckNode),
                        //�غ� ��� ( �׼� )
                        new ActionNode(_bossReady.ReadyNode)
                    }
                ),
                //���� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //üũ ��� ( �׼� )
                        new ActionNode(_bossDeath.CheckNode),
                        //���� ��� ( �׼� )
                        new ActionNode(_bossDeath.DeathNode)
                    }
                )
            }
        );
    }
}
