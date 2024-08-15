using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    //Battle Class

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CharacterManager.instance.enemy = this;

        //if (isField) ExitStateMachine();  //FIX
        //else EnterStateMachine();         //FIX
    }


    //public override void EnterStateMachine()
    //{
    //    //SetBattleMode(true);              //FIX
    //}

    //public override void ExitStateMachine()
    //{
    //    //SetBattleMode(false);             //FIX
    //}

    public void TurnAround()    //�¿� ��ȯ
    {
        Vector2 turn = Vector2.one;
        turn.x = -1;
        Transform targetPlayer = CharacterManager.instance.basePlayer.transform;

        //if(rectTransform.localScale.x > 0)  //����
        //{
        //    if(targetPlayer.position.x > transform.position.x)
        //        rectTransform.localScale *= turn;
        //}
        //else if(rectTransform.localScale.x < 0) //����
        //{
        //    if (targetPlayer.position.x < transform.position.x)
        //        rectTransform.localScale *= turn;
        //}
    }

    public float NormalizeTime(Animator animator, string tag)    //�ִϸ��̼� �������� ������
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if(animator.IsInTransition(0) && nextInfo.IsTag(tag))
            return nextInfo.normalizedTime;
        else if(!animator.IsInTransition(0) && currentInfo.IsTag(tag))
            return currentInfo.normalizedTime;
        else
            return 0;
    }


}
