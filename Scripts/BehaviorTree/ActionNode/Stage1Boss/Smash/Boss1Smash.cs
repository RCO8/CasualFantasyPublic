using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss1Smash : Boss1ActionNodeFunction
{
    private Vector2 _position;
    private Vector2 _obstaclePosition;

    private bool _isJumpEnd = false;     //������ ��������

    public Boss1Smash(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override sealed INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(EnemyAIController.EBossState.SMASH);
    }

    public INode.ENodeState JumpNode()
    {
        //ó�� �� ��带 �����ϸ� �ִϸ��̼� ���, �ݶ��̴� Off
        if (isFirst(Define_Boss1AniParam.STUN_BOOL, enemy.bossAnimationData.StunParameterHash, false)) 
        {
            SetEnemyPosition();
            MoveToPlayerPosition();
        }

        //������ �������� ������ ��ȯ�ϰ� ���� ���� �Ѿ���� �Ѵ�.
        if (_isJumpEnd)
        {
            //�ݶ��̴� On
            enemy.damageCollider.gameObject.SetActive(true);
            //Debug.Log()

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }

    public INode.ENodeState SmashNode()
    {
        //���� ���� �ٴٸ��� ������ ��ȯ�ϰ� ���¸� �����Ѵ�.
        if (enemy.isGround)
        {
            //�ִϸ��̼� ���� ( ����� Stun �ִϸ��̼����� �����ߴ�. )
            controller.animator.SetBool(enemy.bossAnimationData.StunParameterHash, false);

            //�ɼ� �ʱ�ȭ
            _isJumpEnd = false;

            //��� ���·� ����
            controller.ChangeState(Boss1AIController.EBossState.STANDBY);

            //��ֹ� ������Ʈ ��ȯ ( �ݳ� ������ ���� )
            if (enemy.isRageMode)
            {
                //���� �Ű��������� �ؿ��� ����, ���� �ð��� ������ ��� �Ű��������� ���� �����ϴ�.
                //������� ���ӽð�, ��鸮�� ����, �󸶳� ��鸱��, ��鸮�� ����(0.0f ~ 180.0f, 90�̻��ϸ� ���� �Ⱦ���),
                //�ڿ������� ��鸲 ȿ�� �����, ��鸲 ���(�⺻���� full, Harmonic���� ���� �� �����ְ� �� ����)
                Camera.main.transform.DOShakePosition(0.2f);

                //������Ʈ 2���� ��ȯ��
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
        //NULLüũ
        if(CharacterManager.instance.basePlayer != null)
        {
            //��ġ ����( �÷��̾� �Ӹ� ����, ���� �ȴ�� �����ؼ� )
            _position = CharacterManager.instance.basePlayer.transform.position;
            _position.x = Mathf.Clamp(_position.x, Define.BATTLESCENE_LEFTWALL_X, Define.BATTLESCENE_RIGHTWALL_X);
            _position.y += 10.0f;
        }
        else Debug.LogError("Boss1Smash -> SetEnemyPosition: CharacterManager.instance.basePlayer == null");
    }

    private void MoveToPlayerPosition()
    {
        //�������� �̿��� �÷��̾� �Ӹ� ���� �̵��ϴ� ����, �� ���� �� _isJumpEnd�� true�� �����.
        enemy.transform.DOMove(_position, 1.0f / enemy.enemyStatHandler.stat.attackSpeed)
            .OnComplete(() => _isJumpEnd = true);
    }

    private void SetObstaclePosition()
    {
        //����߸� ��ֹ� ��ġ
        _obstaclePosition.x = Random.Range(Define.BATTLESCENE_LEFTWALL_X, Define.BATTLESCENE_RIGHTWALL_X);
        _obstaclePosition.y = 5.0f;
    }
}
