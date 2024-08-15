using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Boss4AIController : EnemyAIController
{
    private Boss4_Battle _boss;                 //Boss 인스턴스

    private Boss4Ready _bossReady;              //준비
    private Boss4Jump _bossJump;                //스매시
    private Boss4Dig _bossDig;                  //땅 파기
    private Boss4Bounce _bossBounce;            //바운스
    private Boss4StandBy _bossStandBy;          //대기
    private Boss4Death _bossDeath;              //죽음
    private Boss4Stun _bossStun;                //기절

    //상태 설정
    public EBossState jumpState { get; private set; } = EBossState.RUSH;      //스매시는 RUSH상태로 설정한다
    public EBossState bounceState { get; private set; } = EBossState.SKILL;   //바운스는 SMASH상태로 설정한다
    public EBossState digState { get; private set; } = EBossState.SMASH;      //땅 파기는 SKILL상태로 설정한다

    //애니메이션 노드 설정
    public string jumpAttackNode { get; private set; } = Define_AnimationNodeName.SKILL_BOW;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //바운스 상태일 때
        if (eBossState == bounceState)
        {
            if (collision.gameObject.CompareTag(Define.TAG_PLAYERATTACK))
            {
                _bossBounce.Damaged();

                //gameObject.transform.DOPunchPosition(gameObject.transform.position, 0.5f);
            }
        }
    }

    //처음 초기화할 때 호출디는 함수
    protected sealed override void Init()
    {
        //보스 클래스 캐싱
        _boss = GetComponent<Boss4_Battle>();

        //ActionNode함수들 초기화
        _bossReady = new Boss4Ready(_boss, this);
        _bossJump = new Boss4Jump(_boss, this);
        _bossDig = new Boss4Dig(_boss, this);
        _bossBounce = new Boss4Bounce(_boss, this);
        _bossStandBy = new Boss4StandBy(_boss, this);
        _bossDeath = new Boss4Death(_boss, this);
        _bossStun = new Boss4Stun(_boss, this);

        //공격 노드는 2개이다.
        //attackStatesOfNumber = 3;

        //공격 상태들을 리스트에 추가한다
        //_attackStateList.Add(jumpState);
        //_attackStateList.Add(bounceState);
        //_attackStateList.Add(digState);

        //공격 노드는 2개이다.
        attackStatesOfNumber = 1;

        //공격 상태들을 리스트에 추가한다
        //_attackStateList.Add(jumpState);
        _attackStateList.Add(bounceState);
        //_attackStateList.Add(digState);
    }

    //BehaviorTreeRunner 생성자로 넘겨줄 INode 인스턴스
    protected sealed override INode SettingBT()
    {
        //루트 노드( 셀렉터 )
        return new SelectNode
        (
            new List<INode>()
            {
                //대기 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        new ActionNode(_bossStandBy.CheckNode),
                        //준비 노드 ( 액션 )
                        new ActionNode(_bossStandBy.StandByNode)
                    }
                ),
                //스매시 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        new ActionNode(_bossJump.CheckNode),
                        //대시 노드 ( 액션 )
                        new ActionNode(_bossJump.DashAttackNode)
                    }
                ),
                //바운스 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        new ActionNode(_bossBounce.CheckNode),
                        //공중부양 노드 ( 액션 )
                        new ActionNode(_bossBounce.LevitationNode),
                        //바운스 노드 ( 액션 )
                        new ActionNode(_bossBounce.BounceNode)
                    }
                ),
                //땅 파기 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        //new ActionNode(_bossDig.CheckNode),
                        //대기 노드 ( 액션 )
                        //new ActionNode(_bossStandBy.StandByNode)
                    }
                ),
                //기절 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        new ActionNode(_bossStun.CheckNode),
                        //기절 노드 ( 액션 )
                        new ActionNode(_bossStun.StunNode)
                    }
                ),
                //준비 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        new ActionNode(_bossReady.CheckNode),
                        //준비 노드 ( 액션 )
                        new ActionNode(_bossReady.ReadyNode)
                    }
                ),
                //죽음 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        new ActionNode(_bossDeath.CheckNode),
                        //죽음 노드 ( 액션 )
                        new ActionNode(_bossDeath.DeathNode)
                    }
                )
            }
        );
    }
}
