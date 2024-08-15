using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Boss4Stun : Boss4ActionNodeFunction
{
    public Boss4Stun(Boss4_Battle _enemy, Boss4AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        //상태 확인
        return CheckFunction(EnemyAIController.EBossState.STUN);
    }

    public INode.ENodeState StunNode()
    {
        //처음 이 노드를 시작하면 애니메이션 재생, 콜라이더 Off
        if (isFirst(Define_Boss4AniParam.STUN_BOOL, enemy.bossAnimationData.StunParameterHash, false))
        {
            //중앙으로 밀려나기
            KnockBack();

            //바운스 애니메이션 중지
            controller.animator.SetBool(enemy.bossAnimationData.BounceParameterHash, false);

            //이펙트 띄우기
            controller.enemyEffect.ChangeEffectState(EffectState.Stun);
        }

        //2초가 지나면
        if (controller.SetTimeAndCheck(2.0f))
        {
            //이펙트 감추기
            controller.enemyEffect.ChangeEffectState(EffectState.None);

            //Stun 애니메이션 중지
            controller.animator.SetBool(enemy.bossAnimationData.StunParameterHash, false);

            //상태 변경
            controller.ChangeState(EnemyAIController.EBossState.STANDBY);

            //콜라이더 On
            enemy.damageCollider.gameObject.SetActive(true);

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }

    //중앙으로 밀려나가는 함수
    public void KnockBack()
    {
        Vector2 targetPos = new Vector2(0.0f, -3.8f);

        enemy.gameObject.transform.DOJump(targetPos, 0.5f, 1, 0.25f);
    }
}
