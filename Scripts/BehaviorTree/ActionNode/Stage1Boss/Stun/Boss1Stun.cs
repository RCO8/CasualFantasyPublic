using UnityEngine;
using DG.Tweening;

public class Boss1Stun : Boss1ActionNodeFunction
{
    public Boss1Stun(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override INode.ENodeState CheckNode()
    {
        //상태 확인
        return CheckFunction(Boss1AIController.EBossState.STUN);
    }

    public INode.ENodeState StunNode()
    {
        //처음 이 노드를 시작하면 애니메이션 재생, 콜라이더 Off
        if (isFirst(Define_Boss1AniParam.STUN_BOOL, enemy.bossAnimationData.StunParameterHash, false))
        {
            //뒤로 밀려나기
            KnockBack();

            //돌진 애니메이션 중지
            controller.animator.SetBool(enemy.bossAnimationData.RunParameterHash, false);

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
            controller.RandomAttackState();

            //콜라이더 On
            enemy.damageCollider.gameObject.SetActive(true);

            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Running;
    }

    //뒤로 밀려나가는 함수
    public void KnockBack()
    {
        float _x;

        //오른쪽
        if(enemy.transform.position.x > 0.0f) _x = enemy.gameObject.transform.position.x - 3.0f;
        //왼쪽
        else _x = enemy.gameObject.transform.position.x + 3.0f;

        Vector2 targetPos = new Vector2(_x, enemy.gameObject.transform.position.y);

        enemy.gameObject.transform.DOJump(targetPos, 0.5f, 1, 0.25f);
    }
}
