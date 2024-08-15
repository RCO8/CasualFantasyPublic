using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static EnemyAIController;

public class Boss4Bounce : Boss4ActionNodeFunction
{
    private bool _isMoving = false;
    private bool _isMoveEnd = false;
    private bool _isLaunched = false;
    private bool _isDamaged = false;

    private float _spd = 5.0f;
    private float _x = 0.0f;
    private float _y = 0.0f;

    private float _offset = 1.25f;

    public Boss4Bounce(Boss4_Battle _enemy, Boss4AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //Debug.Log(controller.eBossState);
        //���� Ȯ��
        return CheckFunction(controller.bounceState);
    }

    public INode.ENodeState LevitationNode()
    {
        isFirst(Define_Boss4AniParam.BOUNCE_BOOL, enemy.bossAnimationData.BounceParameterHash);

        //�������� �� ��������
        if(_isMoveEnd)
        {
            return INode.ENodeState.ENS_Success;
        }

        //�����̴� ���̸�
        if(!_isMoving)
        {
            _isMoving = true;

            //�� ����� �̵�
            enemy.gameObject.transform.DOMove(Vector2.zero, 2.0f).OnComplete(() => _isMoveEnd = true);

            //�߷� 0
            controller.rb.gravityScale = 0.0f;
        }

        return INode.ENodeState.ENS_Running;
    }

    public INode.ENodeState BounceNode()
    {
        if (!_isLaunched)
        {
            //�߻� on
            _isLaunched = true;

            //���� �������� �߻�
            _x = Random.Range(0, 2) == 0 ? -1 : 1;
            _y = Random.Range(0, 2) == 0 ? -1 : 1;

            //�̵�
            controller.rb.velocity = new Vector2(_x * _spd, _y * _spd);
        }

        //���� ƨ��� �ϱ�
        if (enemy.gameObject.transform.position.y > 4.0f || enemy.gameObject.transform.position.y < -3.9f)
        {
            controller.rb.velocity = new Vector2(controller.rb.velocity.x, controller.rb.velocity.y * -1.0f);
        }
        if (enemy.gameObject.transform.position.x < Define.BATTLESCENE_LEFTWALL_X - _offset ||
            enemy.gameObject.transform.position.x > Define.BATTLESCENE_RIGHTWALL_X + _offset)
        {
            controller.rb.velocity = new Vector2(controller.rb.velocity.x * -1.0f, controller.rb.velocity.y);
        }

        if (_isDamaged)
        {
            //�߷� ����
            controller.rb.gravityScale = 10.0f;

            //�ӵ� ����
            controller.rb.velocity = Vector2.zero;

            //���� ���·� ����
            controller.ChangeState(EnemyAIController.EBossState.STUN);

            //�ɼ� �ʱ�ȭ
            _isMoving = false;
            _isMoveEnd = false;
            _isLaunched = false;
            _isDamaged = false;
            _x = 0.0f;
            _y = 0.0f;

            //�ִϸ��̼� ����
            controller.animator.SetBool(enemy.bossAnimationData.BounceParameterHash, false);

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }

    public void Damaged()
    {
        _isDamaged = true;

        //�ݸ��� off
        enemy.damageCollider.gameObject.SetActive(false);
    }
}
