using DG.Tweening;
using UnityEngine;

public class Boss1Rage : Boss1ActionNodeFunction
{
    private bool _isShake = false;
    private bool _isFirst = true;

    public Boss1Rage(Boss1_Battle _enemy, Boss1AIController _con) : base(_enemy, _con) { }

    public override INode.ENodeState CheckNode()
    {
        //���� Ȯ��
        return CheckFunction(Boss1AIController.EBossState.RAGE);
    }

    public INode.ENodeState RageNode()
    {
        if (_isFirst)
        {
            _isFirst = false;

            //Rage �ִϸ��̼� ���
            controller.animator.SetTrigger(enemy.bossAnimationData.RageParameterHash);
        }

        //�ִϸ��̼� ��� ������ Ȯ��
        if (IsPlayingAnimation(Define_AnimationNodeName.ATTACK_BOW, 3.0f) == 2)
        {
            //���� ����
            controller.RandomAttackState();

            //�ɼ� �ʱ�ȭ
            _isShake = false;
            _isFirst = true;

            return INode.ENodeState.ENS_Success;
        }

        if (!_isShake)
        {
            _isShake = true;

            //���� �Ű��������� �ؿ��� ����, ���� �ð��� ������ ��� �Ű��������� ���� �����ϴ�.
            //������� ���ӽð�, ��鸮�� ����, �󸶳� ��鸱��, ��鸮�� ����(0.0f ~ 180.0f, 90�̻��ϸ� ���� �Ⱦ���),
            //�ڿ������� ��鸲 ȿ�� �����, ��鸲 ���(�⺻���� full, Harmonic���� ���� �� �����ְ� �� ����)
            Camera.main.transform.DOShakePosition(0.75f);
        }

        return INode.ENodeState.ENS_Running;
    }
}
