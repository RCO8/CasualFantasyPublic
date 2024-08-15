using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Boss1AIController : EnemyAIController
{
    private Boss1_Battle _boss;                                 //Boss 인스턴스

    //ActionNode함수들
    private Boss1Ready _bossReady;      //준비
    private Boss1Rush _bossRush;        //돌진
    private Boss1Smash _bossSmash;      //스매시
    private Boss1Rage _bossRage;        //격노
    private Boss1Stun _bossStun;        //기절
    private Boss1Death _bossDeath;      //죽음
    private Boss1StandBy _bossStandBy;  //대기

    //처음 초기화할 때 호출디는 함수
    protected sealed override void Init()
    {
        //보스 클래스 캐싱
        _boss = GetComponent<Boss1_Battle>();

        //ActionNode함수들 초기화
        _bossReady = new Boss1Ready(_boss, this);
        _bossRush = new Boss1Rush(_boss, this);
        _bossSmash = new Boss1Smash(_boss, this);
        _bossRage = new Boss1Rage(_boss, this);
        _bossStun = new Boss1Stun(_boss, this);
        _bossDeath = new Boss1Death(_boss, this);
        _bossStandBy = new Boss1StandBy(_boss, this);

        //공격 노드는 2개이다.
        attackStatesOfNumber = 2;

        //공격 상태들을 리스트에 추가한다.
        _attackStateList.Add(EBossState.RUSH);
        _attackStateList.Add(EBossState.SMASH);
    }

    //BehaviorTreeRunner 생성자로 넘겨줄 INode 인스턴스
    protected sealed override INode SettingBT()
    {
        //루트 노드( 셀렉터 )
        return new SelectNode
        (
            new List<INode>()
            {

                //돌진 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        new ActionNode(_bossRush.CheckNode),
                        //돌진 노드 ( 액션 )
                        new ActionNode(_bossRush.RushNode)
                    }
                ),

                //스매시 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        new ActionNode(_bossSmash.CheckNode),
                        //점프 노드 ( 액션 )
                        new ActionNode(_bossSmash.JumpNode),
                        //스매시 노드 ( 액션 )
                        new ActionNode(_bossSmash.SmashNode)
                    }
                ),

                //대기 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        new ActionNode(_bossStandBy.CheckNode),
                        //대기 노드 ( 액션 )
                        new ActionNode(_bossStandBy.StandByNode)
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
                        //점프 노드 ( 액션 )
                        new ActionNode(_bossReady.CheckNode),
                        //스매시 노드 ( 액션 )
                        new ActionNode(_bossReady.ReadyNode)
                    }
                ),

                //격노 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //점프 노드 ( 액션 )
                        new ActionNode(_bossRage.CheckNode),
                        //스매시 노드 ( 액션 )
                        new ActionNode(_bossRage.RageNode)
                    }
                ),

                //죽음 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //점프 노드 ( 액션 )
                        new ActionNode(_bossDeath.CheckNode),
                        //스매시 노드 ( 액션 )
                        new ActionNode(_bossDeath.DeathNode)
                    }
                )
            }
        );
    }

    //적 상태 변경하는 함수 ( 재정의 )
    public override void ChangeState(EBossState _enemyState)
    {
        base.ChangeState( _enemyState );

        //격노, 죽음 상태일 때, 기절 이펙트와 기절 애니메이션을 모두 비활성화한다.
        if(eBossState == EBossState.RAGE || eBossState == EBossState.DEATH)
        {
            //버그때문에 한 번 더 띄우고 이펙트를 감춘다.
            enemyEffect.ChangeEffectState(EffectState.Stun);
            //이펙트 감추기
            enemyEffect.ChangeEffectState(EffectState.None);

            //Stun 애니메이션 중지
            animator.SetBool(_boss.bossAnimationData.StunParameterHash, false);
        }
    }
}
