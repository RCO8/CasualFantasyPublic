using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Boss1AIController : EnemyAIController
{
    private Boss1_Battle _boss;                                 //Boss �ν��Ͻ�

    //ActionNode�Լ���
    private Boss1Ready _bossReady;      //�غ�
    private Boss1Rush _bossRush;        //����
    private Boss1Smash _bossSmash;      //���Ž�
    private Boss1Rage _bossRage;        //�ݳ�
    private Boss1Stun _bossStun;        //����
    private Boss1Death _bossDeath;      //����
    private Boss1StandBy _bossStandBy;  //���

    //ó�� �ʱ�ȭ�� �� ȣ���� �Լ�
    protected sealed override void Init()
    {
        //���� Ŭ���� ĳ��
        _boss = GetComponent<Boss1_Battle>();

        //ActionNode�Լ��� �ʱ�ȭ
        _bossReady = new Boss1Ready(_boss, this);
        _bossRush = new Boss1Rush(_boss, this);
        _bossSmash = new Boss1Smash(_boss, this);
        _bossRage = new Boss1Rage(_boss, this);
        _bossStun = new Boss1Stun(_boss, this);
        _bossDeath = new Boss1Death(_boss, this);
        _bossStandBy = new Boss1StandBy(_boss, this);

        //���� ���� 2���̴�.
        attackStatesOfNumber = 2;

        //���� ���µ��� ����Ʈ�� �߰��Ѵ�.
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

                //���� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //üũ ��� ( �׼� )
                        new ActionNode(_bossRush.CheckNode),
                        //���� ��� ( �׼� )
                        new ActionNode(_bossRush.RushNode)
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
                        new ActionNode(_bossSmash.JumpNode),
                        //���Ž� ��� ( �׼� )
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

                //���� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //üũ ��� ( �׼� )
                        new ActionNode(_bossStun.CheckNode),
                        //���� ��� ( �׼� )
                        new ActionNode(_bossStun.StunNode)
                    }
                ),

                //�غ� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //���� ��� ( �׼� )
                        new ActionNode(_bossReady.CheckNode),
                        //���Ž� ��� ( �׼� )
                        new ActionNode(_bossReady.ReadyNode)
                    }
                ),

                //�ݳ� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //���� ��� ( �׼� )
                        new ActionNode(_bossRage.CheckNode),
                        //���Ž� ��� ( �׼� )
                        new ActionNode(_bossRage.RageNode)
                    }
                ),

                //���� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //���� ��� ( �׼� )
                        new ActionNode(_bossDeath.CheckNode),
                        //���Ž� ��� ( �׼� )
                        new ActionNode(_bossDeath.DeathNode)
                    }
                )
            }
        );
    }

    //�� ���� �����ϴ� �Լ� ( ������ )
    public override void ChangeState(EBossState _enemyState)
    {
        base.ChangeState( _enemyState );

        //�ݳ�, ���� ������ ��, ���� ����Ʈ�� ���� �ִϸ��̼��� ��� ��Ȱ��ȭ�Ѵ�.
        if(eBossState == EBossState.RAGE || eBossState == EBossState.DEATH)
        {
            //���׶����� �� �� �� ���� ����Ʈ�� �����.
            enemyEffect.ChangeEffectState(EffectState.Stun);
            //����Ʈ ���߱�
            enemyEffect.ChangeEffectState(EffectState.None);

            //Stun �ִϸ��̼� ����
            animator.SetBool(_boss.bossAnimationData.StunParameterHash, false);
        }
    }
}
