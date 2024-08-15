public class WizardEnemyBaseState : IState
{
    protected WizardEnemyStateMachine stateMachine;
    public WizardEnemyBaseState(WizardEnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
    }
}
