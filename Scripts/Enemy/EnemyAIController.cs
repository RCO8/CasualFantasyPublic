using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIController : MonoBehaviour
{
    public enum EBossState
    {
        RUSH = 0,       //�����ϸ鼭 �����ϴ� ������ ��
        SMASH = 1,      //���� ũ�� �ֵθ��ų� ������ �������� ��
        ATTACK = 2,     //�Ϲ� ����
        SKILL = 3,      //��ų
        STANDBY = 4,    //���
        STUN = 5,       //����
        READY = 6,      //�غ�
        RAGE = 7,       //�ݳ�
        DEATH = 8       //����
    }

    [field: SerializeField] public Rigidbody2D rb { get; protected set; }
    [field: SerializeField] public EnemyEffect enemyEffect { get; protected set; }
    [field: SerializeField] public Animator animator { get; protected set; }

    //������ Ʈ��
    protected BehaviorTreeRunner _BTRunner = null;

    //�� ����
    public EBossState eBossState { get; private set; }

    //���� ���µ��� ����Ʈ ( ���� ������ ���߿� ���� ���°� �� �þ �� �����ϱ� ���ϱ� ���� )
    protected List<EBossState> _attackStateList = new List<EBossState>();

    //���� ��尡 �� ������ Ȯ���ϴ� ����
    protected int attackStatesOfNumber;

    //�ð� �ٱ��� ( �ַ� �ð��� �� �� ���� )
    public float timeBasket { get; private set; } = 0.0f;

    protected virtual void Awake()
    {
        Init();
        _BTRunner = new BehaviorTreeRunner(SettingBT());
    }

    protected virtual void Start()
    {
        //���� ���´� Ready
        ChangeState(EBossState.READY);
    }

    protected virtual void Update()
    {
        _BTRunner.Operate();
    }

    //ó�� �ʱ�ȭ�� �� ȣ���ϴ� �Լ�
    protected abstract void Init();

    //BehaviorTreeRunner �����ڷ� �Ѱ��� INode �ν��Ͻ� ( �߻� �Լ� )
    protected abstract INode SettingBT();

    //�ð� �� �� ����ϴ� �Լ�
    public bool SetTimeAndCheck(float _time)
    {
        //������ �ð��� �Ѿ����� true�� ��ȯ�ϰ�, �ȳѾ��ٸ� �ð��� Tick���� �����ְ� false�� ��ȯ�Ѵ�. 
        if (timeBasket > _time) return true;
        else
        {
            timeBasket += Time.deltaTime;
            return false;
        }
    }

    //TimeBasket�� 0���� �����.
    public void ResetTimeBasket() { timeBasket = 0.0f; }

    //���� ���¸� �������� ���ϴ� �Լ�
    public void RandomAttackState()
    {
        int _num = Random.Range(0, attackStatesOfNumber);
        ChangeState(_attackStateList[_num]);
    }

    //�� ���� �����ϴ� �Լ�
    public virtual void ChangeState(EBossState _enemyState)
    {
        ResetTimeBasket();
        eBossState = _enemyState;
    }
}
