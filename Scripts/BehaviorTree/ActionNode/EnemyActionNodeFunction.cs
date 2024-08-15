
public abstract class EnemyActionNodeFunction
{
    protected Enemy enemy_Root;
    protected EnemyAIController controller_Root;

    public EnemyActionNodeFunction(Enemy _enemy, EnemyAIController _con)
    {
        enemy_Root = _enemy;
        controller_Root = _con;
    }

    public abstract INode.ENodeState CheckNode();

    public INode.ENodeState CheckFunction(EnemyAIController.EBossState _state)
    {
        //상태 확인
        if (controller_Root.eBossState != _state) return INode.ENodeState.ENS_Failure;

        return INode.ENodeState.ENS_Success;
    }

    //노드에 처음 들어설 때, 사용되는 함수
    protected bool isFirst(in string _param, int _hash)
    {
        //처음 이 노드를 시작할 때
        if (!controller_Root.animator.GetBool(_param))
        {
            //애니메이션 재생
            controller_Root.animator.SetBool(_hash, true);

            return true;
        }

        return false;
    }

    //위에랑 같지만 콜라이더 on, off가 달린 버전
    protected bool isFirst(in string _param, int _hash, bool _onCollider)
    {
        //처음 이 노드를 시작할 때
        if (!controller_Root.animator.GetBool(_param))
        {
            //애니메이션 재생
            controller_Root.animator.SetBool(_hash, true);

            //콜라이더 On
            enemy_Root.damageCollider.gameObject.SetActive(_onCollider);

            return true;
        }

        return false;
    }

    //애니메이션이 재생되고 있는지 확인하는 함수( 0: 재생 시작도 안함, 1: 재생중, 2: 재생 끝남, -1: 오류 )
    public int IsPlayingAnimation(in string _anim)
    {
        if (controller_Root.animator.GetCurrentAnimatorStateInfo(0).IsName(_anim) == true)
        {
            // 원하는 애니메이션이라면 플레이 중인지 체크
            float animTime = controller_Root.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            //Debug.Log(animTime);
            if (animTime > 0.95f)
            {
                //재생이 끝나면 2를 반환
                return 2;
            }
            else if(animTime <= 0.0f)
            {
                //재생 시작도 안했으면 2를 반환
                return 0;
            }
            else
            {
                //재생 중이면 true를 반환
                return 1;
            }
        }
        //오류면 -1 반환
        return - 1;
    }

    //애니메이션이 재생되고 있는지 확인하는 함수( 0: 재생 시작도 안함, 1: 재생중, 2: 재생 끝남, -1: 오류 )( 세부지정 버전 )
    public int IsPlayingAnimation(in string _anim, float _time)
    {
        if (controller_Root.animator.GetCurrentAnimatorStateInfo(0).IsName(_anim) == true)
        {
            // 원하는 애니메이션이라면 플레이 중인지 체크
            float animTime = controller_Root.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (animTime >= _time)
            {
                //재생이 끝나면 2를 반환
                return 2;
            }
            else if (animTime == 0.0f)
            {
                //재생 시작도 안했으면 0을 반환
                return 0;
            }
            else
            {
                //재생 중이면 1을 반환
                return 1;
            }
        }

        //오류면 -1 반환
        return -1;
    }

}
