
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
        //���� Ȯ��
        if (controller_Root.eBossState != _state) return INode.ENodeState.ENS_Failure;

        return INode.ENodeState.ENS_Success;
    }

    //��忡 ó�� �� ��, ���Ǵ� �Լ�
    protected bool isFirst(in string _param, int _hash)
    {
        //ó�� �� ��带 ������ ��
        if (!controller_Root.animator.GetBool(_param))
        {
            //�ִϸ��̼� ���
            controller_Root.animator.SetBool(_hash, true);

            return true;
        }

        return false;
    }

    //������ ������ �ݶ��̴� on, off�� �޸� ����
    protected bool isFirst(in string _param, int _hash, bool _onCollider)
    {
        //ó�� �� ��带 ������ ��
        if (!controller_Root.animator.GetBool(_param))
        {
            //�ִϸ��̼� ���
            controller_Root.animator.SetBool(_hash, true);

            //�ݶ��̴� On
            enemy_Root.damageCollider.gameObject.SetActive(_onCollider);

            return true;
        }

        return false;
    }

    //�ִϸ��̼��� ����ǰ� �ִ��� Ȯ���ϴ� �Լ�( 0: ��� ���۵� ����, 1: �����, 2: ��� ����, -1: ���� )
    public int IsPlayingAnimation(in string _anim)
    {
        if (controller_Root.animator.GetCurrentAnimatorStateInfo(0).IsName(_anim) == true)
        {
            // ���ϴ� �ִϸ��̼��̶�� �÷��� ������ üũ
            float animTime = controller_Root.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            //Debug.Log(animTime);
            if (animTime > 0.95f)
            {
                //����� ������ 2�� ��ȯ
                return 2;
            }
            else if(animTime <= 0.0f)
            {
                //��� ���۵� �������� 2�� ��ȯ
                return 0;
            }
            else
            {
                //��� ���̸� true�� ��ȯ
                return 1;
            }
        }
        //������ -1 ��ȯ
        return - 1;
    }

    //�ִϸ��̼��� ����ǰ� �ִ��� Ȯ���ϴ� �Լ�( 0: ��� ���۵� ����, 1: �����, 2: ��� ����, -1: ���� )( �������� ���� )
    public int IsPlayingAnimation(in string _anim, float _time)
    {
        if (controller_Root.animator.GetCurrentAnimatorStateInfo(0).IsName(_anim) == true)
        {
            // ���ϴ� �ִϸ��̼��̶�� �÷��� ������ üũ
            float animTime = controller_Root.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (animTime >= _time)
            {
                //����� ������ 2�� ��ȯ
                return 2;
            }
            else if (animTime == 0.0f)
            {
                //��� ���۵� �������� 0�� ��ȯ
                return 0;
            }
            else
            {
                //��� ���̸� 1�� ��ȯ
                return 1;
            }
        }

        //������ -1 ��ȯ
        return -1;
    }

}
