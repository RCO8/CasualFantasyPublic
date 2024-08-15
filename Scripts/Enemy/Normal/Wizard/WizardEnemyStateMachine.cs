using UnityEngine;

public class WizardEnemyStateMachine : CharacterState
{
    public WizardEnemy_Battle enemy;

    public WizardIdleState idleState;
    public WizardMoveState moveState;
    public WizardAttackState attackState;
    public WizardSkillState skillState;
    public WizardStunState stunState;
    public WizardDeadState deadState;
    public WizardEnemyStateMachine(WizardEnemy_Battle _enemy)
    {
        enemy = _enemy;

        idleState = new WizardIdleState(this);
        moveState = new WizardMoveState(this);
        attackState = new WizardAttackState(this);
        skillState = new WizardSkillState(this);
        stunState = new WizardStunState(this);
        deadState = new WizardDeadState(this);
    }
}

public class WizardIdleState : WizardEnemyBaseState
{
    public WizardIdleState(WizardEnemyStateMachine _stateMachine) : base(_stateMachine)
    {
    }
}

public class WizardMoveState : WizardEnemyBaseState
{
    public WizardMoveState(WizardEnemyStateMachine _stateMachine) : base(_stateMachine)
    {

    }
}

public class WizardAttackState : WizardEnemyBaseState
{
    public WizardAttackState(WizardEnemyStateMachine _stateMachine) : base(_stateMachine)
    {

    }
}

public class WizardSkillState : WizardEnemyBaseState
{
    public WizardSkillState(WizardEnemyStateMachine _stateMachine) : base(_stateMachine)
    {

    }
}

public class WizardStunState : WizardEnemyBaseState
{
    public WizardStunState(WizardEnemyStateMachine _stateMachine) : base(_stateMachine)
    { 
    }
}

public class WizardDeadState : WizardEnemyBaseState
{
    public WizardDeadState(WizardEnemyStateMachine _stateMachine) : base(_stateMachine)
    {

    }
}