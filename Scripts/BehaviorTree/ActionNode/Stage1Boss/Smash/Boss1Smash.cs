using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss1Smash : Boss1ActionNodeFunction
{
    private Vector2 _position;
    private Vector2 _obstaclePosition;

    private bool _isJumpEnd = false;     //점프가 끝났는지

    public Boss1Smash(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override sealed INode.ENodeState CheckNode()
    {
        //상태 확인
        return CheckFunction(EnemyAIController.EBossState.SMASH);
    }

    public INode.ENodeState JumpNode()
    {
        //처음 이 노드를 시작하면 애니메이션 재생, 콜라이더 Off
        if (isFirst(Define_Boss1AniParam.STUN_BOOL, enemy.bossAnimationData.StunParameterHash, false)) 
        {
            SetEnemyPosition();
            MoveToPlayerPosition();
        }

        //점프가 끝났으면 성공을 반환하고 다음 노드로 넘어가도록 한다.
        if (_isJumpEnd)
        {
            //콜라이더 On
            enemy.damageCollider.gameObject.SetActive(true);
            //Debug.Log()

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }

    public INode.ENodeState SmashNode()
    {
        //적이 땅에 다다르면 성공을 반환하고 상태를 변경한다.
        if (enemy.isGround)
        {
            //애니메이션 중지 ( 모션은 Stun 애니메이션으로 설정했다. )
            controller.animator.SetBool(enemy.bossAnimationData.StunParameterHash, false);

            //옵션 초기화
            _isJumpEnd = false;

            //대기 상태로 변경
            controller.ChangeState(Boss1AIController.EBossState.STANDBY);

            //장애물 오브젝트 소환 ( 격노 상태일 때만 )
            if (enemy.isRageMode)
            {
                //각각 매개변수들은 밑에서 설명, 지속 시간을 제외한 모든 매개변수들은 생략 가능하다.
                //순서대로 지속시간, 흔들리는 강도, 얼마나 흔들릴지, 흔들리는 방향(0.0f ~ 180.0f, 90이상하면 보기 싫어짐),
                //자연스럽게 흔들림 효과 사라짐, 흔들림 모드(기본값은 full, Harmonic으로 선택 시 균형있고 더 쾌적)
                Camera.main.transform.DOShakePosition(0.2f);

                //오브젝트 2개를 소환함
                SetObstaclePosition();
                enemy.MakeObstacle(_obstaclePosition);
                SetObstaclePosition();
                enemy.MakeObstacle(_obstaclePosition);
            }
            
            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }

    private void SetEnemyPosition()
    {
        //NULL체크
        if(CharacterManager.instance.basePlayer != null)
        {
            //위치 지정( 플레이어 머리 위로, 벽과 안닿게 조절해서 )
            _position = CharacterManager.instance.basePlayer.transform.position;
            _position.x = Mathf.Clamp(_position.x, Define.BATTLESCENE_LEFTWALL_X, Define.BATTLESCENE_RIGHTWALL_X);
            _position.y += 10.0f;
        }
        else Debug.LogError("Boss1Smash -> SetEnemyPosition: CharacterManager.instance.basePlayer == null");
    }

    private void MoveToPlayerPosition()
    {
        //보간법을 이용해 플레이어 머리 위로 이동하는 로직, 다 끝난 후 _isJumpEnd를 true로 만든다.
        enemy.transform.DOMove(_position, 1.0f / enemy.enemyStatHandler.stat.attackSpeed)
            .OnComplete(() => _isJumpEnd = true);
    }

    private void SetObstaclePosition()
    {
        //떨어뜨릴 장애물 위치
        _obstaclePosition.x = Random.Range(Define.BATTLESCENE_LEFTWALL_X, Define.BATTLESCENE_RIGHTWALL_X);
        _obstaclePosition.y = 5.0f;
    }
}
