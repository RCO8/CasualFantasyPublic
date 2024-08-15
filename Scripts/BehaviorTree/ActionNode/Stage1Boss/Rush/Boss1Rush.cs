using UnityEngine;

public class Boss1Rush : Boss1ActionNodeFunction
{
    //돌진할 방향과 스피드
    private float _speed = 12.5f;
    private float _direct;

    public Boss1Rush(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override sealed INode.ENodeState CheckNode()
    {
        //상태 확인
        return CheckFunction(Boss1AIController.EBossState.RUSH);
    }

    public INode.ENodeState RushNode()
    {
        //처음 이 노드를 시작하면 애니메이션 재생, 콜라이더 On
        isFirst(Define_Boss1AniParam.RUSH_BOOL, enemy.bossAnimationData.RunParameterHash, true);

        //플레이어의 위치에 따라 바라보는 방향과 돌진하는 방향을 정해줌
        IsThePlayerToRight();
        Rush();

        return INode.ENodeState.ENS_Running;
    }

    private bool IsThePlayerToRight()
    {
        //null체크
        if (CharacterManager.instance.basePlayer != null)
        {
            if (enemy.gameObject.transform.position.x < CharacterManager.instance.basePlayer.gameObject.transform.position.x)
            {
                SetDirect_0Stop_1Right_2Left(1);

                //플레이어가 적보다 오른쪽에 있으면 true를 반환
                return true;
            }
            else
            {
                SetDirect_0Stop_1Right_2Left(2);

                //플레이어가 적보다 왼쪽에 있으면 false를 반환
                return false;
            }
        }
        else
        {
            Debug.LogError("CharacterManager.instance.basePlayer == null");
            return false;
        }
    }

    //돌진할 때 함수
    private void Rush()
    {
        controller.rb.AddForce(Vector2.right * _direct * enemy.enemyStatHandler.stat.attackSpeed, ForceMode2D.Force);
    }

    //방향 정하는 함수 ( 0: 정지, 1: 오른쪽, 2: 왼쪽 )
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
