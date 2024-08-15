using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIController : MonoBehaviour
{
    public enum EBossState
    {
        RUSH = 0,       //돌진하면서 공격하는 상태일 때
        SMASH = 1,      //무언가 크게 휘두르거나 힘차게 내려찍을 때
        ATTACK = 2,     //일반 공격
        SKILL = 3,      //스킬
        STANDBY = 4,    //대기
        STUN = 5,       //기절
        READY = 6,      //준비
        RAGE = 7,       //격노
        DEATH = 8       //죽음
    }

    [field: SerializeField] public Rigidbody2D rb { get; protected set; }
    [field: SerializeField] public EnemyEffect enemyEffect { get; protected set; }
    [field: SerializeField] public Animator animator { get; protected set; }

    //비헤비어 트리
    protected BehaviorTreeRunner _BTRunner = null;

    //적 상태
    public EBossState eBossState { get; private set; }

    //공격 상태들의 리스트 ( 만든 이유는 나중에 공격 상태가 더 늘어날 때 수정하기 편하기 때문 )
    protected List<EBossState> _attackStateList = new List<EBossState>();

    //공격 노드가 몇 개인지 확인하는 변수
    protected int attackStatesOfNumber;

    //시간 바구니 ( 주로 시간을 잴 때 사용됨 )
    public float timeBasket { get; private set; } = 0.0f;

    protected virtual void Awake()
    {
        Init();
        _BTRunner = new BehaviorTreeRunner(SettingBT());
    }

    protected virtual void Start()
    {
        //시작 상태는 Ready
        ChangeState(EBossState.READY);
    }

    protected virtual void Update()
    {
        _BTRunner.Operate();
    }

    //처음 초기화할 때 호출하는 함수
    protected abstract void Init();

    //BehaviorTreeRunner 생성자로 넘겨줄 INode 인스턴스 ( 추상 함수 )
    protected abstract INode SettingBT();

    //시간 잴 때 사용하는 함수
    public bool SetTimeAndCheck(float _time)
    {
        //지정한 시간을 넘었으면 true를 반환하고, 안넘었다면 시간을 Tick마다 더해주고 false를 반환한다. 
        if (timeBasket > _time) return true;
        else
        {
            timeBasket += Time.deltaTime;
            return false;
        }
    }

    //TimeBasket을 0으로 만든다.
    public void ResetTimeBasket() { timeBasket = 0.0f; }

    //공격 상태를 랜덤으로 정하는 함수
    public void RandomAttackState()
    {
        int _num = Random.Range(0, attackStatesOfNumber);
        ChangeState(_attackStateList[_num]);
    }

    //적 상태 변경하는 함수
    public virtual void ChangeState(EBossState _enemyState)
    {
        ResetTimeBasket();
        eBossState = _enemyState;
    }
}
