using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemy_Battle : NormalEnemy
{
    [Header("상태 머신")]
    public WizardEnemyStateMachine stateMachine;
    public NormalEnemyAnimation animationData;

    protected override void Awake()
    {
        base.Awake();

        CharacterManager.instance.enemy = this;

        stateMachine = new WizardEnemyStateMachine(this);
        animationData = new NormalEnemyAnimation();
        animationData.Initialize();
        //InitializeStat();

        //트리거 버그 방지 코드
        //swordEnemyAttack.enabled = false;
    }
    private void Start()
    {
        stateMachine.ChangeState(stateMachine.idleState);
    }
    private void Update()
    {
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Define.TAG_WALL)
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }

    //protected override void InitializeStat()
    //{
    //    stat.maxHealth = 20f;
    //    stat.maxMana = 10f;
    //    stat.attackDamage = 5f;
    //    stat.attackSpeed = 3f;
    //    stat.moveSpeed = 3f;
    //}

    public override void EnemyDeath()
    {
        base.EnemyDeath();
        stateMachine.ChangeState(stateMachine.deadState);
    }

    //FSM
    //public override void EnterStateMachine()
    //{
    //    //SetBattleMode(true);      //FIX
    //    stateMachine.ChangeState(stateMachine.idleState);
    //}
    //public override void ExitStateMachine()
    //{
    //    //SetBattleMode(false);     //FIX
    //    stateMachine.ChangeState(null);
    //}
}
