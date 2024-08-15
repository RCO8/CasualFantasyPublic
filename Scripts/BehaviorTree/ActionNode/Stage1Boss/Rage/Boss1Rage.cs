using DG.Tweening;
using UnityEngine;

public class Boss1Rage : Boss1ActionNodeFunction
{
    private bool _isShake = false;
    private bool _isFirst = true;

    public Boss1Rage(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override INode.ENodeState CheckNode()
    {
        //상태 확인
        return CheckFunction(Boss1AIController.EBossState.RAGE);
    }

    public INode.ENodeState RageNode()
    {
        if (_isFirst)
        {
            _isFirst = false;

            //Rage 애니메이션 재생
            controller.animator.SetTrigger(enemy.bossAnimationData.RageParameterHash);
        }

        //애니메이션 재생 중인지 확인
        if (IsPlayingAnimation(Define_AnimationNodeName.ATTACK_BOW, 3.0f) == 2)
        {
            //상태 변경
            controller.RandomAttackState();

            //옵션 초기화
            _isShake = false;
            _isFirst = true;

            return INode.ENodeState.ENS_Success;
        }

        if (!_isShake)
        {
            _isShake = true;

            //각각 매개변수들은 밑에서 설명, 지속 시간을 제외한 모든 매개변수들은 생략 가능하다.
            //순서대로 지속시간, 흔들리는 강도, 얼마나 흔들릴지, 흔들리는 방향(0.0f ~ 180.0f, 90이상하면 보기 싫어짐),
            //자연스럽게 흔들림 효과 사라짐, 흔들림 모드(기본값은 full, Harmonic으로 선택 시 균형있고 더 쾌적)
            Camera.main.transform.DOShakePosition(0.75f);
        }

        return INode.ENodeState.ENS_Running;
    }
}
