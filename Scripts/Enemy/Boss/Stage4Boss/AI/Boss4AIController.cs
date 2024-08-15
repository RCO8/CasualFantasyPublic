using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Boss4AIController : EnemyAIController
{
    private Boss4_Battle _boss;                 //Boss �ν��Ͻ�

    private Boss4Ready _bossReady;              //�غ�
    private Boss4Jump _bossJump;                //���Ž�
    private Boss4Dig _bossDig;                  //�� �ı�
    private Boss4Bounce _bossBounce;            //�ٿ
    private Boss4StandBy _bossStandBy;          //���
    private Boss4Death _bossDeath;              //����
    private Boss4Stun _bossStun;                //����

    //���� ����
    public EBossState jumpState { get; private set; } = EBossState.RUSH;      //���Žô� RUSH���·� �����Ѵ�
    public EBossState bounceState { get; private set; } = EBossState.SKILL;   //�ٿ�� SMASH���·� �����Ѵ�
    public EBossState digState { get; private set; } = EBossState.SMASH;      //�� �ı�� SKILL���·� �����Ѵ�

    //�ִϸ��̼� ��� ����
    public string jumpAttackNode { get; private set; } = Define_AnimationNodeName.SKILL_BOW;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ٿ ������ ��
        if (eBossState == bounceState)
        {
            if (collision.gameObject.CompareTag(Define.TAG_PLAYERATTACK))
            {
                _bossBounce.Damaged();

                //gameObject.transform.DOPunchPosition(gameObject.transform.position, 0.5f);
            }
        }
    }

    //ó�� �ʱ�ȭ�� �� ȣ���� �Լ�
    protected sealed override void Init()
    {
        //���� Ŭ���� ĳ��
        _boss = GetComponent<Boss4_Battle>();

        //ActionNode�Լ��� �ʱ�ȭ
        _bossReady = new Boss4Ready(_boss, this);
        _bossJump = new Boss4Jump(_boss, this);
        _bossDig = new Boss4Dig(_boss, this);
        _bossBounce = new Boss4Bounce(_boss, this);
        _bossStandBy = new Boss4StandBy(_boss, this);
        _bossDeath = new Boss4Death(_boss, this);
        _bossStun = new Boss4Stun(_boss, this);

        //���� ���� 2���̴�.
        //attackStatesOfNumber = 3;

        //���� ���µ��� ����Ʈ�� �߰��Ѵ�
        //_attackStateList.Add(jumpState);
        //_attackStateList.Add(bounceState);
        //_attackStateList.Add(digState);

        //���� ���� 2���̴�.
        attackStatesOfNumber = 1;

        //���� ���µ��� ����Ʈ�� �߰��Ѵ�
        //_attackStateList.Add(jumpState);
        _attackStateList.Add(bounceState);
        //_attackStateList.Add(digState);
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
                        new ActionNode(_bossStandBy.CheckNode),
                        //�غ� ��� ( �׼� )
                        new ActionNode(_bossStandBy.StandByNode)
                    }
                ),
                //���Ž� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //üũ ��� ( �׼� )
                        new ActionNode(_bossJump.CheckNode),
                        //��� ��� ( �׼� )
                        new ActionNode(_bossJump.DashAttackNode)
                    }
                ),
                //�ٿ ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //üũ ��� ( �׼� )
                        new ActionNode(_bossBounce.CheckNode),
                        //���ߺξ� ��� ( �׼� )
                        new ActionNode(_bossBounce.LevitationNode),
                        //�ٿ ��� ( �׼� )
                        new ActionNode(_bossBounce.BounceNode)
                    }
                ),
                //�� �ı� ���( ������ )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //üũ ��� ( �׼� )
                        //new ActionNode(_bossDig.CheckNode),
                        //��� ��� ( �׼� )
                        //new ActionNode(_bossStandBy.StandByNode)
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
