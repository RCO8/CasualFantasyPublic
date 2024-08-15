using UnityEngine;

public class Boss1Rush : Boss1ActionNodeFunction
{
    //������ ����� ���ǵ�
    private float _speed = 12.5f;
    private float _direct;

    public Boss1Rush(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override sealed INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(Boss1AIController.EBossState.RUSH);
    }

    public INode.ENodeState RushNode()
    {
        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���, �ݶ��̴� On
        isFirst(Define_Boss1AniParam.RUSH_BOOL, enemy.bossAnimationData.RunParameterHash, true);

        //�÷��̾��� ��ġ�� ���� �ٶ󺸴� ����� �����ϴ� ������ ������
        IsThePlayerToRight();
        Rush();

        return INode.ENodeState.ENS_Running;
    }

    private bool IsThePlayerToRight()
    {
        //nullüũ
        if (CharacterManager.instance.basePlayer != null)
        {
            if (enemy.gameObject.transform.position.x < CharacterManager.instance.basePlayer.gameObject.transform.position.x)
            {
                SetDirect_0Stop_1Right_2Left(1);

                //�÷��̾ ������ �����ʿ� ������ true�� ��ȯ
                return true;
            }
            else
            {
                SetDirect_0Stop_1Right_2Left(2);

                //�÷��̾ ������ ���ʿ� ������ false�� ��ȯ
                return false;
            }
        }
        else
        {
            Debug.LogError("CharacterManager.instance.basePlayer == null");
            return false;
        }
    }

    //������ �� �Լ�
    private void Rush()
    {
        controller.rb.AddForce(Vector2.right * _direct * enemy.enemyStatHandler.stat.attackSpeed, ForceMode2D.Force);
    }

    //���� ���ϴ� �Լ� ( 0: ����, 1: ������, 2: ���� )
    public void SetDirect_0Stop_1Right_2Left(int _dir)
    {
        if (_dir == 1)
        {
            _direct = _speed * 1.0f;
            enemy.transform.localScale = new Vector2(-2.5f, 2.5f);
        }
        else if (_dir == 2)
        {
            _direct = _speed * -1.0f;
            enemy.transform.localScale = new Vector2(2.5f, 2.5f);
        }
        else _direct = 0.0f;
    }
}
