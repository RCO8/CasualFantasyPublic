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
        //상태 확인
        return CheckFunction(controller.bounceState);
    }

    public INode.ENodeState LevitationNode()
    {
        isFirst(Define_Boss4AniParam.BOUNCE_BOOL, enemy.bossAnimationData.BounceParameterHash);

        //움직임이 다 끝났으면
        if(_isMoveEnd)
        {
            return INode.ENodeState.ENS_Success;
        }

        //움직이는 중이면
        if(!_isMoving)
        {
            _isMoving = true;

            //한 가운데로 이동
            enemy.gameObject.transform.DOMove(Vector2.zero, 2.0f).OnComplete(() => _isMoveEnd = true);

            //중력 0
            controller.rb.gravityScale = 0.0f;
        }

        return INode.ENodeState.ENS_Running;
    }

    public INode.ENodeState BounceNode()
    {
        if (!_isLaunched)
        {
            //발사 on
            _isLaunched = true;

            //랜덤 방향으로 발사
            _x = Random.Range(0, 2) == 0 ? -1 : 1;
            _y = Random.Range(0, 2) == 0 ? -1 : 1;

            //이동
            controller.rb.velocity = new Vector2(_x * _spd, _y * _spd);
        }

        //벽에 튕기게 하기
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
            //중력 복구
            controller.rb.gravityScale = 10.0f;

            //속도 복구
            controller.rb.velocity = Vector2.zero;

            //스턴 상태로 변경
            controller.ChangeState(EnemyAIController.EBossState.STUN);

            //옵션 초기화
            _isMoving = false;
            _isMoveEnd = false;
            _isLaunched = false;
            _isDamaged = false;
            _x = 0.0f;
            _y = 0.0f;

            //애니메이션 종료
            controller.animator.SetBool(enemy.bossAnimationData.BounceParameterHash, false);

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }

    public void Damaged()
    {
        _isDamaged = true;

        //콜리전 off
        enemy.damageCollider.gameObject.SetActive(false);
    }
}
