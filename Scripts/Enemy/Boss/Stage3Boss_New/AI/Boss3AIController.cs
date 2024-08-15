using System.Collections.Generic;

public class Boss3AIController : EnemyAIController
{
    private Boss3_Battle _boss;                 //Boss 인스턴스

    private Boss3Ready _bossReady;              //준비
    private Boss3Smash _bossSmash;              //스매시
    private Boss3DashAttack _bossDashAttack;    //대시어택
    private Boss3StandBy _bossStandBy;          //대기
    private Boss3Death _bossDeath;              //죽음

    //처음 초기화할 때 호출디는 함수
    protected sealed override void Init()
    {
        //보스 클래스 캐싱
        _boss = GetComponent<Boss3_Battle>();

        //ActionNode함수들 초기화
        _bossReady = new Boss3Ready(_boss, this);
        _bossSmash = new Boss3Smash(_boss, this);
        _bossDashAttack = new Boss3DashAttack(_boss, this);
        _bossStandBy = new Boss3StandBy(_boss, this);
        _bossDeath = new Boss3Death(_boss, this);

        //공격 노드는 2개이다.
        attackStatesOfNumber = 2;

        //공격 상태들을 리스트에 추가한다
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
                //대시 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        new ActionNode(_bossDashAttack.CheckNode),
                        //대시 노드 ( 액션 )
                        new ActionNode(_bossDashAttack.DashAttackReadyNode),
                        new ActionNode(_bossDashAttack.DashAttackStartNode),
                        new ActionNode(_bossDashAttack.DashAttackEndNode)
                    }
                ),
                //스매시 노드( 시퀀스 )
                new SequenceNode
                (
                    new List<INode>()
                    {
                        //체크 노드 ( 액션 )
                        new ActionNode(_bossSmash.CheckNode),
                        //돌진 노드 ( 액션 )
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
