//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Boss1StandByState : Boss1BaseState
//{
//    //temp
//    private float _time = 0.0f;

//    public Boss1StandByState(Boss1StateMachine _stateMachine) : base(_stateMachine)
//    {

//    }

//    public override void Enter()
//    {
//        base.Enter();

//        StartAnimation(stateMachine.boss1.boss1AnimationData.IdleParameterHash);
//    }

//    public override void Exit()
//    {
//        base.Exit();

//        StopAnimation(stateMachine.boss1.boss1AnimationData.IdleParameterHash);
//    }

//    public override void Update()
//    {
//        base.Update();

//        //�ӽ� ( ���߿� ������ �غ� �ִϸ��̼��� ������ ���� ���·� �Ѿ�� ������ © ���̴�, �ڷ�ƾ �Ŵ����� ���� �ۼ��� �� )
//        _time += Time.deltaTime;
//        if (_time > 1.0f)
//        {
//            _time = 0.0f;
//            RandomPattern();
//        }
//    }
//}
